using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OsmosRemake.Tears
{
    /// <summary>
    /// Каждая отдельная капля в игровом мире.
    /// </summary>
    public struct TearSpawnData
    {
        public float radius;
        public Vector2 position;
        public Vector2 acc;
        public Sprite sprite;
    }
}