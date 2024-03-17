using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] GameObject[] collisionObjectArr;


    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }

    public void OnEnable()
    {
        if (autoRelease)
        {
            StartCoroutine(ReleaseRoutine());
        }
    }

    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(releaseTime);
        Release();
    }

    public void Release()
    {
        if (pool != null)
        {
            pool.ReturnPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool isGrowing = gameObject.name.Contains("Growing");
            bool isMagnet = gameObject.name.Contains("Magnet");
            bool isMakeCoin = gameObject.name.Contains("MakeCoin");
            bool isMakeJelly = gameObject.name.Contains("MakeJelly");
            bool isPotion = gameObject.name.Contains("Potion");
            bool iRunningFast = gameObject.name.Contains("RunningFast");

            bool isGoldCoin = gameObject.name.Contains("GoldCoin");
            bool isSilverCoin = gameObject.name.Contains("SilverCoin");

            bool isPinkJelly = gameObject.name.Contains("PinkJelly");
            bool isYellowJelly = gameObject.name.Contains("YellowJelly");

            if (isGrowing)
            {
                Debug.Log("������1");
                gameObject.SetActive(false);
            }
            if (isMagnet)
            {
                Debug.Log("������2");
                gameObject.SetActive(false);
            }
            if (isMakeCoin)
            {
                Debug.Log("������3");
                gameObject.SetActive(false);
            }
            if (isMakeJelly)
            {
                Debug.Log("������4");
                gameObject.SetActive(false);
            }
            if (isPotion)
            {
                Debug.Log("������5");
                gameObject.SetActive(false);
                player.GetItem("potion");

            }
            if (iRunningFast)
            {
                Debug.Log("������6");
                gameObject.SetActive(false);
            }
            if (isGoldCoin)
            {
                Debug.Log("���7");
                gameObject.SetActive(false);
            }
            if (isSilverCoin)
            {
                Debug.Log("�ǹ�8");
                gameObject.SetActive(false);
            }
            if (isPinkJelly)
            {
                Debug.Log("��ũ9");
                gameObject.SetActive(false);
            }
            if (isYellowJelly)
            {
                Debug.Log("���");
                gameObject.SetActive(false);
            }
        }

        if (player != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool isDoubleJumpObstacle = gameObject.name.Contains("DoubleJumpObstacle");
            bool isJumpObstacle = gameObject.name.Contains("JumpObstacle");
            bool isSlideObstacle = gameObject.name.Contains("SlideObstacle");

            if (isDoubleJumpObstacle || isJumpObstacle || isSlideObstacle)
            {
                player.TakeDamage();
            }
        }
    }

    public void Init()
    {
        int count = collisionObjectArr.Length;
        for (int i = 0; i < count; i++)
        {
            collisionObjectArr[i].SetActive(true);
        }
    }
}
