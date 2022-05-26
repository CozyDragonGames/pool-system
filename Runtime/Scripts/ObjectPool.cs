using System.Collections.Generic;
using UnityEngine;

namespace CozyDragon.PoolSystem
{
    public class ObjectPool
    {
        private PoolableObject _prefab;
        private Transform _poolHolder;

        private Queue<PoolableObject> _objects;

        public ObjectPool(Transform parent, PoolableObject prefab)
        {
            _prefab = prefab;
            _objects = new Queue<PoolableObject>();
            _poolHolder = new GameObject($"{prefab.name}Pool").transform;
            _poolHolder.SetParent(parent);
        }

        public PoolableObject Take()
        {
            PoolableObject obj = _objects.Count > 0
                ? _objects.Dequeue()
                : CreateObject();

            obj.gameObject.SetActive(true);

            return obj;
        }

        public void Release(PoolableObject obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }

        private PoolableObject CreateObject()
        {
            PoolableObject obj = Object.Instantiate(_prefab, _poolHolder);
            obj.SetObjectPool(this);

            return obj;
        }
    }
}