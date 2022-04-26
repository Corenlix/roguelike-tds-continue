namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SetValue<T>(T client, string key);
        T GetValue<T>(string key, T defaultValue);
        void Save();
        void Load();
        void Clear();
    }
}