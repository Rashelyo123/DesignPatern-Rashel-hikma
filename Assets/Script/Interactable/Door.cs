namespace Script.Object
{
    using UnityEngine;
    public class Door : Interactable
    {
        [SerializeField] private bool _isLock;
        private bool isOpen = false;
        [SerializeField] private GameObject _visualDoorOpen;
        [SerializeField] private GameObject _visualDoorClose;
        [SerializeField] private AudioClip _doorOpenSound;
        [SerializeField] private AudioClip _doorCloseSound;
        [SerializeField] private AudioClip _doorLockSound;

        [SerializeField] private AudioSource _audioSource;


        public bool IsLock
        {
            get => _isLock;
            set => _isLock = value;
        }

        protected override void Interact()
        {
            if (_isLock) {
                Debug.Log("Door is locked");
                _audioSource.PlayOneShot(_doorLockSound);
                return;
            }
            if (isOpen) {
                Close();
            } else {
                Open();
            }
        }
        private void Open()
        {
            isOpen = true;
            _visualDoorOpen.SetActive(true);
            _visualDoorClose.SetActive(false);
            _audioSource.PlayOneShot(_doorOpenSound);
            Debug.Log("Door is open");
        }
        private void Close()
        {
            isOpen = false;
            _visualDoorOpen.SetActive(false);
            _visualDoorClose.SetActive(true);
            _audioSource.PlayOneShot(_doorCloseSound);
            Debug.Log("Door is close");
        }
    }
}