using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // �ڽ� �� �� �ִ��� Ȯ���ϴ� �迭
    [SerializeField] float scrollSpeed; // ����� �����̴� �ӵ�
    [SerializeField] float offset; // ����� �󸶳� �־����� �ٽÿ�����?

    private void Update()
    {
        for(int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
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
