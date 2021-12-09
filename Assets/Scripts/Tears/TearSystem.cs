using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OsmosRemake.Controllers;

namespace OsmosRemake.Tears
{
    public class TearSystem : GameSystem
    {
        [SerializeField]
        public List<Tear> tears = new List<Tear>();
        public System.Action<Tear, Tear> OnCollision;
        [SerializeField]
        public Tear tearPrefab;
        [SerializeField]
        private Material mat;

        World world;
        OsmosRemake.Scriptables.GameData gameData;
        public override void Iterate()
        {
            foreach (Tear tear in tears)
            {
                Vector2 pos = tear.transform.position;
                float radius = tear.Radius * .5f;
                tear.acc *= 1 - gameData.environmentResistance;
                if (pos.x > World.current.width ||
                    pos.x < -World.current.width)
                {
                    tear.acc = new Vector2(-tear.acc.x, tear.acc.y);
                }
                if (pos.y > World.current.height ||
                    pos.y < -World.current.height)
                {
                    tear.acc = new Vector2(tear.acc.x, -tear.acc.y);
                }
                tear.transform.position += (Vector3)tear.acc;
            }
        }
        public override void OnSystemOn()
        {
            world = World.current;
            gameData = World.current.gameData;
            OnCollision += TierDrainsOtherTier;
        }
        public override void OnSystemOff()
        {
            OnCollision -= TierDrainsOtherTier;
        }
        public void CreateRandomTears()
        {
            Tear playerTear = CreatePlayerTear();
            OsmosRemake.Controllers.PlayerControllSystem.SetPlayerTear(playerTear);
            for (int n = 1; n < world.gameData.randomTearsCount; n++)
            {
                Tear tear = CreateNewTear(playerTear);
                float radius = tear.Radius;
                tear.transform.position = new Vector2(Random.Range(-world.width + radius, world.width - radius), Random.Range(-world.height + radius, world.height - radius));
            }
            RepaintTears(playerTear);
        }
        public void ClearTears()
        {
            for (int n = 0; n < tears.Count; n++)
            {
                GameObject.Destroy(tears[n].gameObject);
            }
            tears.Clear();
        }
        public Tear CreatePlayerTear()
        {
            Tear tear = Instantiate<Tear>(tearPrefab) as Tear;
            //tear.acc = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            tear.Radius = Random.Range(World.current.gameData.tearSizeMin, gameData.tearSizeMax);
            tear.GetComponent<SpriteRenderer>().material = new Material(mat);
            tear.GetComponent<SpriteRenderer>().material.SetColor("_Color", gameData.playerColor);
            tears.Add(tear);
            return tear;
        }
        public Tear CreateNewTear(Tear playerTear)
        {
            Tear tear = Instantiate<Tear>(tearPrefab) as Tear;
            tear.Radius = Random.Range(gameData.tearSizeMin, gameData.tearSizeMax);
            tear.SR.material = new Material(mat);
            tears.Add(tear);
            return tear;
        }
        public void RepaintTears(Tear playerTear)
        {
            for (int n = 1; n < tears.Count; n++)
            {
                float diff = tears[n].Radius / playerTear.Radius;
                tears[n].SR.material.SetColor("_Color",
                diff > 1 ? gameData.BiggerTearColor : gameData.LessTearColor);
            }
        }
        public void ReactiveMoving(Tear tear, Vector2 vector)
        {
            tear.acc += vector;
        }
        private void TierDrainsOtherTier(Tear tear1, Tear tear2)
        {
            OsmosRemake.Scriptables.GameData gameData = World.current.gameData;
            if (tear1.Radius > tear2.Radius)
            {
                if (tear2.Radius > gameData.tearSizeMin + gameData.drainSpeed)
                {
                    tear1.Radius += gameData.drainSpeed;
                    tear2.Radius -= gameData.drainSpeed;
                }
                else
                {
                    tear1.Radius += tear2.Radius;
                    if (tear2 == OsmosRemake.Controllers.PlayerControllSystem.currentTear)
                    {
                        World.current.OnGameDefeat();
                    }
                    tears.Remove(tear2);
                    GameObject.Destroy(tear2.gameObject);
                    if (tears.FirstOrDefault(t => t.Radius > PlayerControllSystem.currentTear.Radius) == null)
                        World.current.OnGameWin();
                }
                RepaintTears(PlayerControllSystem.currentTear);
            }
        }
    }
}