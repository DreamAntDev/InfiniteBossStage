using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace UI.RelicRewardPopup
{
    public class RelicRewardPopup : MonoBehaviour
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
            gameObject.SetActive(false);
            GameManager.Instance.StageIndex = 0;
            Static.StageManager.Instance.UnloadStage();
        }

        public void CloseUI()
        {
            gameObject.SetActive(false);
        }

        public void SetRelicData(Relic relic)
        {
            relicImage.sprite = relic.Sprite;
            relicContext.text = relic.Context;
            ShowUI();
        }
    }
}