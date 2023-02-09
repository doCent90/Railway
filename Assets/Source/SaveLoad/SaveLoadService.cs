using Newtonsoft.Json;
using UnityEngine;

namespace Source.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string GameData = "GameData";

        private readonly IProgressService _progressService;

        public SaveLoadService(IProgressService progressService) =>
            _progressService = progressService;

        public void Save() =>
            PlayerPrefs.SetString(GameData, JsonConvert.SerializeObject(_progressService.GameData));

        public GameData Load()
        {
            if (PlayerPrefs.HasKey(GameData))
                return JsonConvert.DeserializeObject<GameData>(PlayerPrefs.GetString(GameData));

            return null;
        }
    }
}
