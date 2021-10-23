using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectCollisions : MonoBehaviour
{
    private string ResourceTag = "Resource";
    private GameObject VisualPart;
    private static ResourcesData ResourcesData;
    
    public GameObject ConnectedMixedObject;


    private void Start()
    {
        ResourcesData = Resources.Load<ResourcesData>("Data/ResourcesData");
        VisualPart = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(ResourceTag))
        {
            return;
        }

        if (!other.gameObject.GetComponent<ObjectType>())
        {
            Debug.Log($"Couldn't find component ObjectType {other.gameObject.name}");
            return;
        }

        ObjectType objType = gameObject.transform.GetChild(0).GetComponent<ObjectType>();
        ObjectType otherType = other.gameObject.GetComponent<ObjectType>();

        TryGenerateMixedObject(objType, otherType);
    }

    private void TryGenerateMixedObject(ObjectType current, ObjectType other)
    {
        Debug.Log($"Try combine {current.name} with {other.name}");

        if (current.type == other.type)
        {
            Debug.Log($"The same ObjectType on 2 GameObjects");
            return;
        }

        var type = other.type;
        foreach (var r in ResourcesData.Descriptions)
        {
            if ((r.Type == current.type && type == r.OtherType) ||
                r.Type == type && r.OtherType == current.type)
            {
                Vector3 newpos = (other.transform.position + VisualPart.transform.position) / 2;

                other.gameObject.SetActive(false);
                VisualPart.SetActive(false);

                ConnectedMixedObject = Instantiate(r.MixedPrefab, newpos, Quaternion.identity);
                return;
            }
        }

        Debug.Log("Couldn't combine resorces");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(ResourceTag))
        {
            return;
        }

        Debug.Log($"TriggerExit {other.gameObject.name}");
        
        if (other.gameObject != ConnectedMixedObject)
        {
            Destroy(ConnectedMixedObject);
            Destroy(other.gameObject.GetComponent<ObjectCollisions>().ConnectedMixedObject);

            other.gameObject.GetComponent<ObjectCollisions>().VisualPart.SetActive(true);
            VisualPart.SetActive(true);
        }
    }
}
