using IBS.Resoruce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRelic : MonoBehaviour
{
    [SerializeField]
    List<Relic> relics;

    [SerializeField]
    RelicUI relicUI;

    void OnDropRelic()
    {
        var itemRating = GetChanceResult();
        var relic = relics.Find(x => x.Rating == itemRating);
        if(relic is null)
        {
            Debug.Log($"{gameObject.name} Not Relic : {itemRating}");
            return;
        }
        else
        {
            relicUI.SetRelicData(relic);
        }
    }

    private Type.RelicRating GetChanceResult()
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
