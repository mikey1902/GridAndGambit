using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GridGambitProd;

public class CardMovement : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	private RectTransform rectTransform;
	[SerializeField] private int currentState = 0;
	private Vector3 ogScale;
	private Quaternion ogRotation;
	private Vector3 ogPos;
	private GridManager gridManager;
	public GameObject unitPlaying;

	[SerializeField] private float hoverScale = 1.1f;
	[SerializeField] private GameObject highlightEffect;
	[SerializeField] private GameObject playArrow;

	private LayerMask gridLayerMask;
	private LayerMask unitLayerMask;
	private Card cardData;
	private CardDisplay cardDisplay;
	private HandManager handManager;
	private BattleManager battleManager;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();

		ogScale = rectTransform.localScale;
		ogPos = rectTransform.localPosition;
		ogRotation = rectTransform.localRotation;

		gridManager = FindObjectOfType<GridManager>();
		handManager = FindObjectOfType<HandManager>();
		battleManager = FindObjectOfType<BattleManager>();
		cardDisplay = GetComponent<CardDisplay>();

		gridLayerMask = LayerMask.GetMask("Grid");
		unitLayerMask = LayerMask.GetMask("Units");
		cardData = cardDisplay.cardData;
	}

	void Update()
	{
		if (cardData != cardDisplay.cardData)
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
				HandlePlayState();
				break;
		}
	}

	private void TransitionToState0()
	{
		currentState = 0;
		GameManager.Instance.playingCard = false;
		GameManager.Instance.playingMove = false;
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
			playArrow.SetActive(true);
		}
	}

	private void HandleHoverState()
	{
		highlightEffect.SetActive(true);
		rectTransform.localScale = ogScale * hoverScale;
	}

	private void HandlePlayState()
	{
		if (!GameManager.Instance.playingCard)
		{
			GameManager.Instance.playingCard = true;
		}

		if (!Input.GetMouseButton(0))
		{
			//shoot raycast down from mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (cardData is AttackCard attackCard)
			{
				TryAttackCardPlay(ray, attackCard);
			}
			else if (cardData is MoveCard moveCard)
			{
				TryMoveCardPlay(ray, moveCard);
			}
			else if (cardData is SupportCard supportCard)
			{
				TrySupportCardPlay(ray, supportCard);
			}

			GameManager.Instance.playingCard = false;
			TransitionToState0();
		}
	}

	private void TryAttackCardPlay(Ray ray, AttackCard attackCard)
	{
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);

		if (hit.collider != null && hit.collider.GetComponent<PlayerUnit>())
		{
			if (!GameManager.Instance.playingMove)
			{
				GameManager.Instance.playingMove = true;
			}

			//PUT BATTLE FUNCTION HERE RIZZ!!!
			if(checkRange(hit, attackCard) == true)
			{
				battleManager.AttackCardEffect(attackCard, hit.collider.gameObject);
			}

			handManager.cardsInHand.Remove(gameObject);
			handManager.UpdateHandVisuals();
			handManager.ClearHand();
			handManager.cardsInHand.Clear();
			Destroy(gameObject);
		}
	}

	private void TryMoveCardPlay(Ray ray, MoveCard moveCard)
	{
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);
		RaycastHit2D hit2 = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, gridLayerMask);

		if (hit.collider != null && hit.collider.GetComponent<PlayerUnit>())
		{
			if (!GameManager.Instance.playingMove)
			{
				GameManager.Instance.playingMove = true;
			}

			PlayerUnit unit = hit.collider.GetComponent<PlayerUnit>();
			GridCell cell = hit2.collider.GetComponent<GridCell>();
			battleManager.MoveCardEffect(moveCard, unit.gameObject, cell.gridIndex);
		
			handManager.cardsInHand.Remove(gameObject);
			handManager.UpdateHandVisuals();
			handManager.ClearHand();
			handManager.cardsInHand.Clear();
			Destroy(gameObject);
		}
	}

	private void TrySupportCardPlay(Ray ray, SupportCard supportCard)
	{
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, unitLayerMask);

		if (hit.collider != null && hit.collider.GetComponent<PlayerUnit>())
		{
			if (!GameManager.Instance.playingMove)
			{
				GameManager.Instance.playingMove = true;
			}

			//PUT BATTLE FUNCTION HERE RIZZ!!!
			if (checkRange(hit, supportCard) == true)
			{
				battleManager.SupportCardEffect(supportCard, hit.collider.gameObject);
			}

			handManager.cardsInHand.Remove(gameObject);
			handManager.UpdateHandVisuals();
			handManager.ClearHand();
			handManager.cardsInHand.Clear();
			Destroy(gameObject);
		}
	}

	private bool checkRange(RaycastHit2D hit, Card card)
	{
		float unitDistance = Vector2.Distance(unitPlaying.transform.position, hit.collider.gameObject.transform.position);

		if (unitDistance < 1.5)
		{
			unitDistance = 1;
		}
		else
		{
			unitDistance = (unitDistance / 1.5f) + 1;
			unitDistance = Mathf.Floor(unitDistance);
		}

		if (card is AttackCard attackCard)
		{
			if (unitDistance <= attackCard.range)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if(card is SupportCard supportCard)
		{
			if (unitDistance <= supportCard.range)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
}

