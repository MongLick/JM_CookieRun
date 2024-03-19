using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // �ڽ� �� �� �ִ��� Ȯ���ϴ� �迭
    public float scrollSpeed = 5; // ����� �����̴� �ӵ�
    [SerializeField] float offset; // ����� �󸶳� �־����� �ٽÿ�����
    public float scrollSpeedFast = 100;
    bool fast2 = false;

    private void Update()
    {
        ScrollerFast();
    }

    private IEnumerator FastCheck()
    {
        yield return new WaitForSeconds(3f);
        fast2 = false;
    }

    public void GetItem()
    {
        fast2 = true;
        StartCoroutine(FastCheck());
    }

    public void ScrollerFast()
    {
        if (fast2 == true)
        {
            for (int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
            {
                children[i].Translate(Vector2.left * scrollSpeedFast * Time.deltaTime, Space.World); // ���� ��� �������� ��������

                if (children[i].position.x < -offset) // ���� �Ÿ� �־����� �ٽ� ����
                {
                    Vector2 pos = new Vector2(offset, children[i].position.y); // ������ �ڸ��� ���
                    children[i].position = pos; // ������ �ڸ��� ���ư�
                }
            }
        }

        if (fast2 == false)
        {
            for (int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
            {
                children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // ���� ��� �������� ��������

                if (children[i].position.x < -offset) // ���� �Ÿ� �־����� �ٽ� ����
                {
                    Vector2 pos = new Vector2(offset, children[i].position.y); // ������ �ڸ��� ���
                    children[i].position = pos; // ������ �ڸ��� ���ư�
                }
            }
        }
    }
}
