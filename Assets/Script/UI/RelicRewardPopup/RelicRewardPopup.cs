using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace UI.RelicRewardPopup
{
    public class RelicRewardPopup : UI.Popup
    {
        [SerializeField]
        Image relicImage;

        [SerializeField]
        TextMeshProUGUI relicContext;

        private void Start()
        {
            //CloseUI();
        }

        public void ShowUI()
        {
            gameObject.SetActive(true);
            StartCoroutine(RelicTimer());
        }

        IEnumerator RelicTimer()
        {
            yield return new WaitForSeconds(5f);
            GameManager.Instance.StageIndex = 0;
            Static.StageManager.Instance.UnloadStage();
            CloseUI();
        }

        public void CloseUI()
        {
            UI.UILoader.Unload("RelicRewardPopup");
        }

        public void SetRelicData(Relic relic)
        {
            relicImage.sprite = relic.Sprite;
            relicContext.text = relic.Context;
            ShowUI();
        }
    }
}