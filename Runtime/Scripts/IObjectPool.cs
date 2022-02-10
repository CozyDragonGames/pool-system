namespace KaynirGames.Pool
{
    public interface IObjectPool<T>
    {
        T Take();
        void Return(T obj);
        void Clear();
    }
}