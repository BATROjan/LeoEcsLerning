using System;
using UnityEngine;

namespace Client.Bush
{
    [CreateAssetMenu(fileName = "BushPointConfig", menuName = "Configs/BushPointConfig")]

    public class BushPointConfig : ScriptableObject
    {
        [SerializeField] private BushPoinModel[] models;

        public BushPoinModel GetModelByID(int id)
        {
            return models[id];
        }
    }
    [Serializable]
    public struct BushPoinModel
    {
        public BushType Type;
        public Vector3 position;
    }
}