using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRelicManager : MonoBehaviour
{
    private List<Relic> relicList;

    private void Awake()
    {
        relicList = new List<Relic>();    
    }

    void Start()
    {
        ActiveRelic();
    }

    public void ActiveRelic()
    {
        //Relic ������ �޾ƿ���


    }

    public void GetRelic(Relic relic)
    {
        if(relicList.Count <= 3)
        {
            relicList.Add(relic);
        }
        else { 
        }

        //RelicInven �߰�


        foreach(var relicData in relicList)
        {
            Debug.Log(relicData.name+"\n");
        }
    }
}
