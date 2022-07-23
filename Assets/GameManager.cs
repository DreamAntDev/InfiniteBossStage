using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Looby,
        Stage,
    }

    public GameState state { get; private set; }

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
        Static.SoundManager.Instance.PlaySound("Sound/Lobby", Static.SoundManager.SoundType.BGM);
    }

    void Start()
    {
        stageIdx = PlayerPrefs.GetInt(STAGE_INDEX, 1);   
    }
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.xKey.wasPressedThisFrame)
        {
            OnLobby();
        }
    }
    public void OnStage(int stageIndex)
    {
        Static.StageManager.Instance.LoadStage(stageIndex);
        this.state = GameState.Stage;
    }

    public void OnLobby()
    {
        Static.CameraManager.Instance.OnLobby();
        var targetTraceCamera = Static.CameraManager.Instance.MainCamera?.GetComponent<Component.TargetTraceCamera>();
        if (targetTraceCamera != null)
        {
            targetTraceCamera.target = null;
        }
        Static.StageManager.Instance.UnloadStage();
        Static.SoundManager.Instance.PlaySound("Sound/Lobby", Static.SoundManager.SoundType.BGM);
        this.state = GameState.Looby;
    }
}
