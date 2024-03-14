using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ButtonType { None, Slide, Jump}

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

    private void Die() // �׾��� �� ȣ��
    {
        animator.SetBool("Die", true); // �ִϸ����Ϳ��� true�� �ٲ���
        OnDied?.Invoke(); // �̺�Ʈ�� �������� �۾� �Ϸ��� ���� 
    }

    private void OnCollisionEnter2D(Collision2D collision) // ���� ������� ȣ��
    {
        //if (collision.gameObject.tag.CompareTo("Ground") == 0) // ���� �������
        //{
        //    isJumping = false; // ������ �� �ϰ� �ִٷ� �����
        //    animator.SetBool("Jump", false); // ���� �ִϸ��̼� ����
        //    jumpCount = 0; // ���� ī���� �ʱ�ȭ
        //    animator.SetBool("DoubleJump", false); // // �������� �ִϸ��̼� ����
        //}
        // Die(); // Die �Լ� ȣ��
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
        //Die(); // Die �Լ� ȣ��
    }

    private void Update()
    {
        if (rigid.velocity.y <= 0)
            groundChecker.enabled = true;
        else if (rigid.velocity.y > 0.1f)
            groundChecker.enabled = false;
    }
}
