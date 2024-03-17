using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    bool OnClick = true;
    public void GameSceneLoad()
    {
        if(OnClick == true)
        {
            Manager.Scene.LoadScene2("GameScene");
            OnClick = false;
        }
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
