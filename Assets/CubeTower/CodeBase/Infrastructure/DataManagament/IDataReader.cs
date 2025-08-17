using System.Threading;
using CubeTower.Data;
using Cysharp.Threading.Tasks;

namespace CubeTower.Infrastructure.DataManagament
{
    public interface IDataReader
    {
        UniTask<SavedData> Read(CancellationToken token );
    }
}