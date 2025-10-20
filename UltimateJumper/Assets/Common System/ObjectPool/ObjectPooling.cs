using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    public void clearPool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            Destroy(obj);
        }
        pooledObjects.Clear();
    }
    
    /// <summary>
    /// To return object to pool simply set it to inactive
    /// </summary>
    /// <param name="objectTag">The tag of the object you want to spawn</param>
    /// <param name="location">The transform location of where you want to spawn it</param>
    /// /// <param name="setActive">Whether you want to set the object to active when spawned or set it yourself</param>
    /// <returns></returns>
    public GameObject SpawnObject(string objectTag, Transform location, bool setActive = true)
    {
        GameObject obj = GetPooledObject(objectTag);
        if (obj != null)
        {
            obj.transform.position = location.position;
            obj.transform.rotation = location.rotation;
            obj.SetActive(setActive);
            return obj;
        }
        return null;
    }
}
