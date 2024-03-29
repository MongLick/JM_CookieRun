using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] GameObject[] collisionObjectArr;

    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }

    [SerializeField] Scroller scroller;

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

        if (scroller != null && player != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            scroller.GetItem();
        }

        if (player != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            bool isGrowing = gameObject.name.Contains("Growing");
            bool isMagnet = gameObject.name.Contains("Magnet");
            bool isMakeCoin = gameObject.name.Contains("MakeCoin");
            bool isMakeJelly = gameObject.name.Contains("MakeJelly");
            bool isPotion = gameObject.name.Contains("Potion");
            bool isRunningFast = gameObject.name.Contains("RunningFast");

            bool isGoldCoin = gameObject.name.Contains("GoldCoin");
            bool isSilverCoin = gameObject.name.Contains("SilverCoin");

            bool isPinkJelly = gameObject.name.Contains("PinkJelly");
            bool isYellowJelly = gameObject.name.Contains("YellowJelly");

            if (isGrowing)
            {
                Debug.Log("아이템1");
                gameObject.SetActive(false);
                player.GetItem("growing");
            }
            if (isMagnet)
            {
                Debug.Log("아이템2");
                gameObject.SetActive(false);
            }
            if (isMakeCoin)
            {
                Debug.Log("아이템3");
                gameObject.SetActive(false);
            }
            if (isMakeJelly)
            {
                Debug.Log("아이템4");
                gameObject.SetActive(false);
            }
            if (isPotion)
            {
                Debug.Log("아이템5");
                gameObject.SetActive(false);
                player.GetItem("potion");
            }
            if (isRunningFast)
            {
                Debug.Log("아이템6");
                gameObject.SetActive(false);
                player.GetItem("runningFast");
            }
            if (isGoldCoin)
            {
                Debug.Log("골드7");
                gameObject.SetActive(false);
                player.GetItem("gold");
            }
            if (isSilverCoin)
            {
                Debug.Log("실버8");
                gameObject.SetActive(false);
                player.GetItem("silver");
            }
            if (isPinkJelly)
            {
                Debug.Log("핑크9");
                gameObject.SetActive(false);
                player.GetItem("pinkJelly");
            }
            if (isYellowJelly)
            {
                Debug.Log("노랑");
                gameObject.SetActive(false);
                player.GetItem("yellowJelly");
            }
        }

        if (player != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool isDoubleJumpObstacle = gameObject.name.Contains("DoubleJumpObstacle");
            bool isJumpObstacle = gameObject.name.Contains("JumpObstacle");
            bool isSlideObstacle = gameObject.name.Contains("SlideObstacle");
            bool isDeadZone = gameObject.name.Contains("DeadZone");

            if (isDoubleJumpObstacle || isJumpObstacle || isSlideObstacle)
            {
                player.TakeDamage();
            }

            if (isDeadZone)
            {
                player.isGameover = true;
                player.Die();
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