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
    [SerializeField] Rigidbody2D rigid; // ������ٵ�
    [SerializeField] Animator animator; // �ִϸ�����
    [SerializeField] BoxCollider2D boxCollider; // �ڽ��ݶ��̴�
    [SerializeField] CapsuleCollider2D capsuleCollider; // ĸ���ݶ��̴�

    [Header("Specs")]
    [SerializeField] float jumpPower; // �󸶳� ���� ����

    [Header("Events")]
    public UnityEvent OnDied; // �׾��� �� �������� �۾��ϱ� ���� �̺�Ʈ�� ����

    private bool isJumping; // ���������� üũ
    private int jumpCount; // ���� 1�� �ϸ� ++�� ��

    private bool isSliding = false; // �����̵������� üũ

    public void Jump() // �����ϸ� ȣ��
    {
        if (isJumping == false && isSliding == false) // �������� �ƴϰ� �����̵� ���� �ƴ� ��
        {
            if (jumpCount == 0) // ���� ī���� 0�� �� ����
            {
                rigid.velocity = Vector2.up * jumpPower; // ���� ����
                animator.SetBool("Jump", true); // �ִϸ����Ϳ��� true�� �ٲ���
                jumpCount++; // ���� ī���� ++
            }
            
            else if (jumpCount == 1) // ���� ī���� 1�� �� ����
            {
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
            Jump(); // ���� �Լ� ȣ��
        }
    }

    public void SlideStart() // �����̵� ����
    {
        animator.SetBool("Slide", true); // �ִϸ����Ϳ��� true�� �ٲ���
        isSliding = true; // �����̵���

        this.boxCollider.enabled = true; // �ڽ� �ݶ��̴� �� 
        this.capsuleCollider.enabled = false; // ĸ�� �ݶ��̴� ����
    }

    public void SlideEnd() // �����̵� ��
    {
        animator.SetBool("Slide", false); // �ִϸ����Ϳ��� false�� �ٲ���
        isSliding = false; // �����̵����� �ƴ�

        this.capsuleCollider.enabled = true; // ĸ�� �ݶ��̴� ����
        this.boxCollider.enabled = false; // �ڽ� �ݶ��̴� ��
    }

    private void OnSlide(InputValue value) // ��ǲ �׼ǿ��� �������� �������� �˷���
    {
        if (value.isPressed) // ����
        {
            SlideStart(); // Start �Լ� ȣ��
        }
        else // �� ����
        {
            SlideEnd(); // End �Լ� ȣ��
        }
    }

    private void Die() // �׾��� �� ȣ��
    {
        animator.SetBool("Die", true); // �ִϸ����Ϳ��� true�� �ٲ���
        OnDied?.Invoke(); // �̺�Ʈ�� �������� �۾� �Ϸ��� ���� 
    }

    private void OnCollisionEnter2D(Collision2D collision) // ���� ������� ȣ��
    {
        if (collision.gameObject.tag.CompareTo("Ground") == 0) // ���� �������
        {
            isJumping = false; // ������ �� �ϰ� �ִٷ� �����
            animator.SetBool("Jump", false); // ���� �ִϸ��̼� ����
            jumpCount = 0; // ���� ī���� �ʱ�ȭ
            animator.SetBool("DoubleJump", false); // // �������� �ִϸ��̼� ����
        }
        // Die(); // Die �Լ� ȣ��

    }

}
