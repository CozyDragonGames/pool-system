using UnityEngine;

namespace CozyDragon.PoolSystem
{
    public class PoolableObject : MonoBehaviour
    {
        private ObjectPool _objectPool;

        public void SetObjectPool(ObjectPool objectPool) => _objectPool = objectPool;

        public virtual void ReturnInPool() => _objectPool.Release(this);
    }
}