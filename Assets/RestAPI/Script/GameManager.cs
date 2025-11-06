using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public float score;
    public int coins;
    public string sceneName;
    public float[] playerPosition;
}

public static class GameSaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }

    public static GameData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        Debug.LogWarning("No save file found.");
        return null;
    }

    public static bool HasSave()
    {
        return File.Exists(savePath);
    }

    public static void DeleteSave()
    {
        if (File.Exists(savePath)) File.Delete(savePath);
    }
}
