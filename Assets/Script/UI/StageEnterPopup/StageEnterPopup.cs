using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StageEnterPopup
{
    public class StageEnterPopup : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public StageListItem listItem;
        public Button enterButton;
        public Button closeButton;
        public Button dim; // 팝업에 기본으로 BG깔리게 해야할 듯? 나중에 수정

        private StageListItem selectedItem = null;
        private void Start()
        {
            SetList();
            this.enterButton.onClick.AddListener(() => Enter());
            this.closeButton.onClick.AddListener(() => Close());
        }

        private void SetList()
        {
            //StageData 로딩방식 변경후에 나중에 수정
            foreach(var stage in Data.Stage.Instance.Container)
            {
                var stageListItem = GameObject.Instantiate(this.listItem.gameObject, scrollRect.content.transform).GetComponent<StageListItem>();
                stageListItem.SetData(stage.Value);
                stageListItem.button.onClick.AddListener(() => OnClick(stageListItem));
            }
            var list = this.scrollRect.content.transform.GetComponentsInChildren<StageListItem>();
            OnClick(list[0]);
        }
        private void OnClick(StageListItem item)
        {
            var list = this.scrollRect.content.transform.GetComponentsInChildren<StageListItem>();
            foreach (var listItem in list)
            {
                listItem.selectObject.SetActive(listItem == item);
            }
            this.selectedItem = item;
        }
        private void Close()
        {
            UI.UILoader.Unload("StageEnterPopup");
        }
        private void Enter()
        {
            Static.StageManager.Instance.LoadStage(this.selectedItem.GetStageIndex());
            Close();
        }
    }
}