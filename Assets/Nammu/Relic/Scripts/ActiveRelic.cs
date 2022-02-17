using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRelic : MonoBehaviour
{
    private List<Relic> relicList;

    private void Awake()
    {
        relicList = new List<Relic>();    
    }

    void Start()
    {
        Active();
    }

    public void Active()
    {
        //Relic 데이터 받아오기


    }

    public void GetRelic(Relic relic)
    {
        if(relicList.Count <= 3)
        {
            relicList.Add(relic);
        }
        else { 
        }
        //RelicInven 추가


        foreach(var relicData in relicList)
        {
            Debug.Log(relicData.name+"\n");
        }
    }
}
