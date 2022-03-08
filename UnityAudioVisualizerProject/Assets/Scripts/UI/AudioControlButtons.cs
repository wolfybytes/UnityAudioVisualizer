using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioControlButtons : MonoBehaviour
{
    private AudioSource source;
    private Image playButtonImage;

    public TextMeshProUGUI trackTitle;
    public Sprite[] icons;
    public Button[] buttons;

    private void Start()
    {
        source = FindObjectOfType<AudioSource>();

        buttons[0].onClick.AddListener(delegate {
            Rewind();
            AutoPlaylistOrganizer.instance.Rewind();
        });
        buttons[1].onClick.AddListener(delegate {
            AutoPlaylistOrganizer.instance.PlayPause();
            PlayPauseUpdateIcons();
        });
        buttons[2].onClick.AddListener(delegate {
            Skip();
            AutoPlaylistOrganizer.instance.Skip();
        });

        playButtonImage = buttons[1].image;
        if (source.playOnAwake)
            playButtonImage.sprite = icons[1];
        else
            playButtonImage.sprite = icons[0];

        AutoPlaylistOrganizer.instance.onUpdateAudioTrack += UpdateTrackTitle;
    }

    public void Rewind()
    {
        
    }

    public void PlayPauseUpdateIcons()
    {
        if (source.isPlaying) {
            playButtonImage.sprite = icons[0];
        } else {
            playButtonImage.sprite = icons[1];
        }

    }

    public void Skip()
    {
        
    }

    public void UpdateTrackTitle(int index)
    {
        trackTitle.text = AutoPlaylistOrganizer.instance.GetCurrentAudioTrack().name;
    }
}
