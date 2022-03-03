using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicUI : MonoBehaviour
{
    [SerializeField]
    Image relicImage;

    [SerializeField]
    TextMeshProUGUI relicContext;

    private void Start()
    {
        CloseUI();
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void SetRelicData(Relic relic)
    {
        relicImage.sprite = relic.Sprite;
        relicContext.text = relic.Context;
        ShowUI();
    }
}
