namespace CubeTower.Data.Configs.Interfaces
{
    public interface IConfiguration
    {
        IMapViewConfiguration Map { get; }
        ICubesData Cubes { get; }
        IUIConfiguration UI { get; }
        
    }
}