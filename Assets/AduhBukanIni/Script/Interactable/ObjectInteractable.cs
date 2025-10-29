namespace Script.Object
{
    using UnityEngine;
    public class ObjectInteractable : Interactable
    {
        [SerializeField] private AudioClip _onInteract;

        protected override void Interact()
        {
            base.Interact();

            if (_onInteract != null) {
                AudioSource.PlayClipAtPoint(_onInteract, transform.position);
            }
        }
    }
}