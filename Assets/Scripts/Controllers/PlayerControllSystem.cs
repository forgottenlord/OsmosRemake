using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OsmosRemake.Controllers
{
    public class PlayerControllSystem : GameSystem
    {
        public static Tears.Tear currentTear;
        public Tears.TearSystem tearSystem;
        public static void SetPlayerTear(Tears.Tear tear)
        {
            currentTear = tear;
        }
        public override void Iterate()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                MovingTear(touch.position);
            }
            if (Input.GetMouseButton(0))
            {
                MovingTear(Input.mousePosition);
            }
        }
        public void MovingTear(Vector2 vector)
        {
            if (currentTear != null && currentTear.transform != null)
            {
                currentTear.acc += (Vector2)(currentTear.transform.position -
                    Camera.current.ScreenToWorldPoint(vector)).normalized * .001f;
            }
        }
        public override void OnSystemOn()
        {
        }
        public override void OnSystemOff()
        {
        }
    }
}
