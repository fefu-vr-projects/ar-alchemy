using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager instance;
    public static ResourcesManager Instance => instance;

    public ResourcesData Data;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Data = Resources.Load<ResourcesData>("Data/ResourcesData");
    }
}
