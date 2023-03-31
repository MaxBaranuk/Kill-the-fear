namespace Systems.Interfaces
{
    public interface ISpawnSystem
    {
        public void SaveBiomes();
        public void SpawnBiomes();
        public void SmoothBiomesTransitions();
        public void SpawnEnemies();
    }
}