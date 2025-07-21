using System;
using UnityEngine;

namespace Client.Bush
{
    public struct BushComponent
    {
        public Transform Transform;
        public int AddScoreCount;
        public float TimeToAddScore;
        public bool IsReadyToAddScore; 
        public Action<BushComponent> OnAddScore;
        public BushType Type;
    }
}