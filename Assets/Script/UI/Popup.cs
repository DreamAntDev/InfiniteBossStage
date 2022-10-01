using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Popup : MonoBehaviour
    {
        public bool isNeedBackgroun = true;

        public virtual void Enable()
        {
            GameObject bg = new GameObject("Dim");
            bg.layer = this.gameObject.layer;
            bg.transform.SetParent(this.gameObject.transform);
            bg.transform.localScale = Vector3.one;
            bg.transform.SetAsFirstSibling();

            var image = bg.AddComponent<Image>();
            image.rectTransform.anchorMin = Vector2.zero;
            image.rectTransform.anchorMax = Vector2.one;
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.sizeDelta = Vector2.zero;
            image.color = new Color(0, 0, 0, 0.4f);
            bg.AddComponent<Button>();
        }

        public abstract void SetBackButton();
    }
}