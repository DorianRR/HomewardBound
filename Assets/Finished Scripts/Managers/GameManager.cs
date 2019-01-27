﻿using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    START,
    PLAY,
    END
}


public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct PlayerProperties
    {
        [SerializeField]
        public GameObject StartPosition;
    }


    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private List<PlayerProperties> PlayerPresetProperties;


    public static GameManager Instance;

    private GameStates m_CurrentGameState;
    private List<GameObject> m_CurrentPlayers;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        if (m_CurrentPlayers == null)
            m_CurrentPlayers = new List<GameObject>();

        if (PlayerPresetProperties == null)
            PlayerPresetProperties = new List<PlayerProperties>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameStates(GameStates.START);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_CurrentGameState)
        {
            case GameStates.START:
                if (Input.GetButtonDown("Submit"))
                {
                    UpdateGameStates(GameStates.PLAY);
                }
                break;
            case GameStates.PLAY:
                if (Input.GetButtonDown("Submit"))
                {
                    UpdateGameStates(GameStates.END);
                }
                break;
            case GameStates.END:
                if (Input.GetButtonDown("Submit"))
                {
                    UpdateGameStates(GameStates.START);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void UpdateGameStates(GameStates i_newState)
    {

        switch (i_newState)
        {
            case GameStates.START:
                m_CurrentGameState = GameStates.START;
                CanvasCameraManager.Instance.UnloadSplitCameras();
                UnloadPlayerObjects();
                CanvasCameraManager.Instance.UpdateMainUI(m_CurrentGameState);
                break;
            case GameStates.PLAY:
                m_CurrentGameState = GameStates.PLAY;
                LoadPlayerObjects();
                CanvasCameraManager.Instance.LoadSplitCameras(m_CurrentPlayers);
                CanvasCameraManager.Instance.UpdateMainUI(m_CurrentGameState);
                break;
            case GameStates.END:
                m_CurrentGameState = GameStates.END;
                CanvasCameraManager.Instance.UnloadSplitCameras();
                UnloadPlayerObjects();
                CanvasCameraManager.Instance.UpdateMainUI(m_CurrentGameState);
                break;
            default:
                throw new ArgumentOutOfRangeException("i_newState", i_newState, null);
        }
    }

    public void LoadPlayerObjects()
    {
        if (PlayerPresetProperties.Count <= 0)
        {
            Debug.LogError("No Player Count Set");
            return;
        }

        if (m_CurrentPlayers.Count > 0)
        {
            Debug.LogError("Players already loaded. Need to Unload");
            return;
        }

        // Create Players
        foreach (PlayerProperties l_PProperty in PlayerPresetProperties)
        {
            GameObject l_newPlayer = Instantiate(PlayerPrefab, l_PProperty.StartPosition.transform.position, Quaternion.identity,
                this.transform);
            m_CurrentPlayers.Add(l_newPlayer);
        }

    }

    public void UnloadPlayerObjects()
    {
        if (m_CurrentPlayers.Count <= 0)
        {
            Debug.Log("No Players Found To Unload");
            return;
        }

        // Destroy Players
        foreach (GameObject l_Player in m_CurrentPlayers)
        {
            Destroy(l_Player);
        }
        m_CurrentPlayers.Clear();
    }
}
