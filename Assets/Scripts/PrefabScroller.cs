using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // �ڽ� �� �� �ִ��� Ȯ���ϴ� �迭
    public static float scrollSpeed = 5; // ����� �����̴� �ӵ�

    private void Update()
    {
        for (int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
        {
            children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // ���� ��� �������� ��������
        }
    }
}
