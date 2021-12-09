using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OsmosRemake.Scriptables;

namespace OsmosRemake
{
    public class World : MonoBehaviour
    {
        public static World current;
        public Dictionary<Type, GameSystem> systems = new Dictionary<Type, GameSystem>();
        public float height;
        public float width;
        [SerializeField]
        public GameData gameData;
        [SerializeField]
        public PlayerData playerData;

        public Action OnGameWin;
        public GameObject WinPanel;
        public Action OnGameDefeat;
        public GameObject DefeatPanel;

        private void Awake()
        {
            OnGameDefeat += OnDefeat;
            OnGameWin += OnWin;
            Camera cam = Camera.main;
            height = cam.orthographicSize;
            width = height * cam.aspect;
            current = this;
            foreach (GameSystem system in GetComponents<GameSystem>())
            {
                system.OnSystemOn();
                systems.Add(system.GetType(), system);
            }
            ((Tears.TearSystem)systems[typeof(Tears.TearSystem)]).CreateRandomTears();
        }
        private void FixedUpdate()
        {
            foreach (GameSystem system in systems.Values)
            {
                system.Iterate();
            }
            //Graphics.Blit();
        }
        public void Restart()
        {
            Tears.TearSystem tearSystem = ((Tears.TearSystem)systems[typeof(Tears.TearSystem)]);
            tearSystem.ClearTears();
            tearSystem.CreateRandomTears();
            DefeatPanel.SetActive(false);
            WinPanel.SetActive(false);
        }
        private void OnDefeat()
        {
            DefeatPanel.SetActive(true);
        }
        private void OnWin()
        {
            WinPanel.SetActive(true);
        }
    }
}
