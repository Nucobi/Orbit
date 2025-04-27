using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> instanceToPrefab = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab)) pool[prefab] = new Queue<GameObject>();

        if (pool[prefab].Count > 0)
        {
            GameObject obj = pool[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefab);
            instanceToPrefab[newObj] = prefab; // 매핑 추가
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        if (!instanceToPrefab.ContainsKey(obj))
        {
            Debug.LogWarning("This object does not belong to the pool.");
            Destroy(obj); // 풀 외부에서 생성된 경우 삭제 처리
            return;
        }

        GameObject prefab = instanceToPrefab[obj];
        if (!pool.ContainsKey(prefab)) pool[prefab] = new Queue<GameObject>();

        //obj.SetActive(false);
        //pool[prefab].Enqueue(obj);
        Destroy(obj);
    }
}
