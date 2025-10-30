namespace RestAPI.Script
{
    using UnityEngine;
    using System;

    public class UserManager : MonoBehaviour
    {
        public string playerName = "Player";
        public static UserManager Instance { get; private set; }

        private const string PlayerNameKey = "player_name";

        private void Awake()
        {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadPlayerName();
        }

        private void LoadPlayerName()
        {
            if (PlayerPrefs.HasKey(PlayerNameKey))
            {
                // Ambil nama yang pernah disimpan
                playerName = PlayerPrefs.GetString(PlayerNameKey);
                Debug.Log($"[UserManager] Loaded existing player name: {playerName}");
            }
            else
            {
                // Generate random name kalau belum pernah disimpan
                playerName = GenerateRandomName();
                PlayerPrefs.SetString(PlayerNameKey, playerName);
                PlayerPrefs.Save();
                Debug.Log($"[UserManager] Generated new player name: {playerName}");
            }
        }

        private string GenerateRandomName()
        {
            // Contoh: Player1234 (4 digit random)
            int randomNumber = UnityEngine.Random.Range(1000, 9999);
            return $"Player{randomNumber}";
        }
    }
}