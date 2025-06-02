using System;
using UnityEngine;

namespace Client.Point
{
    [CreateAssetMenu(fileName = "PointConfig", menuName = "Configs/PointConfig")]
    public class PointConfig : ScriptableObject
    {
        [SerializeField] private PointModel[] points;

        public PointModel GetModel(int id)
        {
            return points[id];
        }
    }

    [Serializable]
    public struct PointModel
    {
        public int id;
        public Vector3 position;
    }
}