using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectType : MonoBehaviour
{
    public enum TObjectType
    {
        TFire, TTree, TWater, TStone, TMixed
    };

    [SerializeField]
    public TObjectType type;
}
