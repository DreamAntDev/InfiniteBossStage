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
        get { MyRelic(); return haveRelics; }
    }

    public List<Relic> ActiveRelics
    {
        get => activeRelicList;
    }

    private void Awake()
    {
        haveRelics = new List<Relic>();
        InitRelic();
    }

    private void InitRelic()
    {
        int count = PlayerPrefs.GetInt(RelicDefine.ActiveRelicCount, 0);
        
        for(int i = 0; i < count; i++) { 
            var relicID = PlayerPrefs.GetInt(RelicDefine.ActiveRelic + count);
            var relic = relics.Find(x => x.ID == relicID);
            activeRelicList.Add(relic);
        }
        Debug.Log("ActiveRelic:" + activeRelicList.Count);
    }

    private void MyRelic()
    {
        int count = PlayerPrefs.GetInt(RelicDefine.InvenRelicCount, 0);
        Debug.Log("Count:" + count);
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
        int count = PlayerPrefs.GetInt(RelicDefine.InvenRelicCount, 0) + 1;
        PlayerPrefs.SetInt(RelicDefine.InvenRelicCount, count);
        PlayerPrefs.SetInt(RelicDefine.InvenRelic + count, relic.ID);
    }

    public void SelectRelic(Relic relic)
    {
        if (activeRelicList.Count(x => x.ID == relic.ID) >= 1)
            return;

        if (activeRelicList.Count >= limitRelicCount)
        {
            activeRelicList.RemoveAt(0);
        }
        activeRelicList.Add(relic);
        SaveRelicData(relic);
        Debug.Log("Select Relic Count : " + activeRelicList.Count);
    }

    private void SaveRelicData(Relic relic)
    {
        int count = PlayerPrefs.GetInt(RelicDefine.ActiveRelicCount, 0) + 1;
        if (count == 4)
        {
            count = 3;
            PlayerPrefs.SetInt(RelicDefine.ActiveRelic + (count - 2),
                PlayerPrefs.GetInt(RelicDefine.ActiveRelic + (count - 1)));
            PlayerPrefs.SetInt(RelicDefine.ActiveRelic + (count - 1),
                PlayerPrefs.GetInt(RelicDefine.ActiveRelic + count));
        }

        PlayerPrefs.SetInt(RelicDefine.ActiveRelicCount, count);
        PlayerPrefs.SetInt(RelicDefine.ActiveRelic + count, relic.ID);
    }

    public void DeSelectRelic(Relic relic)
    {
        var removeRelic = activeRelicList.Find(x => x.ID == relic.ID);
        activeRelicList.Remove(removeRelic);
        Debug.Log("DeSelectRelic ActiveRelic:" + activeRelicList.Count);
        PlayerPrefs.SetInt(RelicDefine.ActiveRelicCount, activeRelicList.Count);
    }

    public List<Relic> ActivePlayerRelic()
    {
        return activeRelicList.FindAll(x => x.ApplyType == IBS.Resoruce.Type.RelicApplyType.Player);
    }

    public List<Relic> ActiveBossRelic()
    {
        return activeRelicList.FindAll(x => x.ApplyType == IBS.Resoruce.Type.RelicApplyType.Boss);
    }
}
