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
    [SerializeField] Rigidbody2D rigid; // 리지드바디
    [SerializeField] Animator animator; // 애니메이터
    [SerializeField] BoxCollider2D boxCollider; // 박스콜라이더
    [SerializeField] CapsuleCollider2D capsuleCollider; // 캡슐콜라이더
    [SerializeField] BoxCollider2D groundChecker;

    [Header("Specs")]
    [SerializeField] float jumpPower; // 얼마나 힘을 줄지

    [Header("Events")]
    public UnityEvent OnDied; // 죽었을 때 여러가지 작업하기 위해 이벤트로 구현

    public UnityEvent<ButtonType> OnButtonEvent;

    private bool isJumping; // 점프중인지 체크
    private int jumpCount; // 점프 1번 하면 ++이 됨

    public GameObject CountImage;

    private void Start()
    {
        CountImage = GameObject.Find("Canvas/CountImage321");
    }

    public void Jump() // 점프하면 호출
    {
        if (isJumping == false) // 점프중이
        {
            if (jumpCount == 0) // 점프 카운터 0일 때 실행
            {
                animator.SetBool("Slide", false);
                rigid.velocity = Vector2.up * jumpPower; // 점프 높이
                animator.SetBool("Jump", true); // 애니메이터에서 true로 바꿔줌
                jumpCount++; // 점프 카운터 ++
            }

            else if (jumpCount == 1) // 점프 카운터 1일 때 실행
            {
                animator.SetBool("Slide", false);
                isJumping = true; // 점프 중 공중에서 점프하는거 막는 코드
                rigid.velocity = Vector2.up * jumpPower; // 점프 높이
                animator.SetBool("DoubleJump", true); // 애니메이터에서 true로 바꿔줌
                animator.SetBool("Jump", false); // 애니메이터에서 false로 바꿔줌
            }
        }
    }

    private void OnJump(InputValue value) // 한 번 누르면 실행
    {
        if (value.isPressed) // 눌렸을 때 호출 인풋 액션도 사용하기 위해 만든거임
        {
            SlideEnd();
            Jump(); // 점프 함수 호출
            OnButtonEvent?.Invoke(ButtonType.Jump);
        }
        else
        {
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public void SlideStart() // 슬라이드 시작
    {
        animator.SetBool("Slide", true); // 애니메이터에서 true로 바꿔줌

        this.boxCollider.enabled = true; // 박스 콜라이더 온 
        this.capsuleCollider.enabled = false; // 캡슐 콜라이더 오프
    }

    public void SlideEnd() // 슬라이드 끝
    {
        animator.SetBool("Slide", false); // 애니메이터에서 false로 바꿔줌

        this.capsuleCollider.enabled = true; // 캡슐 콜라이더 오프
        this.boxCollider.enabled = false; // 박스 콜라이더 온
    }

    private void OnSlide(InputValue value) // 인풋 액션에서 눌렀는지 떼었는지 알려줌
    {
        if (value.isPressed) // 누름
        {

            SlideStart(); // Start 함수 호출
            OnButtonEvent?.Invoke(ButtonType.Slide);
        }
        else
        {
            SlideEnd(); // End 함수 호출
            OnButtonEvent?.Invoke(ButtonType.None);
        }
    }

    public float hp = 100;
    public float damage = 10;
    public bool isGameover = false;
    public float delta;
    public float damageTime;

    public void Die() // 죽었을 때 호출
    {
        if (isGameover == true && isJumping == false)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
            animator.SetBool("Die", true); // 애니메이터에서 true로 바꿔줌
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // 뭐가 닿았으면
        {
            isJumping = false; // 점프를 안 하고 있다로 변경됨
            animator.SetBool("Jump", false); // 점프 애니메이션 꺼줌
            jumpCount = 0; // 점프 카운터 초기화
            animator.SetBool("DoubleJump", false); // // 더블점프 애니메이션 꺼줌
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

