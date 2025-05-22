using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter _filter;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<PlayerComponent>().End();
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Finish")
                    {
                        foreach (var i in _filter)
                        {
                            ref var transformComp = ref world.GetPool<PlayerComponent>().Get(i);
                            transformComp.Transform.position = hit.collider.transform.position;
                        }

                        Debug.Log("Объект попал в луч: " + hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}