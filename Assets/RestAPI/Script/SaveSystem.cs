namespace RestAPI.Script
{
    using System;
    using System.Collections;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Networking;

    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }

        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private string getBestURL = "https://yourserver.com/api/get_best_score.php";
        [SerializeField] private string postURL    = "https://yourserver.com/api/save_score.php";
        [SerializeField] private string _secretKey = "pens";

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        // Panggil ini
        public void SaveHighScore()
        {
            if (scoreManager == null) { Debug.LogError("SaveSystem: scoreManager is null."); return; }
            StartCoroutine(SaveHighScoreSmart());
        }

        private IEnumerator SaveHighScoreSmart()
        {
            string username = UserManager.Instance.playerName;
            int localScore  = Mathf.FloorToInt(scoreManager.score);

            // 1) GET best score di server
            string hashGet = GetMD5(username + _secretKey);
            string url = $"{getBestURL}?username={UnityWebRequest.EscapeURL(username)}&hash={hashGet}";

            int serverBest = -1;

            using (var get = UnityWebRequest.Get(url))
            {
                yield return get.SendWebRequest();

                if (get.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning($"[SaveSystem] GET best failed: {get.error}. Tetap coba POST kondisional.");
                }
                else
                {
                    string json = get.downloadHandler.text;
                    // Expect: {"ok":true,"score":1234} atau angka plain
                    if (!TryParseBestScore(json, out serverBest))
                        Debug.LogWarning("[SaveSystem] Parse best score gagal, lanjut POST kondisional.");
                }
            }

            // 2) Bandingkan
            if (serverBest >= 0 && localScore <= serverBest)
            {
                Debug.Log($"[SaveSystem] Skip update: local({localScore}) <= server({serverBest})");
                yield break;
            }

            // 3) POST (akan tetap divalidasi server juga)
            yield return PostScore(username, localScore);
        }

        private IEnumerator PostScore(string username, int score)
        {
            string rawData = username + score + _secretKey;
            string hashedKey = GetMD5(rawData);

            var form = new WWWForm();
            form.AddField("username", username);
            form.AddField("score", score);
            form.AddField("hash", hashedKey);

            using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    Debug.Log("Server response: " + www.downloadHandler.text);
                else
                    Debug.LogError("SaveSystem Error: " + www.error);
            }
        }

        private bool TryParseBestScore(string json, out int best)
        {
            best = 0;
            try
            {
                // Coba parse JSON bentuk {"ok":true,"score":123}
                var resp = JsonUtility.FromJson<BestResp>(json);
                if (resp != null && resp.ok) { best = resp.score; return true; }
            }
            catch { /* ignore */ }

            // fallback: angka plain
            return int.TryParse(json, out best);
        }

        [Serializable] private class BestResp { public bool ok; public int score; }

        private string GetMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] b = Encoding.UTF8.GetBytes(input);
                byte[] h = md5.ComputeHash(b);
                var sb = new StringBuilder();
                foreach (var x in h) sb.Append(x.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
