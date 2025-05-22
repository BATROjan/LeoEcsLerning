using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Player
{
    public class PlayerInitializeSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Prefabs pref = systems.GetShared<Prefabs>();
            EcsWorld world = systems.GetWorld();
            EcsPool<PlayerComponent> pool = world.GetPool<PlayerComponent>();
            
            int entity = world.NewEntity();
            ref PlayerComponent transformComponent = ref pool.Add (entity);
            Player player = GameObject.Instantiate(pref.Player);
            transformComponent.Transform = player.transform;
        }
    }
}