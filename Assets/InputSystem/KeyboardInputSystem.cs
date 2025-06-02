using Leopotam.EcsLite;
using UnityEngine;

namespace Client.InputSystem
{
    public class KeyboardInputSystem: IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            systems.GetShared<InputData>();
            EcsPool<InputComponent> pool = world.GetPool<InputComponent>();
            int entity = world.NewEntity();
            ref InputComponent transformComponent = ref pool.Add (entity);
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<InputComponent>().End();
            InputData inputData = systems.GetShared<InputData>();
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<Point.Scripts.Point>())
                    {
                        foreach (var i in _filter)
                        {
                            ref var transformComp = ref world.GetPool<InputComponent>().Get(i);
                            transformComp.TargetTransform = hit.collider.transform;
                        }
                        Debug.Log("Объект попал в луч: " + hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}