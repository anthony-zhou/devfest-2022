using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    private bool isOpen = false;
    private Animation anim;
    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void playAnim()
    {
        if (!isOpen)
        {
            anim.Play("New Animation");
        }
        else
        {
            anim.Play("New Anim2");
        }
        isOpen = !isOpen;
    }
}
