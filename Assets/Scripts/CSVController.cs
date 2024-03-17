using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVController : MonoBehaviour
{
    [SerializeField] PooledObject FirstPattern;
    [SerializeField] PooledObject SecondPattern;
    [SerializeField] PooledObject ThirdPattern;

    private void Start()
    {
        StartCoroutine(PrefabgRoutine());
    }

    public IEnumerator PrefabgRoutine()
    {
        float x;
        float y;

        Manager.Pool.CreatePool(FirstPattern, 1, 3);
        Manager.Pool.CreatePool(SecondPattern, 1, 3);
        Manager.Pool.CreatePool(ThirdPattern, 1, 3);

        x = datas.stage[0].JellyXpos;
        y = datas.stage[0].JellyYpos;

        Manager.Pool.GetPool(FirstPattern, new Vector3(x, y, 0), Quaternion.identity);
        yield return new WaitForSeconds(13f);

        PooledObject pooledObject;

        while (true)
        {
            x = datas.stage[1].JellyXpos;
            y = datas.stage[1].JellyYpos;

            pooledObject = Manager.Pool.GetPool(SecondPattern, new Vector3(x, y, 0), Quaternion.identity);
            pooledObject.Init();
            yield return new WaitForSeconds(22f);

            pooledObject = Manager.Pool.GetPool(ThirdPattern, new Vector3(x, y, 0), Quaternion.identity);
            pooledObject.Init();
            yield return new WaitForSeconds(22f);

            pooledObject = Manager.Pool.GetPool(FirstPattern, new Vector3(x, y, 0), Quaternion.identity);
            pooledObject.Init();
            yield return new WaitForSeconds(22f);
        }
    }

    public TextAsset data;
    private AllData datas;

    private void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);

        foreach (var VARIABLE in datas.stage)
        {

        }
    }

    [System.Serializable]
    public class AllData
    {
        public MapData[] stage;
    }

    [System.Serializable]
    public class MapData
    {
        public int stageID;
        public float JellyXpos;
        public float JellyYpos;
    }
}
