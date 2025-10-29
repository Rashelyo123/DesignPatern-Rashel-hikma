namespace Script.Quest.QuestType.GoToQuest
{
    using System;
    using UnityEngine;
    public class GoToQuestInteract : Interactable
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                OnInteract?.Invoke();
            }
        }
    }
}