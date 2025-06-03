using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Bush
{
    public class BushInitSystem :IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            BushConfig bushConfig = Resources.Load<BushConfig>("BushConfig");
            Prefabs pref = systems.GetShared<Prefabs>();
            EcsWorld world = systems.GetWorld();
            EcsPool<BushComponent> pool = world.GetPool<BushComponent>();
            
            for (int i = 0; i < 4; i++)
            {
                int entity = world.NewEntity();
                ref BushComponent BushComponent = ref pool.Add (entity);
                Bush bush = GameObject.Instantiate(pref.Bush);
                bush.transform.position = bushConfig.GetModel(i).position;;
            }
        }
    }
}