using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // 자식 몇 개 있는지 확인하는 배열
    public float scrollSpeed = 5; // 배경이 움직이는 속도
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

        // 코루틴이 완료된 후에 부모 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    public void PrefabScrollerFast()
    {
        if (fast == true)
        {
            for (int i = 0; i < children.Length; i++) // 자식 배열 다 꺼냄
            {
                Debug.Log("빨라진 속도");
                children[i].Translate(Vector2.left * scrollSpeedFast * Time.deltaTime, Space.World); // 꺼낸 배경 왼쪽으로 움직여줌
            }
        }

        if (fast == false)
        {
            for (int i = 0; i < children.Length; i++) // 자식 배열 다 꺼냄
            {
                children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // 꺼낸 배경 왼쪽으로 움직여줌
            }
        }
    }
}
