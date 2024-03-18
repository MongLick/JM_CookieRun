using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject countDown;

    private bool coroutineEnd = true;

    public void OpenCountDown()
    {
        if(coroutineEnd == true)
        {
            Time.timeScale = 0f;
            StartCoroutine(GameStart());
        }
        coroutineEnd = true;
    }

    private IEnumerator GameStart()
    {
        coroutineEnd = false;
        countDown.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown.transform.GetChild(0).gameObject.SetActive(false);
        countDown.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown.transform.GetChild(1).gameObject.SetActive(false);
        countDown.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown.transform.GetChild(2).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
