using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class SlicedFilledSlider : MonoBehaviour
    {
        public SlicedFilledImage slicedFilledImage;
        float targetValue;
        private void Start()
        {
            targetValue = slicedFilledImage.fillAmount;
        }

        private void LateUpdate()
        {
            if(targetValue != slicedFilledImage.fillAmount)
            {
                slicedFilledImage.fillAmount = Mathf.Lerp(slicedFilledImage.fillAmount,targetValue, Time.deltaTime*5.0f);
            }
        }
        public void SetValue(float value, bool forceApply = false)
        {
            value = Mathf.Clamp01(value);
            if (forceApply == true)
            {
                slicedFilledImage.fillAmount = value;
            }
            targetValue = value;
        }
    }
}