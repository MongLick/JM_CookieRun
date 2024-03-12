using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVController : MonoBehaviour
{
    [SerializeField] 

    private void Start()
    {
        List<Dictionary<string, object>> reader = CSVReader.Read("Data/CSV/CookieRun");

        Debug.Log((int)reader[0]["JellyType"]);


    }
}
