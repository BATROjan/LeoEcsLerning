using System;
using Client.Bush;
using Client.Coins;
using Client.InputSystem;
using Client.Player;
using Client.Point.Scripts;
using Client.UI;
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
                 .Add (new KeyboardInputSystem())
                 .Add (new PlayerInitializeSystem())
                 .Add (new PlayerMovementSystem())
                 //.Add (new PointInitSystem())
                 .Add (new CoinSystem())
                 .Add (new UISystem())
                 //.Add (new PlayerMovementSystem()) ДОДЕЛАТЬ
                 .Add(new BushSystem())
                
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Init ();
            Destroy(Prefabs);//удаляем из оперативной памяти
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

   /* internal class BushInitialize : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Prefabs pref = systems.GetShared<Prefabs>();
            EcsWorld world = systems.GetWorld();
            EcsPool<BushStats> pool = world.GetPool<BushStats>();
            
            for (int i = 0; i < 1; i++)
            {
                int entity = world.NewEntity();
                ref BushStats stats = ref pool.Add(entity);
                Bush bush = GameObject.Instantiate(pref.Bush);
            }
        }
        public struct BushStats
        {
            public int PunchCost;
        }
    }
    public class BushPunch : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter filter;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            filter = world.Filter<Punch>().End();
        }

        public void Run(IEcsSystems systems)
        {

            }
            foreach (var entity in filter)
            {
                
            }
        }
    }

    public struct Punch
    {
    }*/
}