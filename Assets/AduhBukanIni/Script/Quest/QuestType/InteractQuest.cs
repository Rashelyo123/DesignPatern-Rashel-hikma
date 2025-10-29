namespace Script.Quest.QuestType
{
    using UnityEngine;
    public class InteractQuest : Quest
    {
        [SerializeField] private Interactable _itemToCollect;

        public override void StartQuest()
        {
            base.StartQuest();

            _itemToCollect.OnInteract.AddListener(OnItemInteract);
        }
        private void OnItemInteract()
        {
            QuestManager.Instance.CompleteQuest();
        }

        public override void CompleteQuest()
        {

            base.CompleteQuest();

            _itemToCollect.OnInteract.RemoveListener(OnItemInteract);
        }



    }
}