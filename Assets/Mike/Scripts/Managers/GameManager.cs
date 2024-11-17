using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
	START,
	PLAYER,
	ENEMY,
	WON,
	LOST
}

public enum PlayerState
{
	Move,
	Action
}

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public OptionsManager OptionsManager { get; private set; }
	public AudioManager AudioManager { get; private set; }

	//prefabs for spawning on start
	//public GameObject enemyPrefab;
	//public GameObject[] PiecePrefabs;

	public GameState gameState;
	public PlayerState playerState;
	public TMP_Text stateText;
	public TMP_Text playerStateText;
	public bool playingCard = false;
	public bool playingMove = false;
	public List<PlayerUnit> playerPieces;
	public ToySoldierBTree[] enemyPieces;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			InitializeManagers();
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		PlayerUnit[] pieces = (FindObjectsOfType<PlayerUnit>());
		foreach(PlayerUnit unit in pieces)
		{
			playerPieces.Add(unit);
		}
		enemyPieces = FindObjectsOfType<ToySoldierBTree>();
	}

	private void InitializeManagers()
	{
		OptionsManager = GetComponentInChildren<OptionsManager>();
		AudioManager = GetComponentInChildren<AudioManager>();

		if(OptionsManager == null)
		{
			GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
			if(prefab == null)
			{
				Debug.Log($"OptionsManager prefab not found");
			}
			else
			{
				Instantiate(prefab, transform.position, Quaternion.identity, transform);
				OptionsManager = GetComponentInChildren<OptionsManager>();
			}
		}
		if (AudioManager == null)
		{
			GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
			if (prefab == null)
			{
				Debug.Log($"AudioManager prefab not found");
			}
			else
			{
				Instantiate(prefab, transform.position, Quaternion.identity, transform);
				AudioManager = GetComponentInChildren<AudioManager>();
			}
		}
	}

	private void Start()
	{
		gameState = GameState.START;
		playerState = PlayerState.Move;
		stateText.text = gameState.ToString();
		playerStateText.text = playerState.ToString();

		foreach(ToySoldierBTree enemy in enemyPieces)
		{
			enemy.enabled = false;
		}
		SetupCombat();
	}

	private void SetupCombat()
	{
		//spawn pieces on level start

		gameState = GameState.PLAYER;
		stateText.text = gameState.ToString();
	}

	public void StartPlayerTurn()
	{
		gameState = GameState.PLAYER;
		stateText.text = gameState.ToString();

		foreach(PlayerUnit unit in playerPieces)
		{
			unit.moveReady = true;
			unit.discoverReady = true;
		}
		foreach (ToySoldierBTree enemy in enemyPieces)
		{
			enemy.enabled = false;
		}

		if (playerPieces.Count <= 0)
		{
			gameState = GameState.LOST;
		}
	}

	public void EndPlayerTurn()
	{
		gameState = GameState.ENEMY;
		stateText.text = gameState.ToString();

		foreach (ToySoldierBTree enemy in enemyPieces)
		{
			enemy.enabled = true;
		}
	}

	public void SwitchToMove()
	{
		playerState = PlayerState.Move;
		playerStateText.text = playerState.ToString();
	}

	public void SwitchToAction()
	{
		playerState = PlayerState.Action;
		playerStateText.text = playerState.ToString();
	}
}
