using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    public void TitleLoad()
    {
        Manager.Scene.LoadScene("TitleScene");
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
