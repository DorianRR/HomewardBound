using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
