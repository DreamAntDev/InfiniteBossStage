using System;
using System.Collections;
using System.Collections.Generic;
using UI.Lobby;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RelicIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected Image relicImage;

    private Relic currentRelic;

    public event Action<Relic> OnItemClick;

    public void SetRelicIcon(Relic relic)
    {
        currentRelic = relic;
        relicImage.sprite = relic.Sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnItemClick != null)
        {
            OnItemClick.Invoke(currentRelic);
        }
    }
}
