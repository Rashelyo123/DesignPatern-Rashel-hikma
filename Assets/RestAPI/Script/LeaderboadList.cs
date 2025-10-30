namespace RestAPI.Script
{
    using TMPro;
    using UnityEngine;
    public class LeaderboadList : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _leaderboardText;
        
        public void SetScoreboardText(string text)
        {
            _leaderboardText.text = text;
        }
        
    }
}