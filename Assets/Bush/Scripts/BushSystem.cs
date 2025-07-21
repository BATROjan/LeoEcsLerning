using Client.Coins;
using Client.Point;
using Client.Point.Scripts;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Bush
{
    public class BushSystem :IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _bushFilter; 
        private DOTween tween;
        private float timer = 5;
        private EcsWorld _world = new EcsWorld();
        
        private BushPointConfig _bushPointConfig;
        private BushConfig _bushConfig;
        private PointConfig _pointConfig;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _bushConfig = Resources.Load<BushConfig>("BushConfig");
            _bushPointConfig = Resources.Load<BushPointConfig>("BushPointConfig");
            _pointConfig = Resources.Load<PointConfig>("PointConfig");
            
            Prefabs pref = systems.GetShared<Prefabs>();

            EcsPool<BushComponent> bushPool = _world.GetPool<BushComponent>();
            
            EcsPool<BushPointComponent> bushPointComponent = _world.GetPool<BushPointComponent>();
            
            EcsPool<PointComponent> pointPool = _world.GetPool<PointComponent>();
            EcsFilter pointFilter = _world.Filter<PointComponent>().End();
            
            for (int i = 0; i < 4; i++)
            {
                int entity = _world.NewEntity();
                ref BushPointComponent BushPointComponent = ref bushPointComponent.Add (entity);
                BushPoint bushPoint = GameObject.Instantiate(pref.BushPoint);
                bushPoint.transform.position = _bushPointConfig.GetModelByID(i).position;
                BushPointComponent.Type = _bushPointConfig.GetModelByID(i).Type;
                
                entity = _world.NewEntity();
                ref BushComponent BushComponent = ref bushPool.Add (entity);
                Bush bush = GameObject.Instantiate(_bushConfig.GetModelByType(BushPointComponent.Type).BushPrefab);
                bush.transform.SetParent(bushPoint.transform, false);

                BushPointComponent.Bush = bush;
                
                BushComponent.AddScoreCount = _bushConfig.GetModelByType(BushPointComponent.Type).AddScoreCount;
                BushComponent.OnAddScore += AddScore;
                BushComponent.Type = BushPointComponent.Type;
                
                int pointEntity = _world.NewEntity();
                ref PointComponent pointComponent = ref pointPool.Add (pointEntity);
                Point.Scripts.Point point = GameObject.Instantiate(pref.Point);
                point.transform.position = _pointConfig.GetModel(i).position;
                pointComponent.Type = _pointConfig.GetModel(i).Type;
                pointComponent.Transform = point.transform;
                pointComponent.BushEntityID = entity;
            }
        }

        public void Run(IEcsSystems systems)
        {
            _bushFilter = _world.Filter<BushComponent>().End();
            EcsPool<BushComponent> pool = _world.GetPool<BushComponent> (); 
            
            foreach (var entity in _bushFilter)
            {
                ref BushComponent bushComponent = ref pool.Get(entity);
                if (bushComponent.IsReadyToAddScore)
                {
                    bushComponent.TimeToAddScore -= Time.deltaTime;
                    // Debug.Log(bushComponent.TimeToAddScore);
                    if (bushComponent.TimeToAddScore <=0)
                    {
                        bushComponent.OnAddScore?.Invoke(bushComponent);
                        bushComponent.TimeToAddScore = _bushConfig.GetModelByType(bushComponent.Type).TimeToAddScore;
                    }
                }
            }
        }

        public void AddScore(BushComponent bushComponent)
        {
             ref  CoinStorage storage = ref _world.GetPool<CoinStorage>().GetRawDenseItems()[0];
            storage.CoinCount += bushComponent.AddScoreCount;
            Debug.Log("Add " + bushComponent.AddScoreCount + " scores");
        }
    }}