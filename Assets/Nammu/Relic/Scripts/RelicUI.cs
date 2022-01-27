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

    [SerializeField]
    CanvasGroup relicCanvasGroup;

    public void ShowUI()
    {
        relicCanvasGroup.alpha = 1;
    }

    public void CloseUI()
    {
        relicCanvasGroup.alpha = 0;
    }

    public void SetRelicData(Relic relic)
    {
        relicImage.sprite = relic.Sprite;
        relicContext.text = relic.Context;
        ShowUI();
    }
}
