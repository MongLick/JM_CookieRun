using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStart : MonoBehaviour
{
    [SerializeField] PooledObject goldCoin;
    [SerializeField] PooledObject silverCoin;
    [SerializeField] PooledObject pinkJelly;
    [SerializeField] PooledObject yellowJelly;

    private void Start()
    {
        Manager.Pool.CreatePool(goldCoin, 30, 50);
        Manager.Pool.CreatePool(silverCoin, 30, 50);
        Manager.Pool.CreatePool(pinkJelly, 30, 50);
        Manager.Pool.CreatePool(yellowJelly, 30, 50);
    }
}
