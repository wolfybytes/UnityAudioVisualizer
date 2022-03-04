using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimCont : MonoBehaviour
{
    private AudioSource source;
    private Animator anim;

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
}
