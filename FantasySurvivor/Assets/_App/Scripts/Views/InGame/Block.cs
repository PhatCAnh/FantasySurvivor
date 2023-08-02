using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
    public enum WallDirectionType 
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public class Block : View<GameApp>
    {
        [SerializeField] Transform top;
        [SerializeField] Transform bottom;
        [SerializeField] Transform left;
        [SerializeField] Transform right;

        private Dictionary<WallDirectionType, Transform> wallDic = new Dictionary<WallDirectionType, Transform>();
        private void Awake() 
        {
            wallDic.Add(WallDirectionType.Top, top);
            wallDic.Add(WallDirectionType.Bottom, bottom);
            wallDic.Add(WallDirectionType.Left, left);
            wallDic.Add(WallDirectionType.Right, right);
        }

        public void SetActiveWall(WallDirectionType wallDirectionType, bool isActive) 
        {
            wallDic[wallDirectionType].gameObject.SetActive(isActive);
        }
    }
}
