using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstances;
    
    [Header("Juice Prefabs")]
    public List<GameObject> pooledJuices;
    public GameObject juicesToPool;
    public int amountofJuicesToPool;
    
    [Header("Flies Prefabs")]
    public List<GameObject> pooledFlies;
    public GameObject fliesToPool;
    public int amountOfFliesToPool;

    private void Awake()
    {
        SharedInstances = this;
    }

    private void Start()
    {
        pooledJuices = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountofJuicesToPool; i++)
        {
            tmp = Instantiate((juicesToPool));
            tmp.SetActive(false);
            pooledJuices.Add(tmp);
        }
    }

    public GameObject GetPooledJuices()
    {
        for (int i = 0; i < amountofJuicesToPool; i++)
        {
            if (!pooledJuices[i].activeInHierarchy)
            {
                return pooledJuices[i];
            }
        }
        return null;
    }
}
