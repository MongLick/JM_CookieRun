using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // �ڽ� �� �� �ִ��� Ȯ���ϴ� �迭
    public float scrollSpeed = 5; // ����� �����̴� �ӵ�
    public float scrollSpeedFast = 100f;
    [SerializeField] bool fast = false;

    private void Update()
    {
        PrefabScrollerFast();
    }

    public void GetItem()
    {
        Debug.Log("GetItem");

        StartCoroutine(FastCheck());
    }

    private IEnumerator FastCheck()
    {
        fast = true;
        yield return new WaitForSeconds(3f);
        fast = false;

        // �ڷ�ƾ�� �Ϸ�� �Ŀ� �θ� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    public void PrefabScrollerFast()
    {
        if (fast == true)
        {
            for (int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
            {
                Debug.Log("������ �ӵ�");
                children[i].Translate(Vector2.left * scrollSpeedFast * Time.deltaTime, Space.World); // ���� ��� �������� ��������
            }
        }

        if (fast == false)
        {
            for (int i = 0; i < children.Length; i++) // �ڽ� �迭 �� ����
            {
                children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // ���� ��� �������� ��������
            }
        }
    }
}
