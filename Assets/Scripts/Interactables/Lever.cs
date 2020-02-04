using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public delegate void Active();
    public Active active;

    public override void Interact()
    {
        Debug.Log("Lever pulled");
        if(active != null)
            active.Invoke();
    }
}