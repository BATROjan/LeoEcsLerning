using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Point.Scripts
{
    public class PointInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            PointConfig pointConfig = Resources.Load<PointConfig>("PointConfig");
            Prefabs pref = systems.GetShared<Prefabs>();
            EcsWorld world = systems.GetWorld();
            EcsPool<PointComponent> pool = world.GetPool<PointComponent>();
            for (int i = 0; i < 4; i++)
            {
                int entity = world.NewEntity();
                ref PointComponent pointComponent = ref pool.Add (entity);
                Point point = GameObject.Instantiate(pref.Point);
                point.transform.position = pointConfig.GetModel(i).position;
                pointComponent.Type = pointConfig.GetModel(i).Type;
                pointComponent.Transform = point.transform;
            }
        }
    }
}