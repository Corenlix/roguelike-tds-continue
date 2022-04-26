namespace Infrastructure.Progress
{
    public interface IProgressService : IService
    {
        void AddClient(IProgressClient progressClient);
        void Save();
        void Load();
    }
}