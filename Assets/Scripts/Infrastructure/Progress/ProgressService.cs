using System.Collections.Generic;
using Infrastructure.SaveLoad;

namespace Infrastructure.Progress
{
    public class ProgressService : IProgressService
    {
        private readonly List<IProgressClient> _progressClients = new List<IProgressClient>();
        private readonly ISaveLoadService _saveLoadService;

        public ProgressService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        
        public void AddClient(IProgressClient progressClient)
        {
            _progressClients.Add(progressClient);
        }

        public void Save()
        {
            _progressClients.ForEach(x=>x.Save(_saveLoadService));
        }

        public void Load()
        {
            _progressClients.ForEach(x=>x.Load(_saveLoadService));
        }
    }
}