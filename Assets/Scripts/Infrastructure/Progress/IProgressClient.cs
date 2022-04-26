using Infrastructure.SaveLoad;

namespace Infrastructure.Progress
{
    public interface IProgressClient
    {
        void Save(ISaveLoadService saveLoadService);
        void Load(ISaveLoadService saveLoadService);
    }
}