using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public float x, y, z;
}

public class PlayerSaveLoad : MonoBehaviour
{
    private string filePath;
    private Transform playerTransform;

    void Start()
    {
        filePath = Application.persistentDataPath + "/player_position.json";
        playerTransform = this.transform;

        LoadPlayerPosition();
    }

    void Update()
    {
        SavePlayerPosition();
        LoadPlayerPosition();
    }

    public void SavePlayerPosition()
    {
        PlayerData data = new PlayerData();
        data.x = playerTransform.position.x;
        data.y = playerTransform.position.y;
        data.z = playerTransform.position.z;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Posisi disimpan ke: " + filePath);
        Debug.Log("Lokasi file: " + Application.persistentDataPath);

    }

    public void LoadPlayerPosition()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            Vector3 pos = new Vector3(data.x, data.y, data.z);
            playerTransform.position = pos;

            Debug.Log("Posisi dimuat: " + pos);
        }
        else
        {
            Debug.Log("File posisi tidak ditemukan.");
        }
    }
}