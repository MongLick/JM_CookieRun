using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    bool OnClick = true;
    public void TitleSceneLoad()
    {
        if (OnClick == true)
        {
            Time.timeScale = 1f;
            OnLoadingEnd();
            StartCoroutine(LoadingRoutine());
            Manager.Scene.LoadScene3("TitleScene");
            OnClick = false;
        }
    }

    public override void OnLoadingEnd()
    {
        GameObject gameObject = transform.GetChild(0).gameObject;
        gameObject.SetActive(true);
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return new WaitForSeconds(2f);
    }
}
