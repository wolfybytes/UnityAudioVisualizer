using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumDataReader : MonoBehaviour
{
    [HideInInspector] public static float spectrumValue { get; private set; }
    private float[] m_audioSpectrum;

    [Header("Spectrum Data")]
    public FFTWindow windowType = FFTWindow.Hamming;

    [Header("Debug")]
    public bool enableDebug = false;

    private void Start()
    {
        m_audioSpectrum = new float[256];
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, windowType);

        // Grabbing the first value of the spectrum to track beats.
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }

        if (enableDebug)
        {
            for (int i = 1; i < m_audioSpectrum.Length - 1; i++)
            {
                Debug.DrawLine(new Vector3(i - 1, m_audioSpectrum[i] + 10, 0), new Vector3(i, m_audioSpectrum[i + 1] + 10, 0), Color.red);
                Debug.DrawLine(new Vector3(i - 1, Mathf.Log(m_audioSpectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(m_audioSpectrum[i]) + 10, 2), Color.cyan);
                Debug.DrawLine(new Vector3(Mathf.Log(i - 1), m_audioSpectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), m_audioSpectrum[i] - 10, 1), Color.green);
                Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(m_audioSpectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(m_audioSpectrum[i]), 3), Color.blue);
            }
        }
    }
}
