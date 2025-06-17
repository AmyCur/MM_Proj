using System.Collections.Generic;
using UnityEngine;

using WFC.Enums;

namespace WFC.Consts
{
    public static class Dicts
    {
        // Dictionary that converts enum directions to vector directions
        public readonly static Dictionary<DEnum, Vector2Int> directionDic = new()
        {
            {DEnum.up, Vector2Int.up},
            {DEnum.down, Vector2Int.down},
            {DEnum.left, Vector2Int.right},
            {DEnum.right, Vector2Int.left}
        };

        // Dictionary
        public readonly static Dictionary<DEnum, DEnum> inverseDic = new()
        {
            {DEnum.up, DEnum.down},
            {DEnum.down, DEnum.up},
            {DEnum.left, DEnum.right},
            {DEnum.right, DEnum.left}
        };
    }

}