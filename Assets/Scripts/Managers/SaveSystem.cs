using Newtonsoft.Json;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    //[SerializeField] private SaveData _data;
    [SerializeField] private SaveData _localPlayerData;
    private const string SAVE_DATA_KEY = "PlayerData";

    //TODO save by script
    [ContextMenu("Save")]
    public void Save(SaveData data)
    {
        var playerData = JsonConvert.SerializeObject(data);
        Debug.Log(playerData);
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }

    //TODO Load
    [ContextMenu("Load")]
    public SaveData Load()
    {
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            var jsonToConvert = PlayerPrefs.GetString(SAVE_DATA_KEY);
            _localPlayerData = JsonConvert.DeserializeObject<SaveData>(jsonToConvert);
        }
        else
        {
            var playerData = new SaveData();
            _localPlayerData = playerData;
            var data = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(SAVE_DATA_KEY, data);
        }
        return _localPlayerData;
    }
}
