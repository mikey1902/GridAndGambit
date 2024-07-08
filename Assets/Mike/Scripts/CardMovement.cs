using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	private RectTransform rectTransform;
	private Canvas canvas;
	private RectTransform canvsRectTransform;
	private int currentState = 0;
	private Vector3 ogScale;
	private Quaternion ogRotation;
	private Vector3 ogPos;


	[SerializeField] private float hoverScale = 1.1f;
	[SerializeField] private Vector2 cardPlay;
	[SerializeField] private Vector3 playPosition;
	[SerializeField] private GameObject highlightEffect;
	[SerializeField] private GameObject playArrow;
	[SerializeField] private float dragDelay = 0.1f;

	//Check play position/mouse position is same no matter the resolution
	[SerializeField] private int cardPlayDivider = 4;
	[SerializeField] private float cardPlayMultiplier = 1f;
	[SerializeField] private bool needUpdateCardPlayPosition = false;
	[SerializeField] private int playPositionYDivider = 2;
	[SerializeField] private float playPositionYMultiplier = 1f;	
	[SerializeField] private int playPositionXDivider = -3;
	[SerializeField] private float playPositionXMultiplier = 1.2f;
	[SerializeField] private bool needUpdatePlayPosition = false;


	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();

		if(canvas != null)
		{
			canvsRectTransform = canvas.GetComponent<RectTransform>();
		}

		ogScale = rectTransform.localScale;
		ogPos = rectTransform.localPosition;
		ogRotation = rectTransform.localRotation;

		UpdateCardPlayPosition();
		UpdatePlayPosition();
	}

	void Update()
	{
		if (needUpdateCardPlayPosition)
		{
			UpdateCardPlayPosition();
		}

		if(needUpdatePlayPosition)
		{
			UpdatePlayPosition();
		}

		//handles states
		switch (currentState)
		{
			case 1:
				HandleHoverState();
				break;
			case 2:
				HandleDragState();
				if (!Input.GetMouseButton(0))
				{
					TransitionToState0();
				}
				break;
			case 3:
				HandlePlayState();
				if (!Input.GetMouseButton(0))
				{
					TransitionToState0();
				}
				break;
		}
	}

	private void TransitionToState0()
	{
		currentState = 0;
		//reset scale, rotation and position
		rectTransform.localScale = ogScale;
		rectTransform.localPosition = ogPos;
		rectTransform.localRotation = ogRotation;
		highlightEffect.SetActive(false);
		playArrow.SetActive(false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (currentState == 0)
		{
			ogScale = rectTransform.localScale;
			ogPos = rectTransform.localPosition;
			ogRotation = rectTransform.localRotation;

			currentState = 1;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (currentState == 1)
		{
			TransitionToState0();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (currentState == 1)
		{
			currentState = 2;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (currentState == 2)
		{	
			if (rectTransform.localPosition.y > cardPlay.y)
			{
				currentState = 3;
				playArrow.SetActive(true);
				rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, dragDelay);
			}
		}
	}

	private void HandleHoverState()
	{
		highlightEffect.SetActive(true);
		rectTransform.localScale = ogScale * hoverScale;
	}

	private void HandleDragState()
	{
		rectTransform.localRotation = Quaternion.identity;
		rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, dragDelay);
	}

	private void HandlePlayState()
	{
		rectTransform.localPosition = playPosition;
		rectTransform.localRotation = Quaternion.identity;

		if (Input.mousePosition.y < cardPlay.y)
		{
			currentState = 2;
			playArrow.SetActive(false);
		}
	}

	private void UpdateCardPlayPosition()
	{
		if(cardPlayDivider != 0 && canvsRectTransform != null)
		{
			float segment = cardPlayMultiplier / cardPlayDivider;

			cardPlay.y = canvsRectTransform.rect.height * segment;
		}
	}

	private void UpdatePlayPosition()
	{
		if(canvsRectTransform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
		{
			float segmentX = playPositionXMultiplier / playPositionXDivider;
			float segmentY = playPositionYMultiplier / playPositionYDivider;

			playPosition.x = canvsRectTransform.rect.width * segmentX;
			playPosition.y = canvsRectTransform.rect.height * segmentY;
		}
	}
}

