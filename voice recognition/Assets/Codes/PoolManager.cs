using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prepab;
    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prepab.Length];
        for (int i = 0; i < prepab.Length; i++) {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) {
        GameObject select = null;
        foreach (GameObject item in pools[index]) {
            if (!item.activeSelf) {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if(!select) 
        {
            select = Instantiate(prepab[index], transform);
            pools[index].Add(select);
        }
                    
        return select;
    }
}
