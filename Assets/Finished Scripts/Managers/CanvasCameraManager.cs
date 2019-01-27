using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasCameraManager : MonoBehaviour
{
    [SerializeField] private RectTransform MainCanvasPrefab;
    [SerializeField] private Canvas PlayerCanvasPrefab;



    [SerializeField] private RectTransform StartMainScreen;
    [SerializeField] private RectTransform PlayMainScreen;
    [SerializeField] private RectTransform EndMainScreen;
    [SerializeField] private Camera MainCamera;


    public static CanvasCameraManager Instance;

    private List<Camera> m_PlayerCameras;
    private List<Canvas> m_PlayerCanvases;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        m_PlayerCameras = new List<Camera>();
        m_PlayerCanvases = new List<Canvas>();

        if (MainCamera == null)
            MainCamera = Camera.main;

        MainCamera.enabled = true;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadSplitCameras(List<GameObject> i_PlayerObjects)
    {
        // Finds all cameras on all of the players
        foreach (GameObject l_PlayerGameObject in i_PlayerObjects)
        {
            Camera l_PlayerCamera = l_PlayerGameObject.GetComponentsInChildren<Camera>().Single();
            m_PlayerCameras.Add(l_PlayerCamera);
        }

        if (m_PlayerCameras.Count <= 0)
        {
            Debug.LogError("No Cameras Found On Players");
        }


        // Disable Main Camera
        MainCamera.enabled = false;


        // Set Camera ViewPorts For Multiplayer
        switch (m_PlayerCameras.Count)
        {
            // Initialize Split Screen Crap
            case 1:
                break;
            case 2:
                m_PlayerCameras[1].rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f); // PlayerProperties 2
                m_PlayerCameras[0].rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f); // PlayerProperties 1
                break;
            case 3:
                m_PlayerCameras[0].rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                m_PlayerCameras[1].rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                m_PlayerCameras[2].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                break;
            case 4:
                m_PlayerCameras[0].rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                m_PlayerCameras[1].rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                m_PlayerCameras[2].rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                m_PlayerCameras[3].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                break;
            default:
                Debug.LogError("Does not support camera count:" + m_PlayerCameras.Count);
                break;
        }


        // Set Up PlayerProperties Canvases via duplication
        foreach (Camera l_Camera in m_PlayerCameras)
        {
            Canvas l_PlayerCanvas = Instantiate(PlayerCanvasPrefab, this.transform);
            l_PlayerCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            l_PlayerCanvas.worldCamera = l_Camera;
            m_PlayerCanvases.Add(l_PlayerCanvas);
        }
    }


    public void UnloadSplitCameras()
    {

        // Clear Canvases
        foreach (Canvas l_Canvas in m_PlayerCanvases)
        {
            if (l_Canvas != null)
                Destroy(l_Canvas.gameObject);
        }
        m_PlayerCanvases.Clear();

        // Clear Referenced Cameras
        foreach (Camera l_Camera in m_PlayerCameras)
        {
            if (l_Camera != null)
                l_Camera.enabled = false;
        }
        m_PlayerCameras.Clear();

        // Enable Main Camera
        MainCamera.enabled = true;
    }




    public void UpdateMainUI(GameStates i_newState)
    {
        switch (i_newState)
        {
            case GameStates.START:
                StartMainScreen.gameObject.SetActive(true);
                PlayMainScreen.gameObject.SetActive(false);
                EndMainScreen.gameObject.SetActive(false);
                break;
            case GameStates.PLAY:
                StartMainScreen.gameObject.SetActive(false);
                PlayMainScreen.gameObject.SetActive(true);
                EndMainScreen.gameObject.SetActive(false);
                break;
            case GameStates.END:
                StartMainScreen.gameObject.SetActive(false);
                PlayMainScreen.gameObject.SetActive(false);
                EndMainScreen.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException("i_newState", i_newState, null);
        }
    }


}
