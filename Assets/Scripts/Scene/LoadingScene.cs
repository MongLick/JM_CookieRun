using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    private void Start()
    {
        StartCoroutine(LoadingRoutine());
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameObject gameObject = transform.GetChild(1).gameObject;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Manager.Scene.LoadScene("TitleScene");
    }

    public override void OnLoadingEnd()
    {
        GameObject gameObject = transform.GetChild(1).gameObject;
        gameObject.SetActive(false);
        GameObject gameObject2 = transform.GetChild(2).gameObject;
        gameObject2.SetActive(true);
        GameObject gameObject3 = transform.GetChild(3).gameObject;
        gameObject3.SetActive(true);
    }
}
