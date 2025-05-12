using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    [SerializeField] private AudioClip _onPickUpSound;
    protected override void Interact()
    {
        base.Interact();
        if (CanInteract) {
            //play sound
            AudioSource.PlayClipAtPoint(_onPickUpSound, transform.position);
            //destroy gameobject
        }
    }


}