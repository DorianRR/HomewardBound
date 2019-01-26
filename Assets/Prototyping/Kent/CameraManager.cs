using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private List<Camera> m_PlayerCameras;


    // Start is called before the first frame update
    void Start()
    {
        if (m_PlayerCameras == null)
        {
            m_PlayerCameras = new List<Camera>();
        }

        if (m_PlayerCameras.Count <= 0)
        {
            m_PlayerCameras.Add(Camera.main);
        }


        switch (m_PlayerCameras.Count)
        {
            // Initialize Split Screen Crap
            case 1:
                break;
            case 2:
                m_PlayerCameras[1].rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                m_PlayerCameras[0].rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
