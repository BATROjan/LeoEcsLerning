using Client.InputSystem;
using Client.Point.Scripts;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter _playerFilter; // фильтр для игрока
        private EcsPool<PlayerComponent> _transformPool;
        private EcsPool<InputComponent> _targetPool;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            // Обновляем фильтры
            _playerFilter = world.Filter<PlayerComponent>().End();

            // Получаем пулы компонентов
            _transformPool = world.GetPool<PlayerComponent>();
            _targetPool = world.GetPool<InputComponent>();
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var entity in _playerFilter)
                {
                        Debug.Log("AAAAAAAAA");
                        var targetTransform = _targetPool.Get(0).TargetTransform;
                       // var transformComponent = _transformPool.Get(entity);
                        var playerTransform = targetTransform;

                        // Расстояние до цели
                       // float distance = Vector3.Distance(playerTransform.position, targetTransform.position);
                }
            }
        }
    }
}