namespace Script.Ui
{
    using Quest;
    using System;
    using TMPro;
    using UnityEngine;
    [DefaultExecutionOrder(-1)]
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleQuest;
        [SerializeField] private TextMeshProUGUI _descriptionQuest;
        public static UiManager Instance { get; private set; }

        private QuestManager _questManager;

        private Quest currentQuest;
        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }

            _questManager = QuestManager.Instance;
            _questManager.OnQuestStart += InitNewTrackQuest;

        }

        private void InitNewTrackQue*st()
        {
            currentQuest = _questManager.CurrentQuest;
            if (currentQuest != null) {
                _titleQuest.text = currentQuest.QuestName;
                _descriptionQuest.text = currentQuest.Description;
            } else {
                _titleQuest.text = "Congratulation";
                _descriptionQuest.text = "All quest completed";
            }
        }

        private void Update()
        {
            if (currentQuest != null) {
                if (!currentQuest.IsDirty) return;
                _titleQuest.text = currentQuest.QuestName;
                _descriptionQuest.text = currentQuest.Description;
            }
        }





    }
}