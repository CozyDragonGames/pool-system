namespace KaynirGames.Pool
{
    public interface IPoolable<T>
    {
        void SetPool(IObjectPool<T> pool);
        void ReturnInPool();
    }
}