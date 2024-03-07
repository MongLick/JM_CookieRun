using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigid; // 리지드바디
    [SerializeField] Animator animator; // 애니메이터
    [SerializeField] BoxCollider2D boxCollider; // 박스콜라이더
    [SerializeField] CapsuleCollider2D capsuleCollider; // 캡슐콜라이더

    [Header("Specs")]
    [SerializeField] float jumpPower; // 얼마나 힘을 줄지

    [Header("Events")]
    public UnityEvent OnDied; // 죽었을 때 여러가지 작업하기 위해 이벤트로 구현

    private bool isJumping; // 점프중인지 체크
    private int jumpCount; // 점프 1번 하면 ++이 됨

    private bool isSliding = false; // 슬라이드중인지 체크

    public void Jump() // 점프하면 호출
    {
        if (isJumping == false && isSliding == false) // 점프중이 아니고 슬라이드 중이 아닐 때
        {
            if (jumpCount == 0) // 점프 카운터 0일 때 실행
            {
                rigid.velocity = Vector2.up * jumpPower; // 점프 높이
                animator.SetBool("Jump", true); // 애니메이터에서 true로 바꿔줌
                jumpCount++; // 점프 카운터 ++
            }
            
            else if (jumpCount == 1) // 점프 카운터 1일 때 실행
            {
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
            Jump(); // 점프 함수 호출
        }
    }

    public void SlideStart() // 슬라이드 시작
    {
        animator.SetBool("Slide", true); // 애니메이터에서 true로 바꿔줌
        isSliding = true; // 슬라이드중

        this.boxCollider.enabled = true; // 박스 콜라이더 온 
        this.capsuleCollider.enabled = false; // 캡슐 콜라이더 오프
    }

    public void SlideEnd() // 슬라이드 끝
    {
        animator.SetBool("Slide", false); // 애니메이터에서 false로 바꿔줌
        isSliding = false; // 슬라이드중이 아님

        this.capsuleCollider.enabled = true; // 캡슐 콜라이더 오프
        this.boxCollider.enabled = false; // 박스 콜라이더 온
    }

    private void OnSlide(InputValue value) // 인풋 액션에서 눌렀는지 떼었는지 알려줌
    {
        if (value.isPressed) // 누름
        {
            SlideStart(); // Start 함수 호출
        }
        else // 안 누름
        {
            SlideEnd(); // End 함수 호출
        }
    }

    private void Die() // 죽었을 때 호출
    {
        animator.SetBool("Die", true); // 애니메이터에서 true로 바꿔줌
        OnDied?.Invoke(); // 이벤트로 여러가지 작업 하려고 만듬 
    }

    private void OnCollisionEnter2D(Collision2D collision) // 뭐가 닿았으면 호출
    {
        if (collision.gameObject.tag.CompareTo("Ground") == 0) // 뭐가 닿았으면
        {
            isJumping = false; // 점프를 안 하고 있다로 변경됨
            animator.SetBool("Jump", false); // 점프 애니메이션 꺼줌
            jumpCount = 0; // 점프 카운터 초기화
            animator.SetBool("DoubleJump", false); // // 더블점프 애니메이션 꺼줌
        }
        // Die(); // Die 함수 호출

    }

}
