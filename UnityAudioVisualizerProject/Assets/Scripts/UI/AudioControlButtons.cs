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
        });
        buttons[1].onClick.AddListener(delegate {
            PlayPause();
        });
        buttons[2].onClick.AddListener(delegate {
            Skip();
        });

        playButtonImage = buttons[1].image;
        if (source.playOnAwake)
            playButtonImage.sprite = icons[1];
        else
            playButtonImage.sprite = icons[0];

        UpdateTrackTitle();
    }

    public void Rewind()
    {
        UpdateTrackTitle();
    }

    public void PlayPause()
    {
        if (source.isPlaying) {
            source.Pause();
            playButtonImage.sprite = icons[0];
        } else {
            source.Play();
            playButtonImage.sprite = icons[1];
        }

        UpdateTrackTitle();
    }

    public void Skip()
    {
        UpdateTrackTitle();
    }

    public void UpdateTrackTitle()
    {
        trackTitle.text = source.clip.name;
    }
}
