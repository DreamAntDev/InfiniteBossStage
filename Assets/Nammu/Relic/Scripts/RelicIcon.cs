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
    [SerializeField]
    protected TMPro.TextMeshProUGUI text_itemCount;
    private int itemCount = 1;

    private Relic currentRelic;

    public event Action<RelicIcon> OnItemClick;

    public GameObject point = null;

    public Relic Relic
    {
        get => currentRelic;
    }

    public int ItemCount
    {
        get => itemCount;
    }

    public void SetRelicIcon(Relic relic)
    {
        currentRelic = relic;
        relicImage.sprite = relic.Sprite;
    }

    public void UpdateItemCount()
    {
        itemCount++;
        if (!text_itemCount.gameObject.activeSelf)
        {
            if(itemCount > 1)
            {
                text_itemCount.gameObject.SetActive(true);
            }
        }
        text_itemCount.text = itemCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnItemClick != null)
        {
            OnItemClick.Invoke(this);
        }
    }

    public bool Equals(Relic relic) 
    {
        if(currentRelic == relic)
        {
            return true;
        }

        return false;
    }
}
