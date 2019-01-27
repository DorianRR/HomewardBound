using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ScoreValue;
    [SerializeField] private TextMeshProUGUI MultiplierValue;
    [SerializeField] private TextMeshProUGUI MultiplierScoreValue;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SetScore(int i_Score)
    {
        ScoreValue.text = i_Score.ToString();
    }

    public void SetMultiplier(int i_Score)
    {
        MultiplierValue.text = i_Score.ToString();
    }

    public void SetMultiplierScore(int i_Score)
    {
        MultiplierScoreValue.text = i_Score.ToString();
    }
}
