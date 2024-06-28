using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] prefabs;
    public int poolSize = 20;

    private Dictionary<int, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(i, objectPool);
        }
    }

    public GameObject GetObject(int prefabIndex, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(prefabIndex) && poolDictionary[prefabIndex].Count > 0)
        {
            GameObject obj = poolDictionary[prefabIndex].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefabs[prefabIndex], position, rotation);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj, int prefabIndex)
    {
        obj.SetActive(false);
        if (poolDictionary.ContainsKey(prefabIndex))
        {
            poolDictionary[prefabIndex].Enqueue(obj);
        }
    }
}
