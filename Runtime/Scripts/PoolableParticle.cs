using UnityEngine;

namespace CozyDragon.PoolSystem
{
    public class PoolableParticle : PoolableObject
    {
        [SerializeField] private ParticleSystemStopBehavior _stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear;

        public ParticleSystem System { get; private set; }
        public ParticleSystemRenderer Renderer { get; private set; }

        private void Awake()
        {
            System = GetComponent<ParticleSystem>();
            Renderer = System.GetComponent<ParticleSystemRenderer>();
            var main = System.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void EmitAt(Vector3 position, Color color)
        {
            transform.position = position;
            var main = System.main;
            main.startColor = color;
            System.Play();
        }

        public override void ReturnInPool()
        {
            System.Stop(true, _stopBehavior);
            base.ReturnInPool();
        }

        private void OnParticleSystemStopped() => ReturnInPool();
    }
}