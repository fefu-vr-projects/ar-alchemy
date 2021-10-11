using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectCollisions : MonoBehaviour
{
    public enum ObjectType
    {
        TStone, TFire, TBricks, TTree, TFlower
    };

    [SerializeField]
    public ObjectType type;

    public GameObject MixedObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectCollisions obj = gameObject.GetComponent<ObjectCollisions>();
        if (obj.type == type) return;

        if (obj.type == ObjectType.TStone && type == ObjectType.TFire)
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(MixedObject, gameObject.transform);
        }
    }
}
