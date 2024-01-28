using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    [SerializeField] private ObjectPool[] pools;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach (var objectPool in pools)
        {
            objectPool.Initialize();
        }
    }


    public GameObject GetPool(PoolReference poolReference)
    {
        foreach (var objectPool in pools)
        {
            if (objectPool.poolReference == poolReference)
            {
                return objectPool.Get();
            }
        }

        return null;
    }
}



[System.Serializable]
public class ObjectPool
{
    public PoolReference poolReference;
    public GameObject prefab;
    public int count;
    public bool isExpandible;
    public Transform parent;
    private Queue<GameObject> pool;

    public void Initialize()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var newObj = Object.Instantiate(prefab);
            newObj.GetComponent<ObjectPooleable>().SetPool(this);
            newObj.transform.SetParent(parent);
            Pull(newObj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
           var obj = pool.Dequeue();
           obj.SetActive(true);
           return obj;
        }
        else
        {
            if (!isExpandible) return null;
            var newObj = Object.Instantiate(prefab);
            newObj.GetComponent<ObjectPooleable>().SetPool(this);
            newObj.transform.SetParent(parent);
            return newObj;
        }
    }

    public void Pull(GameObject obj)
    {
        pool.Enqueue(obj);
        obj.transform.position = Vector3.zero;
    }
}


public enum PoolReference
{
  CleaningCanvasWorldInteractable,
  ToolCanvasWorldInteractable
}
