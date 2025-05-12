namespace Script.Quest
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public class QuestHolder : MonoBehaviour
    {
        [SerializeField] private List<Quest> quests = new List<Quest>();

        public List<Quest> Quests => quests;

        private void Start()
        {
            InitializeQuests();
            InitializeQuestsManager();
        }
        private void InitializeQuestsManager()
        {
            QuestManager.Instance.InitQuest(this);
        }
        private void InitializeQuests()
        {
            //get all quests  in child
            foreach (Transform child in transform) {
                Quest quest = child.GetComponent<Quest>();
                if (quest != null) {
                    quests.Add(quest);
                }
            }
        }

    }
}