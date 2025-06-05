using Client.InputSystem;
using Client.Point.Scripts;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter _playerFilter; 
        private EcsPool<InputComponent> _targetPool;
        private Transform target;
        private DOTween tween;
        private Animator anim;
        
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            _playerFilter = world.Filter<PlayerComponent>().End();
            _targetPool = world.GetPool<InputComponent>();
            
            if (_targetPool.Get(0).TargetTransform != null)//убрать 0 и сделать нормальный id
            {
                foreach (var entity in _playerFilter)
                {
                    ref var transformComp = ref world.GetPool<PlayerComponent>().Get(entity);
                    Quaternion targetRotation = Quaternion.LookRotation(_targetPool.Get(0).TargetTransform.position);
                    transformComp.Transform.rotation = Quaternion.Slerp(transformComp.Transform.rotation,
                        targetRotation,
                        5 * Time.deltaTime);
                    Vector3 directionToTarget = _targetPool.Get(0).TargetTransform.position - transformComp.Transform.position;
                    float angle = Vector3.Angle(transformComp.Transform.forward, directionToTarget);
                    Debug.Log(angle);

                    if (angle <= 2)
                    {
                        Vector3 direction = (_targetPool.Get(0).TargetTransform.position - transformComp.Transform.position).normalized;
                        transformComp.Rigidbody.MovePosition(transformComp.Transform.position + direction * 1 * Time.deltaTime);
                        Debug.Log((_targetPool.Get(0).TargetTransform.position - transformComp.Transform.position).magnitude);

                        if ((_targetPool.Get(0).TargetTransform.position - transformComp.Transform.position).magnitude < 0.1f)
                        {
                            _targetPool.Get(0).TargetTransform = null; 
                            Debug.Log("AAAAAAAAAAAA");
                        }
                    }
                }
            }
        }
    }
}