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

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public OptionsManager OptionsManager { get; private set; }
	public AudioManager AudioManager { get; private set; }

	//prefabs for spawning on start
	//public GameObject enemyPrefab;
	//public GameObject[] PiecePrefabs;

	public GameState gameState;
	public TMP_Text stateText;
	public bool playingCard = false;
	public bool playingMove = false;
	public Unit[] playerPieces;

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

		playerPieces = FindObjectsOfType<Unit>();
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
		stateText.text = gameState.ToString();
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

		foreach(Unit unit in playerPieces)
		{
			unit.moveReady = true;
			unit.discoverReady = true;
		}
	}

	public void EndPlayerTurn()
	{
		gameState = GameState.ENEMY;
		stateText.text = gameState.ToString();
	}
}
