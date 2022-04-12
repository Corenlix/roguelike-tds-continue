namespace Infrastructure
{
    public interface IStaticDataService
    {
        public EnemyStaticData ForEnemy(EnemyId id);
    }
}