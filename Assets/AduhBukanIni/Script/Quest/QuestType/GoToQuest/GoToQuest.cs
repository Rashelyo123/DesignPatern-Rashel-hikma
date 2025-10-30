namespace Script.Quest.QuestType.GoToQuest
{
    using UnityEngine;
    public class GoToQuest : Quest
    {
        [SerializeField] private GoToQuestInteract goToQuestInteract;
        public override void StartQuest()
        {
            base.StartQuest();
            goToQuestInteract.OnInteract.AddListener(OnPlayerTriggerEnter);
        }
        private void OnPlayerTriggerEnter()
        {
            QuestManager.Instance.CompleteQuest();
        }

        public override void CompleteQuest()
        {
            base.CompleteQuest();
            goToQuestInteract.OnInteract.RemoveListener(OnPlayerTriggerEnter);
            goToQuestInteract.CanInteract = false;
            goToQuestInteract.gameObject.SetActive(false);
        }

    }
}