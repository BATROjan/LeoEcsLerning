using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;        
        IEcsSystems _systems;
        public Prefabs Prefabs;
        
        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world, Prefabs);
            _systems
                // register your systems here, for example:
                 .Add (new BushInitialize ())
                 //.Add (new TestSystem2 ())
                
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Init ();
        }

        void Update () 
        {
            
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }

    internal class BushInitialize : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Prefabs pref = systems.GetShared<Prefabs>();
            for (int i = 0; i < 1; i++)
            {
                Bush bush = GameObject.Instantiate(pref.Bush);
            }
        }
    }
}