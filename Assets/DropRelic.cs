using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRelic : MonoBehaviour
{
    [SerializeField]
    Relic[] relics;

    [SerializeField]
    CanvasGroup canvasGroup;
    
    void OnDropRelic()
    {
        canvasGroup.alpha = 1;
    }
}
