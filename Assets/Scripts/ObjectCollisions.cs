using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectCollisions : MonoBehaviour
{
    private string ResourceTag = "Resource";
    private GameObject VisualPart;
    
    public GameObject ConnectedMixedObject;
    public GameObject OtherResource;
    public bool IsActive = true;


    private void Start()
    {
        VisualPart = gameObject.transform.GetChild(0).gameObject;
        IsActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(ResourceTag))
        {
            Debug.Log($"Resource {other.gameObject.name} hasn't tag {ResourceTag}");
            return;
        }

        if (!other.gameObject.GetComponent<ObjectType>())
        {
            Debug.Log($"Couldn't find component ObjectType {other.gameObject.name}");
            return;
        }

        ObjectType objType = gameObject.transform.GetChild(0).GetComponent<ObjectType>();
        ObjectType otherType = other.gameObject.GetComponent<ObjectType>();

        if (IsActive && other.gameObject.transform.parent.GetComponent<ObjectCollisions>().IsActive)
            TryGenerateMixedObject(objType, otherType);
    }

    private void TryGenerateMixedObject(ObjectType current, ObjectType other)
    {
        Debug.Log($"Try combine {current.name} with {other.name}");

        if (current.Type == other.Type)
        {
            Debug.Log($"The same ObjectType {current.Type.ToString()} on 2 GameObjects");
            return;
        }

        var type = other.Type;
        foreach (var r in ResourcesManager.Instance.Data.Descriptions)
        {
            if ((r.Type == current.Type && type == r.OtherType) ||
                r.Type == type && r.OtherType == current.Type)
            {
                Vector3 newpos = (other.transform.position + VisualPart.transform.position) / 2;

                SetActiveResources(this, other.gameObject.transform.parent.GetComponent<ObjectCollisions>(), false);

                ConnectedMixedObject = Instantiate(r.MixedPrefab, newpos, Quaternion.identity);

                var parent = other.GetComponentInParent<ObjectCollisions>();
                parent.ConnectedMixedObject = ConnectedMixedObject;
                OtherResource = parent.transform.gameObject;
                parent.OtherResource = gameObject;
                

                Debug.Log($"Successfull combine resources {gameObject.name} with {other.gameObject.name}");
                return;
            }
        }

        Debug.Log($"Couldn't combine resources {gameObject.name} with {other.gameObject.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(ResourceTag))
        {
            return;
        }

        Debug.Log($"TriggerExit {other.gameObject.name} ConnectedMixedObject {ConnectedMixedObject}");
        var parent = other.gameObject.transform.parent;

        if (parent.name.CompareTo(ConnectedMixedObject.name) == 0)
        {
            Destroy(ConnectedMixedObject);
            ConnectedMixedObject = null;

            SetActiveResources(this, OtherResource.GetComponent<ObjectCollisions>(), true);
        }
    }

    void SetActiveResources(ObjectCollisions res1, ObjectCollisions res2, bool value)
    {
        res1.VisualPart.SetActive(value);
        res1.IsActive = value;

        res2.VisualPart.SetActive(value);
        res2.IsActive = value;
    }
}
