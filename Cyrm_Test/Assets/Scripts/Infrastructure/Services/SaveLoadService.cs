using Data;
using Data.Extensions;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        
        public SaveLoadService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }
        
        public void SaveProgress()
        {
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public GameProgress LoadProgress()
        {
           return PlayerPrefs.GetString(ProgressKey)?.ToDeserialize<GameProgress>();
        }

        public void DeleteProgress()
        {
            PlayerPrefs.DeleteKey(ProgressKey);
        }
    }
}