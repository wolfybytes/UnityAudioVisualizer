using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    private float m_previousAudioValue;
    private float m_currentAudioValue;
    private float m_timer;
    protected bool m_isBeat;

    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restLerpTime;

    public virtual void OnBeat()
    {
        Debug.Log("beat");
        m_timer = 0;
        m_isBeat = true;
    }

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_currentAudioValue;
        m_currentAudioValue = SpectrumDataReader.spectrumValue;

        if (m_previousAudioValue > bias && m_currentAudioValue <= bias) {
            if (m_timer > timeStep) {
                OnBeat();
            }
        }

        if (m_previousAudioValue <= bias && m_currentAudioValue > bias) {
            if (m_timer > timeStep) {
                OnBeat();
            }
        }

        m_timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }
}
