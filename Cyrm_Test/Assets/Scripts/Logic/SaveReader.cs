using System;
using Infrastructure.Services;
using Model;
using UnityEngine;

namespace Logic
{
    public class SaveReader : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _persistentProgressService;
        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        private void OnEnable()
        {
            _persistentProgressService.Progress.WorldModel.LootModel.OnChanged += Save;
            _persistentProgressService.Progress.WorldModel.LootModel.OnAdd += Save;
            _persistentProgressService.Progress.WorldModel.LootModel.OnRemove += Save;
            _persistentProgressService.Progress.WorldModel.CoinModel.Changed += Save;
        }
        
        private void OnDisable()
        {
            _persistentProgressService.Progress.WorldModel.LootModel.OnChanged -= Save;
            _persistentProgressService.Progress.WorldModel.LootModel.OnAdd -= Save;
            _persistentProgressService.Progress.WorldModel.LootModel.OnRemove -= Save;
            _persistentProgressService.Progress.WorldModel.CoinModel.Changed -= Save;
        }
        
        private void Save()
        {
            _saveLoadService.SaveProgress();
        }
        
        private void Save(Resource obj)
        {
            _saveLoadService.SaveProgress();
        }
    }
}
