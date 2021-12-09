using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OsmosRemake.Tears
{
    /// <summary>
    /// Каждая отдельная капля в игровом мире.
    /// </summary>
    public class Tear : MonoBehaviour
    {
        private float AntiPI;
        private float radius;
        public float Radius
        {
            get { return radius; }
            set {
                radius = value;
                float scale = Mathf.Sqrt(radius) * AntiPI;
                transform.localScale = new Vector3(scale, scale, 0); }
        }
        public Vector2 acc;
        public static TearSystem system;
        public SpriteRenderer SR;
        public void Awake()
        {
            AntiPI = 1 / Mathf.PI;
            system = ((TearSystem)World.current.systems[typeof(TearSystem)]);
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            system.OnCollision(this, other.GetComponent<Tear>());
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            system.OnCollision(this, other.GetComponent<Tear>());
        }
    }
}