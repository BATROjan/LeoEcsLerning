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
        private EcsPool<PointComponent> _targetPool;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            // Обновляем фильтры
            _playerFilter = world.Filter<PlayerComponent>().End();

            // Получаем пулы компонентов
            _transformPool = world.GetPool<PlayerComponent>();
            _targetPool = world.GetPool<PointComponent>();

            foreach (var entity in _playerFilter)
            {
                // Проверяем, есть ли у сущности цель
                if (_targetPool.Has(entity))
                {
                    Debug.Log("AAAAAAAAA");
                    var targetTransform = _targetPool.Get(entity).Transform;
                    var transformComponent = _transformPool.Get(entity);
                    var playerTransform = transformComponent.Transform;

                    // Расстояние до цели
                    float distance = Vector3.Distance(playerTransform.position, targetTransform.position);
                }
            }
        }
    }
}