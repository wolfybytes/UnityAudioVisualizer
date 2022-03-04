using UnityEngine;
using UnityEngine.UI;

public class AudioControlButtons : MonoBehaviour
{
    private AudioSource source;

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
    }

    public void Rewind()
    {

    }

    public void PlayPause()
    {
        if (source.isPlaying) {
            source.Pause();
        } else {
            source.Play();
        }
    }

    public void Skip()
    {

    }
}
