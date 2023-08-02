using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
    public class Map : View<GameApp>
    {
        private CompositeCollider2D collider2D;
        [SerializeField] Transform trfParent;
        public List<Block> blocks { get; private set; } = new List<Block>();

        public void SpawnMap(int[,] configs) 
        {
            StartCoroutine(RunSpawnMap(configs));
        }

        private IEnumerator RunSpawnMap(int[,] configs) 
        {
            for (int i = 0; i < configs.GetLength(0); i++) 
            {
                for (int j = 0; j < configs.GetLength(1); j++) 
                {
                    var value = configs[i, j];
                    if (value == 0)
                        continue;

                    SpawnBlock(i, j, configs);
                }
            }

            yield return null;
            AddCompositeCollider();
            yield return null;
            RefreshCollider();
        }
            

        private void SpawnBlock(int x, int y, int[,] configs) 
        {
            var block = Instantiate(app.resourceManager.blockPrefab, trfParent);
            var xPosition = x * GameConst.BLOCK_SIZE;
            var yPosition = y * GameConst.BLOCK_SIZE;
            block.transform.position = new Vector2(xPosition, yPosition);
            var top = new Vector2Int(x, y + 1);
            var bottom = new Vector2Int(x, y - 1);
            var left = new Vector2Int(x - 1, y);
            var right = new Vector2Int(x + 1, y);

            block.SetActiveWall(WallDirectionType.Top, !IsHaveBlock(top.x, top.y, configs));
            block.SetActiveWall(WallDirectionType.Bottom, !IsHaveBlock(bottom.x, bottom.y, configs));
            block.SetActiveWall(WallDirectionType.Left, !IsHaveBlock(left.x, left.y, configs));
            block.SetActiveWall(WallDirectionType.Right, !IsHaveBlock(right.x, right.y, configs));
            var blockId = configs[x, y];
            var blockCfg = app.configs.blockConfigs.GetConfig(blockId);

            foreach (var item in blockCfg.items)
            {
                var prefab = app.resourceManager.GetItem(item.type);
                var go = Instantiate(prefab, trfParent);
                go.transform.position = new Vector2(xPosition + item.position.x, yPosition + item.position.y);
            }
        }

        private bool IsHaveBlock(int x, int y, int[,] configs) 
        {
            if (x < 0 || x >= configs.GetLength(0))
                return false;

            if (y < 0 || y >= configs.GetLength(1))
                return false;

            var value = configs[x, y];
            return value != 0;
        }


        public void AddCompositeCollider() 
        {
            collider2D = trfParent.gameObject.GetOrAddComponent<CompositeCollider2D>();
            collider2D.vertexDistance = 0.2f;
            collider2D.offsetDistance = 0.1f;
            collider2D.generationType = CompositeCollider2D.GenerationType.Manual;
            collider2D.geometryType = CompositeCollider2D.GeometryType.Polygons;
        }


        public void RefreshCollider()
        {
            collider2D.GenerateGeometry();
        }
    }
}
