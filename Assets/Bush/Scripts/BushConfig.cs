using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Bush
{
    [CreateAssetMenu(fileName = "BushConfig", menuName = "Configs/BushConfig")]
    public class BushConfig : ScriptableObject
    { 
        [SerializeField] private BushModel[] models;

        public BushModel GetModelByType(BushType type)
        {
            var model = new BushModel();
            foreach (var bushModel in models)
            {
                if (bushModel.Type == type)
                {
                    model = bushModel;
                }
            }
            return model;
        }
    }
    
    [Serializable]
    public struct BushModel
    {
        public BushType Type;
        public Bush BushPrefab;
        public int AddScoreCount;
        public float TimeToAddScore;
    }
    
    [Serializable]
    public enum BushType
    {
    Mini,
    Normal,
    Two,
    Big
    }
}