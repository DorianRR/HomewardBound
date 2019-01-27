using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ScoreValue;
    [SerializeField] private RectTransform MultiplierSection;
    [SerializeField] private TextMeshProUGUI MultiplierValue;
    [SerializeField] private RectTransform MultiplierScoreSection;
    [SerializeField] private TextMeshProUGUI MultiplierScoreValue;


    // Start is called before the first frame update
    void Start()
    {
        MultiplierSection.gameObject.SetActive(false);
        MultiplierScoreSection.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SetScore(int i_Score)
    {
        ScoreValue.text = i_Score.ToString();
    }

    public void SetMultiplier(int i_Multiplier)
    {
        if (i_Multiplier > 0)
            MultiplierSection.gameObject.SetActive(true);
        else
            MultiplierSection.gameObject.SetActive(false);
        MultiplierValue.text = i_Multiplier.ToString();
    }

    public void SetMultiplierScore(int i_MultiplierScore)
    {
        if (i_MultiplierScore > 0)
            MultiplierScoreSection.gameObject.SetActive(true);
        else
            MultiplierScoreSection.gameObject.SetActive(false);
        MultiplierScoreValue.text = i_MultiplierScore.ToString();
    }
}
