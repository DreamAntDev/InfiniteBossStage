using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int stageIdx = 1;
    #region ConstString
    public readonly string STAGE_INDEX = "StageIndex";
    #endregion
    #region Property
    public int StageIndex
    {
        get => stageIdx;
        set => PlayerPrefs.SetInt(STAGE_INDEX, ++stageIdx);
    }
    #endregion

    private void Awake()
    {
    }

    void Start()
    {
        stageIdx = PlayerPrefs.GetInt(STAGE_INDEX, 1);   
    }
}
