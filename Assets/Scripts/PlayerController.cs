using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigid; // ������ٵ�
    [SerializeField] Animator animator; // �ִϸ�����
    [SerializeField] BoxCollider2D boxCollider; // �ڽ��ݶ��̴�
    [SerializeField] CapsuleCollider2D capsuleCollider; // ĸ���ݶ��̴�

    [Header("Specs")]
    [SerializeField] float jumpPower; // �󸶳� ���� ����

    [Header("Events")]
    public UnityEvent OnDied; // �׾��� �� �������� �۾��ϱ� ���� �̺�Ʈ�� ����

    private bool isJumping;
    private int jumpCount;

    private void Jump() // �����ϸ� ȣ��
    {
        isJumping = true;
        rigid.velocity = Vector2.up * jumpPower; // ���� ���
        animator.SetBool("Jump", true);
        jumpCount++;
        if(Input.GetKey(KeyCode.Space) && jumpCount < 2) // �̰� ���� �����ؾ���. �ʹ� �ǰ���
        {
            rigid.velocity = Vector2.up * jumpPower;
            animator.SetBool("DoubleJump", true);
            animator.SetBool("Jump", false);
        }
    }

    private void OnJump(InputValue value) // �� �� ������ ����
    {
        if(value.isPressed && isJumping == false) // �� �� ����
        {
            Jump(); // ���� �Լ� ȣ��
        }
    }

    private void OnSlide(InputValue value) // ��ǲ �׼ǿ��� �������� �������� �˷���
    {
        if (value.isPressed) // ����
        {
            animator.SetBool("Slide", true); // �ִϸ����Ϳ��� true�� �ٲ���

            //this.boxCollider.enabled = true; // �̰� �� �� �Ǵ��� �𸣰��� �ؿ� 3�� ���� �� �� ��
            //this.capsuleCollider.enabled = false;

            //boxCollider.gameObject.SetActive(true);
            //capsuleCollider.gameObject.SetActive(false);

            //gameObject.GetComponent<CapsuleCollider>().enabled = false; 
            //gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else // ����
        {
            animator.SetBool("Slide", false); // �ִϸ����Ϳ��� false�� �ٲ���

            //this.boxCollider.enabled = false;
            //this.capsuleCollider.enabled = true;

            //boxCollider.gameObject.SetActive(false);
            //capsuleCollider.gameObject.SetActive(true);

            //gameObject.GetComponent<CapsuleCollider>().enabled = true;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Die() // �׾��� �� ȣ��
    {
        animator.SetBool("Die", true); // �ִϸ����Ϳ��� true�� �ٲ���
        OnDied?.Invoke(); // �̺�Ʈ�� �������� �۾� �Ϸ��� ���� 
    }

    private void OnCollisionEnter2D(Collision2D collision) // ���� ������� �±� �������� �����ؾ���
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
        // Die(); // Die �Լ� ȣ��

    }
}
