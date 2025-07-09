using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Bush
{
    public class BushInitSystem :IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            BushConfig bushConfig = Resources.Load<BushConfig>("BushConfig");
            BushPointConfig bushPointConfig = Resources.Load<BushPointConfig>("BushPointConfig");
            Prefabs pref = systems.GetShared<Prefabs>();
            EcsWorld world = systems.GetWorld();
            EcsPool<BushPointComponent> pointPool = world.GetPool<BushPointComponent>();
            EcsPool<BushComponent> bushPool = world.GetPool<BushComponent>();
            
            for (int i = 0; i < 4; i++)
            {
                int entity = world.NewEntity();
                ref BushPointComponent BushPointComponent = ref pointPool.Add (entity);
                BushPoint bushPoint = GameObject.Instantiate(pref.BushPoint);
                bushPoint.transform.position = bushPointConfig.GetModel(i).position;
                BushPointComponent.Type = bushPointConfig.GetModel(i).Type;
                
                entity = world.NewEntity();
                ref BushComponent BushComponent = ref bushPool.Add (entity);
                Bush bush = GameObject.Instantiate(bushConfig.GetModelByType(BushPointComponent.Type).BushPrefab);
                bush.transform.SetParent(bushPoint.transform, false);
            }
        }
    }
}