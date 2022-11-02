using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.StageEnterPopup
{
    public class StageListItem : MonoBehaviour
    {
        public TextMeshProUGUI Index;
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Desc;
        public GameObject selectObject;
        public Button button;
        public GameObject Icon;

        private Data.Stage.Stage data;
        public void SetData(Data.Stage.Stage stage)
        {
            this.data = stage;
            Title.SetText(stage.Title.ToString());
            Desc.SetText(stage.Desc.ToString());
            var image_icon = Icon.GetComponent<Image>();
            image_icon.sprite = stage.Icon;
        }
        public int GetStageIndex()
        {
            return this.data.Index;
        }
    }
}