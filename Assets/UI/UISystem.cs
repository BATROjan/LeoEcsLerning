using Client.Coins;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class UISystem: IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
           EcsWorld world = systems.GetWorld();
           Prefabs pref = systems.GetShared<Prefabs>();
           EcsFilter filter = world.Filter<CoinStorage>().End();
          ref  CoinStorage storage = ref world.GetPool<CoinStorage>().GetRawDenseItems()[0];
           
           GameUI ui = GameObject.Instantiate(pref.GameUI);
           EcsPool<GameUIComponent> pool = world.GetPool<GameUIComponent>();
           int entity = world.NewEntity();
           GameUIComponent gameUIComponent = pool.Add (entity);
           gameUIComponent.CoinText = ui.GetComponentInChildren<Text>();
           storage.OnAddCoin += (coins) =>
           {
               gameUIComponent.CoinText.text = $"Score {coins}";
           };
        }
    }
}