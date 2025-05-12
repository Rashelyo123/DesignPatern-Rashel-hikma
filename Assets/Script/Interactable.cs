using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvent;
    public String PromptMessage;



    public void BaseInteracr()
    {
        if (useEvent)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();

        }
        Interact();
    }
    protected virtual void Interact()
    {

    }
}
