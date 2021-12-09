using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OsmosRemake
{
    public abstract class GameSystem : MonoBehaviour
    {
        public abstract void Iterate();
        public abstract void OnSystemOn();
        public abstract void OnSystemOff();
    }
}
