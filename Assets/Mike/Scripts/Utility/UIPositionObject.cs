using System;
using UnityEngine;

public class UIObjectPositioner : MonoBehaviour
{
    //set UI position so it is compatible with diffent screen sizes
    public RectTransform objectToPosition;

    public int widthDivider = 2;
    public int heightDivider = 2;
    public float widthMultiplier = 1f;
    public float heightMultiplier = 1f;

    public bool updatePosition = false;

    void Start()
    {
        SetUIObjectPosition();
    }

    void Update()
    {
        if(updatePosition)
		{
            SetUIObjectPosition();
        }
    }

	public void SetUIObjectPosition()
	{
		if(objectToPosition != null && widthDivider != 0 && heightDivider != 0)
		{
            //calculate anchor position
            float anchorX = widthMultiplier / widthDivider;
            float anchorY = heightMultiplier / heightDivider;

            //set anchor and pivot
            objectToPosition.anchorMin = new Vector2(anchorX, anchorY);
            objectToPosition.anchorMax = new Vector2(anchorX, anchorY);
            objectToPosition.pivot = new Vector2(0.5f, 0.5f);

            //set local position to 0 to align with anchor
            objectToPosition.anchoredPosition = Vector2.zero;
		}
	}
}
