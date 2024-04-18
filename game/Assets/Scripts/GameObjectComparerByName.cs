using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameObjectComparerByName : IComparer<GameObject>
{
    int IComparer<GameObject>.Compare(GameObject x, GameObject y)
    {
        return string.Compare(x.name, y.name);
    }
}