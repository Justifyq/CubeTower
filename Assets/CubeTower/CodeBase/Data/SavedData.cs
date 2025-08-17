using System;

namespace CubeTower.Data
{
    public class SavedData
    {
        public CubeViewModel[] Tower { get; set; }

        public static SavedData Default()
        {
            return new SavedData()
            {
                Tower = Array.Empty<CubeViewModel>()
            };
        }
    }
}