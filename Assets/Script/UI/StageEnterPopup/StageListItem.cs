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

        private int data;
        public void SetData(int i) // ���߿� ����
        {
            this.data = i;
            text.SetText(i.ToString());
        }
        public int GetData()
        {
            return this.data;
        }
    }
}