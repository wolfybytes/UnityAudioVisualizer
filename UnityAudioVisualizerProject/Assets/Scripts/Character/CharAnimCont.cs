using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimCont : MonoBehaviour
{
    private AudioSource source;
    private Animator anim;
    private int danceIndex = 0;
    private int maxNumDances = 2;

    private void Start()
    {
        source = FindObjectOfType<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (source.isPlaying)
            anim.SetBool("isPaused", false);
        else
            anim.SetBool("isPaused", true);
    }

    public void AdvanceDance()
    {
        danceIndex = (danceIndex + 1) % maxNumDances;
        if (danceIndex == 0)
            anim.SetTrigger("HouseDance");
        else if (danceIndex == 1)
            anim.SetTrigger("HipHopDance");
    }
}
