using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.StageEnterPopup
{
    public class StageListItem : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public GameObject selectObject;
        public Button button;

        private Data.Stage.Stage data;
        public void SetData(Data.Stage.Stage stage)
        {
            this.data = stage;
            text.SetText(stage.Index.ToString());
        }
        public int GetStageIndex()
        {
            return this.data.Index;
        }
    }
}