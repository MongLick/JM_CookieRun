using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGaugeEff : MonoBehaviour
{
    public Image img;
    public GameObject eff;
    public float value = 2;
    private float width;

    private void Start()
    {
        this.width = this.img.GetComponent<RectTransform>().rect.width;
    }

    private void Update()
    {
        var pos = this.eff.transform.localPosition;
        pos.x = this.img.fillAmount * this.width - 12 - 2; // 12 : eff¿« ≥–¿Ã¿« π›, 2 : offset
        this.eff.transform.localPosition = pos;
    }
}
