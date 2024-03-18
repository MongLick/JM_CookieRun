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

        GetUI<Button>("����ϱ�").onClick.AddListener(Close);
        GetUI<Button>("�׸��ϱ�").onClick.AddListener(Close);
    }

    
}

