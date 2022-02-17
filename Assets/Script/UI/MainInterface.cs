using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainInterface
{
    public class MainInterface : MonoBehaviour
    {
        static MainInterface instance = null;
        public static MainInterface Instance
        {
            get { return MainInterface.instance; }
        }

        public Common.UI.SlicedFilledSlider hpSlider;
        public Common.UI.SlicedFilledSlider staminaSlider;
        public Common.UI.SlicedFilledSlider bossHpSlider;

        private void Awake()
        {
            MainInterface.instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.UICamera;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}