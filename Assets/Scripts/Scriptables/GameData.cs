using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OsmosRemake.Scriptables
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public Color playerColor;
        public Color LessTearColor;
        public Color BiggerTearColor;
        [SerializeField]
        [Range(0.0001f, .9f)]
        public float environmentResistance = 0.1f;
        [SerializeField]
        [Range(0.01f, 10)]
        public float drainSpeed;
        [SerializeField]
        [Range(0.01f, 1)]
        public float tearSizeMax;
        [SerializeField]
        [Range(0.001f, 1)]
        public float tearSizeMin;
        [SerializeField]
        [Range(1, 400)]
        public int randomTearsCount;
    }
}
