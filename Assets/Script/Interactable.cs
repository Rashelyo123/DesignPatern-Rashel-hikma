using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public bool CanInteract = true;
    public String PromptMessage;
    public UnityEvent OnInteract;

    public virtual void BaseInteract()
    {
        if (!CanInteract)
            return;
        Interact();
    }
    protected virtual void Interact()
    {
        OnInteract?.Invoke();
    }
}