using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PooledObject FirstPattern;

    private void Start()
    {
        Manager.Pool.CreatePool(FirstPattern, 2, 4);

        float x = datas.stage[0].JellyXpos;
        float y = datas.stage[0].JellyYpos;

        Manager.Pool.GetPool(FirstPattern, new Vector3(x, y, 0), Quaternion.identity);
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