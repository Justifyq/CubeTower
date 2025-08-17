using System.IO;
using System.Threading;
using CubeTower.Data;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace CubeTower.Infrastructure.DataManagament
{
    public class DataHandler : IDataReader, IDataWriter
    {
        private const string FileName = "data.json";
        
        private readonly string _dataPath = Path.Combine(Application.persistentDataPath, FileName);

        public async UniTask<SavedData> Read(CancellationToken token)
        {
            if (!File.Exists(_dataPath))
                return SavedData.Default();

            string result = await File.ReadAllTextAsync(_dataPath, token);
            
            return string.IsNullOrWhiteSpace(result) ? SavedData.Default() : JsonConvert.DeserializeObject<SavedData>(result);
        }

        public async UniTask Write(SavedData savedData, CancellationToken token = default) => 
            await File.WriteAllTextAsync(_dataPath, JsonConvert.SerializeObject(savedData), token);
    }
}