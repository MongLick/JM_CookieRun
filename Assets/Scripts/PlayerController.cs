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
    [SerializeField] Rigidbody2D rigid; // ������ٵ�
    [SerializeField] Animator animator; // �ִϸ�����
    [SerializeField] BoxCollider2D boxCollider; // �ڽ��ݶ��̴�
    [SerializeField] CapsuleCollider2D capsuleCollider; // ĸ���ݶ��̴�
    [SerializeField] BoxCollider2D groundChecker;

    [Header("Specs")]
    [SerializeField] float jumpPower; // �󸶳� ���� ����

    [Header("Events")]
    public UnityEvent OnDied; // �׾��� �� �������� �۾��ϱ� ���� �̺�Ʈ�� ����

    public UnityEvent<ButtonType> OnButtonEvent;

    private bool isJumping; // ���������� üũ
    private int jumpCount; // ���� 1�� �ϸ� ++�� ��

    public GameObject CountImage;

    private void Start()
    {
        CountImage = GameObject.Find("Canvas/CountImage321");
    }

    public void Jump() // �����ϸ� ȣ��
    {
        if (isJumping == false) // ��������
        {
            if (jumpCount == 0) // ���� ī���� 0�� �� ����
            {
                animator.SetBool("Slide", false);
                rigid.velocity = Vector2.up * jumpPower; // ���� ����
                animator.SetBool("Jump", true); // �ִϸ����Ϳ��� true�� �ٲ���
                jumpCount++; // ���� ī���� ++
            }

            else if (jumpCount == 1) // ���� ī���� 1�� �� ����
            {
                animator.SetBool("Slide", false);
                isJumping = true; // ���� �� ���߿��� �����ϴ°� ���� �ڵ�
                rigid.velocity = Vector2.up * jumpPower; // ���� ����
                animator.SetBool("DoubleJump", true); // �ִϸ����Ϳ��� true�� �ٲ���
                animator.SetBool("Jump", false); // �ִϸ����Ϳ��� false�� �ٲ���
            }
        }
    }

    private void OnJump(InputValue value) // �� �� ������ ����
    {
        if (value.isPressed) // ������ �� ȣ�� ��ǲ �׼ǵ� ����ϱ� ���� �������
        {
            SlideEnd();
            Jump(); // ���� �Լ� ȣ��
            OnButtonEvent?.Invoke(ButtonType.Jump);
        }
        else
        {
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public void SlideStart() // �����̵� ����
    {
        animator.SetBool("Slide", true); // �ִϸ����Ϳ��� true�� �ٲ���

        this.boxCollider.enabled = true; // �ڽ� �ݶ��̴� �� 
        this.capsuleCollider.enabled = false; // ĸ�� �ݶ��̴� ����
    }

    public void SlideEnd() // �����̵� ��
    {
        animator.SetBool("Slide", false); // �ִϸ����Ϳ��� false�� �ٲ���

        this.capsuleCollider.enabled = true; // ĸ�� �ݶ��̴� ����
        this.boxCollider.enabled = false; // �ڽ� �ݶ��̴� ��
    }

    private void OnSlide(InputValue value) // ��ǲ �׼ǿ��� �������� �������� �˷���
    {
        if (value.isPressed) // ����
        {

            SlideStart(); // Start �Լ� ȣ��
            OnButtonEvent?.Invoke(ButtonType.Slide);
        }
        else
        {
            SlideEnd(); // End �Լ� ȣ��
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public float hp = 100;
    public float damage = 10;
    public bool isGameover = false;
    public float delta;
    public float damageTime;

    public void Die() // �׾��� �� ȣ��
    {
        if (isGameover == true && isJumping == false)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            animator.SetBool("Die", true); // �ִϸ����Ϳ��� true�� �ٲ���
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // ���� �������
        {
            isJumping = false; // ������ �� �ϰ� �ִٷ� �����
            animator.SetBool("Jump", false); // ���� �ִϸ��̼� ����
            jumpCount = 0; // ���� ī���� �ʱ�ȭ
            animator.SetBool("DoubleJump", false); // // �������� �ִϸ��̼� ����
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

        if(CountImage != null)
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

    private IEnumerator runningFasting()
    {
        Scroller.scrollSpeed = 10;
        PrefabScroller.scrollSpeed = 10;
        yield return new WaitForSeconds(3f);
        Scroller.scrollSpeed = 5;
        PrefabScroller.scrollSpeed = 5;
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

    Scroller scroller = new Scroller();
    PrefabScroller prefabScroller = new PrefabScroller();

    public void GetItem(string name)
    {
        if(name == "growing")
        {
            StartCoroutine(Cookiegrowing());
        }

        if(name == "runningFast")
        {
            StartCoroutine(runningFasting());
        }

        if (name == "potion")
        {
            hp += 30;
            if (hp >= MaxHp)
            {
                hp = MaxHp;
            }
        }

        if (name == "gold")
        {
            coinScore += 1000;
            UpdateScoreTextCoin(coinScore);
        }

        if (name == "silver")
        {
            coinScore += 500;
            UpdateScoreTextCoin(coinScore);
        }

        if (name == "pinkJelly")
        {
            jellyScore += 500;
            UpdateScoreTextJelly(jellyScore);
        }

        if (name == "yellowJelly")
        {
            jellyScore += 1000;
            UpdateScoreTextJelly(jellyScore);
        }
    }
}

