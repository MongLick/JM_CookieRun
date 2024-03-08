using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    private Image image;

    [SerializeField] private Sprite[] sprites;

    private int index;

    private bool JumpSlide = false;
    private bool SlideJump = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnNone()
    {
        index = 0;
        image.sprite = sprites[index];
        JumpSlide = false;
        SlideJump = false;
    }

    public void OnJump()
    {
        index = 1;
        image.sprite = sprites[index];
        JumpSlide = true;
        SlideJump = false;
    }

    public void OnSlide()
    {
        if (JumpSlide == false)
        {
            index = 2;
            image.sprite = sprites[index];
            SlideJump = true;
        }
        if (JumpSlide == true)
        {
            index = 3;
            image.sprite = sprites[index];
            SlideJump = false;
        }    
    }

    public void ChangeButton(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.None:
                OnNone();
                break;

            case ButtonType.Jump:
                OnJump();
                break;

            case ButtonType.Slide:
                OnSlide();
                break;
        }
    }
}
