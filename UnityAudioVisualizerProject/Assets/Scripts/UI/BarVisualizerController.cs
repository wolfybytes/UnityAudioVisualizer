using UnityEngine;

public class BarVisualizerController : MonoBehaviour
{
    private GameObject[] o;

    public GameObject template;
    public int numBars;

    public void Start()
    {
        o = new GameObject[numBars];
        for (int i = 0; i < numBars; i++)
        {
            o[i] = Instantiate(template, template.transform.parent);
            o[i].GetComponent<AudioSyncScale>().bias = i + 1;
        }

        template.SetActive(false);
    }

}
