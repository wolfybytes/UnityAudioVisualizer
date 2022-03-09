using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightController : MonoBehaviour
{
    private int numPresets = 4;
    private Quaternion targetRotation;
    public enum LightPreset
    {
        Day,
        Dusk,
        Night,
        Dawn
    };
    public LightPreset lightPreset;
    public Vector3[] rotationPresets;
    public float presetUpdateSpeed = 8f;

    public static SunlightController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        rotationPresets = new Vector3[numPresets];
        rotationPresets[0] = new Vector3(50, -30, 0);
        rotationPresets[1] = new Vector3(176, -30, 0);
        rotationPresets[2] = new Vector3(284, -30, 0);
        rotationPresets[3] = new Vector3(9, -30, 0);

        UpdatePreset(0);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, presetUpdateSpeed * Time.deltaTime);
    }

    public void UpdatePreset(int index)
    {
        lightPreset = (LightPreset)index;
        targetRotation = Quaternion.Euler(rotationPresets[index]);
    }

    public void AdvancePreset()
    {
        int nextIndex = ((int)lightPreset + 1) % numPresets;
        UpdatePreset(nextIndex);
    }
}
