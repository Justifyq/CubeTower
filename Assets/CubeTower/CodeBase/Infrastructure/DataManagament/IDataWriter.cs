using System.Threading;
using CubeTower.Data;
using Cysharp.Threading.Tasks;

namespace CubeTower.Infrastructure.DataManagament
{
    public interface IDataWriter
    {
        UniTask Write(SavedData savedData, CancellationToken token);
    }
}