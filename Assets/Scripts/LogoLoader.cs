using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Countdown");
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2);
        GameObject gameObject = transform.GetChild(1).gameObject;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(1);
    }
}
