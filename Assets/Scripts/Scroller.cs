using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // 자식 몇 개 있는지 확인하는 배열
    public float scrollSpeed = 5; // 배경이 움직이는 속도
    [SerializeField] float offset; // 배경이 얼마나 멀어지면 다시오는지
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
            for (int i = 0; i < children.Length; i++) // 자식 배열 다 꺼냄
            {
                children[i].Translate(Vector2.left * scrollSpeedFast * Time.deltaTime, Space.World); // 꺼낸 배경 왼쪽으로 움직여줌

                if (children[i].position.x < -offset) // 일정 거리 멀어지면 다시 실행
                {
                    Vector2 pos = new Vector2(offset, children[i].position.y); // 오프셋 자리를 기억
                    children[i].position = pos; // 오프셋 자리로 돌아감
                }
            }
        }

        if (fast2 == false)
        {
            for (int i = 0; i < children.Length; i++) // 자식 배열 다 꺼냄
            {
                children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // 꺼낸 배경 왼쪽으로 움직여줌

                if (children[i].position.x < -offset) // 일정 거리 멀어지면 다시 실행
                {
                    Vector2 pos = new Vector2(offset, children[i].position.y); // 오프셋 자리를 기억
                    children[i].position = pos; // 오프셋 자리로 돌아감
                }
            }
        }
    }
}
