namespace Script.Quest
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    public class Quest : MonoBehaviour
    {
        [SerializeField] private string _questName = "QuestName";

        [TextArea(3, 5)] [SerializeField] private string _description = "Description";


        public UnityEvent OnQuestStartEvent;
        public UnityEvent OnQuestCompleteEvent;

        public string QuestName { get => _questName; set => _questName = value; }
        public string Description { get => _description; set => _description = value; }
        public bool IsDirty = true;


        //buat onvalide nama gameobjek sama dengan _questname
        private void OnValidate()
        {
            if (gameObject.name != _questName) {
                gameObject.name = _questName;
            }
        }

        public virtual void StartQuest()
        {
            IsDirty = true;
            OnQuestStartEvent?.Invoke();
        }

        public virtual void CompleteQuest()
        {
            IsDirty = false;
            OnQuestCompleteEvent?.Invoke();
        }


    }
}