using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectCollisions : MonoBehaviour
{
    public GameObject MixedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<ObjectType>())
        {
            Debug.Log($"Couldn't find component ObjectType {other.gameObject.name}");
            return;
        }

        ObjectType obj = gameObject.transform.GetChild(0).GetComponent<ObjectType>();
        var type = other.gameObject.GetComponent<ObjectType>().type;
        if (obj.type == type)
        {
            Debug.Log($"The same ObjectType on 2 GameObjects");
            return;
        }

        Debug.Log($"Collide {obj.name} with {other.gameObject.name}");

        if (obj.type == ObjectType.TObjectType.TStone && type == ObjectType.TObjectType.TFire)
        {
            other.gameObject.gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(MixedObject, gameObject.transform);
        }
    }
}
