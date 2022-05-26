using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField] int followerPrefabIndex;
    public int GetFollowerPrefabIndex()
    {
        return followerPrefabIndex;
    }
}
