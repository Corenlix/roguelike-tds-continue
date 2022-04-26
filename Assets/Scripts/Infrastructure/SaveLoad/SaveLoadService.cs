using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public void SetValue<T>(T client, string key)
        {
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(client));
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            return PlayerPrefs.HasKey(key) ? JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key)) : defaultValue;
        }
    
        public void Save()
        {
            PlayerPrefs.Save();
        }

        public void Load()
        {
        
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}