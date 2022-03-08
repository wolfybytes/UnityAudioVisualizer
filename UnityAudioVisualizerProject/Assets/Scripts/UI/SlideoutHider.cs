using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideoutHider : MonoBehaviour
{
    private bool setHide;
    private MenuSlideoutUI[] allSlideouts;

    private void Start()
    {
        allSlideouts = FindObjectsOfType<MenuSlideoutUI>();
        for (int i = 0; i < allSlideouts.Length; i++) {
            allSlideouts[i].slideoutIndex = i;
            allSlideouts[i].onOpenSlideout += UpdateHidingSlideouts;
        }
    }

    public void UpdateHidingSlideouts(int index)
    {
        if (allSlideouts[index].isOpen)
            setHide = true;
        else
            setHide = false;

        for (int i = 0; i < allSlideouts.Length; i++) {
            if (i == index) {
                
            } else {
                if (setHide)
                    allSlideouts[i].gameObject.SetActive(false);
                else
                    allSlideouts[i].gameObject.SetActive(true);
            }
        }
    }
}
