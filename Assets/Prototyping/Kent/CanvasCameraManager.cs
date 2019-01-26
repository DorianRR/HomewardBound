using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasCameraManager : MonoBehaviour
{
    [SerializeField] private RectTransform MainCanvasPrefab;
    [SerializeField] private Canvas PlayerCanvasPrefab;

    [SerializeField]
    private List<GameObject> m_PlayersWithCameras;


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

        if (m_PlayersWithCameras.Count <= 0)
            Debug.LogError("Player Objects Need to be added in CanvasCameraManager");

        m_PlayerCameras = new List<Camera>();
        m_PlayerCanvases = new List<Canvas>();

        // Finds all cameras on all of the players

        foreach (GameObject l_PlayerGameObject in m_PlayersWithCameras)
        {
            Camera l_PlayerCamera = l_PlayerGameObject.GetComponentsInChildren<Camera>().Single();
            m_PlayerCameras.Add(l_PlayerCamera);
        }

        if (m_PlayerCameras.Count <= 0)
        {
            Debug.LogError("No Cameras Found On Players");
        }

        // Set Camera ViewPorts For Multiplayer
        switch (m_PlayerCameras.Count)
        {
            // Initialize Split Screen Crap
            case 1:
                break;
            case 2:
                m_PlayerCameras[1].rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f); // Player 2
                m_PlayerCameras[0].rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f); // Player 1
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


        // Set Up Player Canvases via duplication
        foreach (Camera l_Camera in m_PlayerCameras)
        {
            Canvas l_PlayerCanvas = Instantiate(PlayerCanvasPrefab, this.transform);
            l_PlayerCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            l_PlayerCanvas.worldCamera = l_Camera;
            m_PlayerCanvases.Add(l_PlayerCanvas);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
