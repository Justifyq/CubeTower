using System.Collections.Generic;

namespace CubeTower.Data.Configs.Interfaces
{
    public interface ICubesData
    {
        IEnumerable<Cube> Data { get; }

        Cube Get(string name);
    }
}