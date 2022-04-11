using System;
using System.Collections.Generic;

namespace CozyDragon.PoolSystem
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private Queue<T> _objects;

        private Func<T> _createFunc;
        private Action<T> _takeAction;
        private Action<T> _returnAction;
        private Action<T> _destroyAction;

        public ObjectPool(
            Func<T> createFunc,
            Action<T> takeAction = null,
            Action<T> returnAction = null,
            Action<T> destroyAction = null)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _takeAction = takeAction;
            _returnAction = returnAction;
            _destroyAction = destroyAction;
            _objects = new Queue<T>();
        }

        public T Take()
        {
            T obj = _objects.Count > 0
                ? _objects.Dequeue()
                : _createFunc();

            _takeAction?.Invoke(obj);

            return obj;
        }

        public void Return(T obj)
        {
            _returnAction?.Invoke(obj);
            _objects.Enqueue(obj);
        }

        public void Clear()
        {
            if (_destroyAction != null)
            {
                for (int i = 0; i < _objects.Count; i++)
                {
                    _destroyAction.Invoke(_objects.Dequeue());
                }
            }

            _objects.Clear();
        }
    }
}