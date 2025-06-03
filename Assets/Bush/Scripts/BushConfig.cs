using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Bush
{
    [CreateAssetMenu(fileName = "BushConfig", menuName = "Configs/BushConfig")]
    public class BushConfig : ScriptableObject
    { 
        [SerializeField] private BushModel[] models;

        public BushModel GetModel(int id)
        {
            return models[id];
        }
    }
    
    [Serializable]
    public struct BushModel
    {
        public int id;
        public Vector3 position;
    }
}