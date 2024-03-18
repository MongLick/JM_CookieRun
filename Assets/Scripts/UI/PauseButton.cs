using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] PauseUI pauseUIPrefab;
    [SerializeField] GameObject countDown;

    public void Click()
    {
        PauseUI pauseUI = Manager.UI.ShowPopUpUI(pauseUIPrefab);
        pauseUI.countDown = countDown;
    }
}
