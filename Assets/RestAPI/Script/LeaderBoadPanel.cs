namespace RestAPI.Script
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    public class LeaderBoadPanel : MonoBehaviour
    {
        [SerializeField] private LeaderboadList _leaderboadListprepabs;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private string _url; // ex: https://domain/get_leaderboard.php
        [SerializeField] private string _secretKey; // ex: indo123
        [SerializeField] private Transform _targetContent;
        [SerializeField] private float _refreshInterval = 3f; // tiap 3 detik

        private readonly List<GameObject> _cachedEntries = new List<GameObject>();
        private Coroutine _running;

        private void OnEnable()
        {
            // Jalankan auto refresh loop
            _running = StartCoroutine(AutoRefreshLoop());

            // Tambah tombol manual (opsional)
            if (_refreshButton != null) {
                _refreshButton.onClick.RemoveAllListeners();
                _refreshButton.onClick.AddListener(() =>
                {
                    StartCoroutine(FetchLeaderboard(10, OnResult));
                });
            }
        }

        private void OnDisable()
        {
            if (_running != null)
                StopCoroutine(_running);
        }

        /// <summary>
        /// Loop yang otomatis refresh leaderboard tiap interval.
        /// </summary>
        private IEnumerator AutoRefreshLoop()
        {
            while (true) {
                yield return FetchLeaderboard(10, OnResult);
                yield return new WaitForSeconds(_refreshInterval);
            }
        }

        public IEnumerator FetchLeaderboard(int limit, Action<LeaderboardEntry[]> onResult)
        {
            string hash = GetMD5(limit.ToString() + _secretKey);
            string url = $"{_url}?limit={limit}&hash={hash}";

            using (UnityWebRequest www = UnityWebRequest.Get(url)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.LogError($"Leaderboard error: {www.error}");
                    onResult?.Invoke(Array.Empty<LeaderboardEntry>());
                    yield break;
                }

                string json = www.downloadHandler.text;

                try {
                    var resp = JsonUtility.FromJson<LeaderboardResponse>(FixJson(json));
                    if (resp != null && resp.ok && resp.data != null) {
                        onResult?.Invoke(resp.data);
                    } else {
                        onResult?.Invoke(Array.Empty<LeaderboardEntry>());
                    }
                }
                catch (Exception ex) {
                    Debug.LogError("Parse JSON failed: " + ex.Message);
                    onResult?.Invoke(Array.Empty<LeaderboardEntry>());
                }
            }
        }

        private void OnResult(LeaderboardEntry[] data)
        {
            RefreshUi(data);
        }

        private void RefreshUi(LeaderboardEntry[] respData)
        {
            // Bersihkan yang lama
            foreach (var go in _cachedEntries)
                Destroy(go);
            _cachedEntries.Clear();

            if (respData == null || respData.Length == 0) return;

            // Spawn baru
            foreach (var leaderboard in respData) {
                var entry = Instantiate(_leaderboadListprepabs, _targetContent);
                string usernameDisplay = leaderboard.username;
                if (leaderboard.username == UserManager.Instance.playerName)
                    usernameDisplay += " (You)";
                entry.SetScoreboardText($"{usernameDisplay} : {leaderboard.score}");
                _cachedEntries.Add(entry.gameObject);
            }
        }

        [Serializable]
        public class LeaderboardResponse
        {
            public bool ok;
            public LeaderboardEntry[] data;
        }

        [Serializable]
        public class LeaderboardEntry
        {
            public string username;
            public int score;
        }

        private string GetMD5(string input)
        {
            using (var md5 = MD5.Create()) {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private string FixJson(string json) => json;
    }
}