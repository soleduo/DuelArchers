using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundMarker : MonoBehaviour {

    public RectTransform rectTrans;

    public RawImage img_inactive;
    public RawImage img_active;

    float xOffset = 45;

    float xPos = 75f;
    float yPos = -110f;

    bool isActive = false;

    public void Init(int index, AnchorPreset anchor, RectTransform parent)
    {
        rectTrans.SetParent(parent);

        if (anchor == AnchorPreset.TopLeft)
        {
            rectTrans.anchorMin = new Vector2(0, 1);
            rectTrans.anchorMax = new Vector2(0, 1);

            rectTrans.anchoredPosition = new Vector2(xPos + (index * xOffset), yPos);
            rectTrans.localScale = new Vector2(1, 1);
        }

        if(anchor == AnchorPreset.TopRight)
        {
            rectTrans.anchorMin = new Vector2(1, 1);
            rectTrans.anchorMax = new Vector2(1, 1);

            rectTrans.anchoredPosition = new Vector2(-1 * (xPos + (index * xOffset)), yPos);
            rectTrans.localScale = new Vector2(1, 1);
        }
        
    }

	public void Activate()
    {
        img_active.color = Color.white;
        isActive = true;
    }

    public void Deactivate()
    {
        img_active.color = Color.clear;
        isActive = false;
    }
    
    
}

public enum AnchorPreset
{
    TopLeft,
    TopRight
}