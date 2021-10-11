using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectCollisions : MonoBehaviour
{
    public GameObject visualPart;
    public GameObject MixedObject;
    public GameObject ConnectedMixedObject;

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
            Vector3 newpos = (other.transform.position + visualPart.transform.position) / 2;

            other.gameObject.SetActive(false);
            visualPart.SetActive(false);

            ConnectedMixedObject = Instantiate(MixedObject, newpos, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"TriggerExit {other.gameObject.name}");
        
        if (other.gameObject != MixedObject)
        {
            Destroy(ConnectedMixedObject);
            other.gameObject.SetActive(true);
            visualPart.SetActive(true);
        }
    }
}
