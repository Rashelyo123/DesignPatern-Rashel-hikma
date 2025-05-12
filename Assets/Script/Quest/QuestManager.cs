namespace Script.Quest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder(-100)]
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private List<Quest> quests = new List<Quest>();
        [SerializeField] private AudioClip _onMissionComplete;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _AllQuestComplete;


        public Action OnQuestStart;
        public Action OnQuestComplete;

        private Quest currentQuest;
        public static QuestManager Instance { get; private set; }
        public Quest CurrentQuest => currentQuest;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public void InitQuest(QuestHolder questHolder)
        {
            quests = questHolder.Quests;
            currentQuest = questHolder.Quests[0];

            StartQuest();

        }

        private void StartQuest()
        {
            if (currentQuest != null) {
                currentQuest.StartQuest();
                OnQuestStart?.Invoke();
            }
        }

        public void CompleteQuest()
        {
            if (currentQuest != null) {
                currentQuest.CompleteQuest();
                OnQuestComplete?.Invoke();
            }


            int currentIndex = quests.IndexOf(currentQuest);
            if (currentIndex < quests.Count - 1) {
                currentQuest = quests[currentIndex + 1];
                StartQuest();
                _audioSource.PlayOneShot(_onMissionComplete);
            } else {
                currentQuest = null;
                OnQuestStart?.Invoke();
                _audioSource.PlayOneShot(_AllQuestComplete);
            }

        }










    }
}