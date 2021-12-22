using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickHighlighter : MonoBehaviour
{
    public RectTransform stick;
    public List<GameObject> highLightObjects = new List<GameObject>();
    private int intervalValue = 90;
    
    private void Awake()
    {
        if (highLightObjects.Count > 0)
        {
            this.intervalValue = 360 / highLightObjects.Count;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(stick.anchoredPosition.Equals(Vector2.zero))
        {
            for (int i = 0; i < highLightObjects.Count; i++)
            {
                highLightObjects[i].SetActive(false);
            }
            return;
        }
        var dotValue = Vector2.Dot(Vector2.right, stick.anchoredPosition.normalized); // dot value is cos value
        var theta = Mathf.Acos(dotValue);
        if(stick.anchoredPosition.y < 0) // 작은 각을 구하기 때문에 y가 x축 밑에 있는 경우 360 - theta로 회전 값을 구한다.
        {
            theta = 2 * Mathf.PI - theta;
        }
        var degree = Mathf.Rad2Deg * theta;
        var idx = (int)degree / intervalValue;
        for (int i = 0; i < highLightObjects.Count; i++)
        {
            highLightObjects[i].SetActive(i==idx);
        }
    }
}
