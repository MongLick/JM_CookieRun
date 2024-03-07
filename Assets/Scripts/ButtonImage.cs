using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    private Image image;

    [SerializeField] private Sprite[] sprites;

    private int index;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return;

            index = 0;
            image.sprite = sprites[index];
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            index = 1;
            image.sprite = sprites[index];
        }

        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKey(KeyCode.Space))
                return;

            index = 2;
            image.sprite = sprites[index];
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            index = 3;
            image.sprite = sprites[index];
        }
    }
}
