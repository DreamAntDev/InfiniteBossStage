using IBS.Resoruce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropRelic : MonoBehaviour
{
    [SerializeField]
    List<Relic> relics;

    private void Awake()
    {

    }
    void OnDropRelic()
    {
        var itemRating = GetChanceResult();
        Debug.Log("Relic Rating : " + itemRating);
        var relic = relics.Find(x => x.Rating == itemRating);
        if(relic is null)
        {
            Debug.Log($"{gameObject.name} Not Relic : {itemRating}");
            return;
        }
        else
        {
            RelicManager.Instance.AddRelic(relic);
            UI.UILoader.Load("RelicRewardPopup", () => UI.UILoader.GetUI<UI.RelicRewardPopup.RelicRewardPopup>("RelicRewardPopup").SetRelicData(relic));
            //Relic ȹ�� �ִϸ��̼� ?!
        }
    }

    //Test
    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            OnDropRelic();
        }
    }

    public static Type.RelicRating GetChanceResult()
    {
        List<int> itemChanceIdx = new List<int>();

        while(itemChanceIdx.Count < 11)
        {
            int random = Random.Range(0, 101);
            if (itemChanceIdx.Count > 0 && itemChanceIdx.Contains(random))
            {
                continue;
            }
            else
            {
                itemChanceIdx.Add(random);
            }
        }


        int chanceIdx = Random.Range(0, 101);
        if (!itemChanceIdx.Contains(chanceIdx))
        {
            return Type.RelicRating.Normal;
        }
        else if(itemChanceIdx[0] == chanceIdx)
        {
            return Type.RelicRating.Unique;
        }
        else
        {
            return Type.RelicRating.Epic;
        }
    }
}
