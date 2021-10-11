using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public enum TObjectType
    {
        TStone, TFire, TBricks, TTree, TFlower
    };

    [SerializeField]
    public TObjectType type;
}
