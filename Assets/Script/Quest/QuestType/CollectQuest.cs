namespace Script.Quest.QuestType
{
    using System.Collections.Generic;
    using UnityEngine;
    public class CollectQuest : Quest
    {
        [SerializeField] int totalItemsToCollect = 5;
        [SerializeField] private int _curretItemsToCollect = 0;
        [SerializeField] private List<Interactable> collectableItemsList = new List<Interactable>();
        public override void StartQuest()
        {
            base.StartQuest();
            foreach (var item in collectableItemsList) {
                item.OnInteract.AddListener(CollectItem);
            }

            //tampiokan deskiprsi currect item to collect
            Description = $"{_curretItemsToCollect} /{totalItemsToCollect} Makanan DiAmbil";
        }

        public void CollectItem()
        {
            _curretItemsToCollect++;
            if (_curretItemsToCollect >= totalItemsToCollect) {
                QuestManager.Instance.CompleteQuest();
            }

            Description = $"{_curretItemsToCollect} /{totalItemsToCollect} Makanan DiAmbil";
            IsDirty = true;
        }
        
        public override void CompleteQuest()
        {
            base.CompleteQuest();
            foreach (var item in collectableItemsList) {
                item.OnInteract.RemoveListener(CollectItem);
            }
        }


    }
}