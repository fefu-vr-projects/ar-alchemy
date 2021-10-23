using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "Data/Resources", order = 1)]
public class ResourcesData : ScriptableObject
{
    [Serializable]
    public class ResourceDescription {
        [SerializeField]
        public ObjectType.TObjectType Type;
        [SerializeField]
        public ObjectType.TObjectType OtherType;

        [SerializeField]
        public GameObject MixedPrefab;
    }

    [SerializeField]
    public List<ResourceDescription> Descriptions;
}
 