using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    private bool isJumping;
    private int jumpCount;

    private void Jump() // 점프하면 호출
    {
        isJumping = true;
        rigid.velocity = Vector2.up * jumpPower; // 점프 기능
        animator.SetBool("Jump", true);
        jumpCount++;
        if(Input.GetKey(KeyCode.Space) && jumpCount < 2) // 이거 부터 수정해야함. 너무 피곤함
        {
            rigid.velocity = Vector2.up * jumpPower;
            animator.SetBool("DoubleJump", true);
            animator.SetBool("Jump", false);
        }
    }

    private void OnJump(InputValue value) // 한 번 누르면 실행
    {
        if(value.isPressed && isJumping == false) // 한 번 누름
        {
            Jump(); // 점프 함수 호출
        }
    }

    private void OnSlide(InputValue value) // 인풋 액션에서 눌렀는지 떼었는지 알려줌
    {
        if (value.isPressed) // 눌림
        {
            animator.SetBool("Slide", true); // 애니메이터에서 true로 바꿔줌

            //this.boxCollider.enabled = true; // 이거 왜 안 되는지 모르겠음 밑에 3개 전부 다 안 됨
            //this.capsuleCollider.enabled = false;

            //boxCollider.gameObject.SetActive(true);
            //capsuleCollider.gameObject.SetActive(false);

            //gameObject.GetComponent<CapsuleCollider>().enabled = false; 
            //gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else // 떼짐
        {
            animator.SetBool("Slide", false); // 애니메이터에서 false로 바꿔줌

            //this.boxCollider.enabled = false;
            //this.capsuleCollider.enabled = true;

            //boxCollider.gameObject.SetActive(false);
            //capsuleCollider.gameObject.SetActive(true);

            //gameObject.GetComponent<CapsuleCollider>().enabled = true;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Die() // 죽었을 때 호출
    {
        animator.SetBool("Die", true); // 애니메이터에서 true로 바꿔줌
        OnDied?.Invoke(); // 이벤트로 여러가지 작업 하려고 만듬 
    }

    private void OnCollisionEnter2D(Collision2D collision) // 뭐가 닿았으면 태그 형식으로 수정해야함
    {
        switch(collision.gameObject.name)
        {
            case "Ground":
                isJumping = false;
                animator.SetBool("Jump", false);
                jumpCount = 0;
                animator.SetBool("DoubleJump", false);
                break;
        }
        // Die(); // Die 함수 호출

    }
}
