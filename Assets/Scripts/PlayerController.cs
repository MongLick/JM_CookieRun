using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ButtonType { None, Slide, Jump }

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] BoxCollider2D groundChecker;
    [SerializeField] PooledObject prefabScrollerPool1; // 수정된 부분
    [SerializeField] PooledObject prefabScrollerPool2;
    [SerializeField] PooledObject prefabScrollerPool3;
    [SerializeField] CSVController csvController;

    [Header("Specs")]
    [SerializeField] float jumpPower;

    [Header("Events")]
    public UnityEvent OnDied;
    public UnityEvent<ButtonType> OnButtonEvent;

    private bool isJumping;
    private int jumpCount;

    public GameObject CountImage;

    private void Start()
    {
        CountImage = GameObject.Find("Canvas/CountImage321");
        prefabScrollerPool1 = csvController.ReturnCurrentMap1();
        prefabScrollerPool2 = csvController.ReturnCurrentMap2();
        prefabScrollerPool3 = csvController.ReturnCurrentMap3();
    }

    public void Jump()
    {
        if (isJumping == false)
        {
            if (jumpCount == 0)
            {
                animator.SetBool("Slide", false);
                rigid.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
                jumpCount++;
            }
            else if (jumpCount == 1)
            {
                animator.SetBool("Slide", false);
                isJumping = true;
                rigid.velocity = Vector2.up * jumpPower;
                animator.SetBool("DoubleJump", true);
                animator.SetBool("Jump", false);
            }
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            SlideEnd();
            Jump();
            OnButtonEvent?.Invoke(ButtonType.Jump);
        }
        else
        {
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public void SlideStart()
    {
        animator.SetBool("Slide", true);
        this.boxCollider.enabled = true;
        this.capsuleCollider.enabled = false;
    }

    public void SlideEnd()
    {
        animator.SetBool("Slide", false);
        this.capsuleCollider.enabled = true;
        this.boxCollider.enabled = false;
    }

    private void OnSlide(InputValue value)
    {
        if (value.isPressed)
        {
            SlideStart();
            OnButtonEvent?.Invoke(ButtonType.Slide);
        }
        else
        {
            SlideEnd();
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public float hp = 100;
    public float damage = 10;
    public bool isGameover = false;
    public float delta;
    public float damageTime;

    public void Die()
    {
        if (isGameover == true && isJumping == false)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            animator.SetBool("Die", true);
            Invoke("TimeCheck", 1.3f);
            hp = 0;
            Slider.value = hp;
        }
    }

    public void TimeCheck()
    {
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", false);
            jumpCount = 0;
            animator.SetBool("DoubleJump", false);
        }
    }

    private void Update()
    {
        if (rigid.velocity.y <= 0)
            groundChecker.enabled = true;
        else if (rigid.velocity.y > 0.1f)
            groundChecker.enabled = false;

        if (isGameover == false)
        {
            delta += Time.deltaTime;
            hp -= delta * 0.7f;
            Slider.value = hp;
            delta = 0;
        }

        if (hp <= 0)
        {
            isGameover = true;
            Die();
        }

        if (CountImage != null)
        {
            CountImage.transform.position = rigid.transform.position + new Vector3(1.5f, 2.5f, 0);
        }
    }

    [SerializeField] Slider Slider;

    private bool isHurt = true;

    private IEnumerator CookieBlink()
    {
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        isHurt = true;
        cookiegrowing = false;
    }

    private bool cookiegrowing = false;

    private IEnumerator Cookiegrowing()
    {
        cookiegrowing = true;
        transform.localScale = new Vector3(3f, 3f, 3f);
        yield return new WaitForSeconds(3f);
        transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(CookieBlink());
    }

    public void TakeDamage()
    {
        if (isHurt && cookiegrowing == false)
        {
            hp -= damage;
            Slider.value = hp;
            isHurt = false;
            StartCoroutine(CookieBlink());
        }
    }

    public Text scoreTextCoin;
    public Text scoreTextJelly;
    public int coinScore = 0;
    public int jellyScore = 0;
    public int MaxHp = 100;

    public void UpdateScoreTextCoin(int newScore)
    {
        scoreTextCoin.text = newScore + "";
    }

    public void UpdateScoreTextJelly(int newScore)
    {
        scoreTextJelly.text = newScore + "";
    }

    public void GetItem(string name)
    {
        if (name == "growing")
        {
            StartCoroutine(Cookiegrowing());
        }
        else if (name == "runningFast")
        {
            if (prefabScrollerPool1.gameObject.activeSelf)
            {
                PrefabScroller instance1 = prefabScrollerPool1.GetComponent<PrefabScroller>();
                PrefabScroller instance2 = prefabScrollerPool2.GetComponent<PrefabScroller>();
                PrefabScroller instance3 = prefabScrollerPool3.GetComponent<PrefabScroller>();

                instance1.GetItem();
                instance2.GetItem();
                instance3.GetItem();
            }
        }
        else if (name == "potion")
        {
            hp += 30;
            if (hp >= MaxHp)
            {
                hp = MaxHp;
            }
            Slider.value = hp;
        }
        else if (name == "gold")
        {
            coinScore += 1000;
            UpdateScoreTextCoin(coinScore);
        }
        else if (name == "silver")
        {
            coinScore += 500;
            UpdateScoreTextCoin(coinScore);
        }
        else if (name == "pinkJelly")
        {
            jellyScore += 500;
            UpdateScoreTextJelly(jellyScore);
        }
        else if (name == "yellowJelly")
        {
            jellyScore += 1000;
            UpdateScoreTextJelly(jellyScore);
        }
    }
}
