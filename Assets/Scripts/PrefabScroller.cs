using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScroller : MonoBehaviour
{
    [SerializeField] Transform[] children; // 자식 몇 개 있는지 확인하는 배열
    public static float scrollSpeed = 5; // 배경이 움직이는 속도

    private void Update()
    {
        for (int i = 0; i < children.Length; i++) // 자식 배열 다 꺼냄
        {
            children[i].Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World); // 꺼낸 배경 왼쪽으로 움직여줌
        }
    }
}
