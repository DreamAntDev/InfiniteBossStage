using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : Singleton<RelicManager>
{
    [SerializeField]
    private List<Relic> relics;

    List<Relic> haveRelics;

    //private readonly Dictionary<int, Relic> activeRelic = new Dictionary<int, Relic>();

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

            if(relic != null)
            {
                haveRelics.Add(relic);
            }
            else
            {
                Debug.Log($"해당 itemID:{id}의 유물이 존재 하지 않습니다.");
            }
        }
    }
}
