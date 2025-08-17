namespace CubeTower.Data
{
    public class CubeViewModel
    {
        public CubeViewModel(Cube cube)
        {
            Name = cube.name;
            Placed = false;
            PosX = 0;
            GroundIndex = -1;
        }


        public CubeViewModel()
        {
            Name = string.Empty;
            Placed = false;
            PosX = 0;
            GroundIndex = 0;
        }
        
        public string Name { get; set; }
        
        public int GroundIndex { get; set; }

        public float PosX { get; set; }
        
        public bool Placed { get; set; }
        
    }
}