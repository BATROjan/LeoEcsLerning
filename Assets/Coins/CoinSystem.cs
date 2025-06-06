using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Coins
{
    public class CoinSystem: IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsPool<CoinStorage> pool = world.GetPool<CoinStorage>();
            int entity = world.NewEntity();
            pool.Add (entity);
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            ref CoinStorage storage = ref world.GetPool<CoinStorage>().GetRawDenseItems()[0];
            if (Input.GetMouseButtonDown(0))
            {
                storage.CoinCount++;
            }
        }
    }
}