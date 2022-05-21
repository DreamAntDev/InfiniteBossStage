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
            bg.transform.SetAsFirstSibling();

            var image = bg.AddComponent<Image>();
            image.rectTransform.anchorMin = new Vector2(0, 0);
            image.rectTransform.anchorMax = new Vector2(1, 1);
            image.rectTransform.anchoredPosition = new Vector2(0, 0);
            image.rectTransform.sizeDelta = new Vector2(0, 0);
            image.color = new Color(0, 0, 0, 0.4f);
            bg.AddComponent<Button>();
        }
    }
}