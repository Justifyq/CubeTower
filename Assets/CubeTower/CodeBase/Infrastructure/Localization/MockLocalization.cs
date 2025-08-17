namespace CubeTower.Infrastructure.Localization
{
    public class MockLocalization : ILocalization
    {
        public string GetString(string key) => key;
    }
}