using System.Collections.Generic;
using UnityEngine;

namespace CozyDragon.PoolSystem
{
    public class MonoPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] protected T _prefab = null;

        protected Queue<T> _objects;

        protected virtual void Awake() => _objects = new Queue<T>();

        public void SetPrefab(T prefab) => _prefab = prefab;

        public virtual T Take()
        {
            T obj = _objects.Count > 0
                ? _objects.Dequeue()
                : Create();

            obj.gameObject.SetActive(true);

            return obj;
        }

        public virtual void Return(T obj)
        {
            obj.gameObject.SetActive(false);

            _objects.Enqueue(obj);
        }

        public virtual void Clear()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                Destroy(_objects.Dequeue().gameObject);
            }
        }

        protected virtual T Create()
        {
            T obj = Instantiate(_prefab, transform);
            obj.SetPool(this);

            return obj;
        }
    }
}