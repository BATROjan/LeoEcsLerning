using Client.InputSystem;
using Client.Point.Scripts;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter _playerFilter; 
        private EcsPool<InputComponent> _targetPool;
        private Transform target;
        
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            _playerFilter = world.Filter<PlayerComponent>().End();
            _targetPool = world.GetPool<InputComponent>();
            
            if (target != _targetPool.Get(0).TargetTransform)//убрать 0 и сделать нормальный id
            {
                foreach (var entity in _playerFilter)
                {
                    ref var transformComp = ref world.GetPool<PlayerComponent>().Get(entity);
                    transformComp.Transform.position = _targetPool.Get(0).TargetTransform.position;//убрать 0 и сделать нормальный id
                }
            }
        }
    }
}