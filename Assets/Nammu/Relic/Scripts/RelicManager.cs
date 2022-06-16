using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelicManager : Singleton<RelicManager>
{
    [SerializeField]
    public List<Relic> relics;

    [SerializeField]
    protected int limitRelicCount;

    List<Relic> haveRelics;

    private List<Relic> activeRelicList = new List<Relic>();

    public List<Relic> Relics
    {
        get { InitRelic(); return haveRelics; }
    }

    public List<Relic> ActiveRelics
    {
        get => activeRelicList;
    }

    private void Awake()
    {
        haveRelics = new List<Relic>();
    }

    private void Start()
    {
        InitRelic();
    }

    private void InitRelic()
    {
        int count = PlayerPrefs.GetInt(RelicDefine.RelicCount, 0);

        for (int i = 1; i <= count; i++)
        {
            int id = PlayerPrefs.GetInt(RelicDefine.InvenRelic + i);

            var relic = relics.Find(x => x.ID == id);

            if (relic != null)
            {
                haveRelics.Add(relic);
            }
            else
            {
                Debug.Log($"해당 itemID:{id}의 유물이 존재 하지 않습니다.");
            }
        }
    }

    public void AddRelic(Relic relic)
    {
        int count = PlayerPrefs.GetInt(RelicDefine.RelicCount, 0) + 1;
        PlayerPrefs.SetInt(RelicDefine.RelicCount, count);
        PlayerPrefs.SetInt(RelicDefine.InvenRelic + count, relic.ID);
    }

    public void SelectRelic(Relic relic)
    {
        if(activeRelicList.Count >= limitRelicCount)
            return;

        if (activeRelicList.Count(x => x.ID == relic.ID) >= 1)
            return;

        activeRelicList.Add(relic);
        Debug.Log("Select Relic Count : " + activeRelicList.Count);
    }

    public List<Relic> ActivePlayerRelic()
    {
        List<Relic> relics = new List<Relic>();
        foreach(var relic in activeRelicList)
        {
            if(relic.ApplyType == IBS.Resoruce.Type.RelicApplyType.Player)
            {
                relics.Add(relic);
                continue;
            }
        }
        return relics;
    }
    public List<Relic> ActiveBossRelic()
    {
        List<Relic> relics = new List<Relic>();
        foreach (var relic in activeRelicList)
        {
            if (relic.ApplyType == IBS.Resoruce.Type.RelicApplyType.Boss)
            {
                relics.Add(relic);
                continue;
            }
        }
        return relics;
    }
}
