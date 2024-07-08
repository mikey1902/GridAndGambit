using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GridGambitProd;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	private RectTransform rectTransform;
	private Canvas canvas;
	private RectTransform canvsRectTransform;
	private int currentState = 0;
	private Vector3 ogScale;
	private Quaternion ogRotation;
	private Vector3 ogPos;
	private GridManager gridManager;

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

	private LayerMask gridLayerMask;
	private LayerMask unitLayerMask;
	private Card cardData;
	private CardDisplay cardDisplay;
	HandManager handManager;

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
		gridManager = FindObjectOfType<GridManager>();
		handManager = FindObjectOfType<HandManager>();
		cardDisplay = GetComponent<CardDisplay>();

		gridLayerMask = LayerMask.GetMask("Grid");
		unitLayerMask = LayerMask.GetMask("Units");
		cardData = cardDisplay.cardData;
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

		if(cardData != cardDisplay.cardData)
		{
			cardData = cardDisplay.cardData;
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

		if(!Input.GetMouseButton(0))
		{
			//shoot raycast down from mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(cardData is UnitCard unitCard)
			{
				TryUnitCardPlay(ray, unitCard);
			}
			else if (cardData is SpellCard spellCard)
			{
				TrySpellCardPlay(ray, spellCard);
			}

			TransitionToState0();
		}

		if (Input.mousePosition.y < cardPlay.y)
		{
			currentState = 2;
			playArrow.SetActive(false);
		}
	}

	private void TryUnitCardPlay(Ray ray, UnitCard unitCard)
	{
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, gridLayerMask);

		//check if gridcell has been hit
		if (hit.collider != null && hit.collider.GetComponent<GridCell>())
		{
			GridCell cell = hit.collider.GetComponent<GridCell>();
			Vector2 cellPos = cell.gridIndex;

			if (gridManager.AddObjectToGrid(unitCard.unitPrefab, cellPos))
			{
				handManager.cardsInHand.Remove(gameObject);
				//discardManager.AddToDiscard(cardData);
				handManager.UpdateHandVisuals();
				Debug.Log("spawned character");
				Destroy(gameObject);
			}
		}
	}

	private void TrySpellCardPlay(Ray ray, SpellCard spellCard)
	{
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);

		if (hit.collider != null)
		{
			handManager.cardsInHand.Remove(gameObject);
			//discardManager.AddToDiscard(cardData);
			handManager.UpdateHandVisuals();
			Debug.Log("played spell");
			Destroy(gameObject);
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

