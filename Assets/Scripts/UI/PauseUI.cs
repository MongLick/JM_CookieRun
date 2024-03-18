using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : PopUpUI
{
    public GameObject countDown;

    protected override void Awake()
    {
        base.Awake();

        GetUI<Button>("계속하기").onClick.AddListener(Close);
        GetUI<Button>("그만하기").onClick.AddListener(Close);
    }

    
}

