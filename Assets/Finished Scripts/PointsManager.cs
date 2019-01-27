using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject OwningPlayer = null;

    [SerializeField]
    [Range(1, 10)]
    private int NumberOfSamplesPerSecond = 2;

    public float AggregateScore;

    public float PendingScore = 0;

    private float timer;
    private int positionInSample = 0;

    public float spinTimer = 0;
    public float slideTimer = 0;
    public bool isSliding = false;
    public bool isSpinning = false;

    private Vector3[] DeltaRotationSamepls;

    private float[] DeltaXRot;

    private Vector3 RotationAtLastSample;
    private int m_Index; // Most Ghetto Ass way to determine index. But quick


    private void Start()
    {
        timer = 0;
        DeltaRotationSamepls = new Vector3[NumberOfSamplesPerSecond];
        DeltaXRot = new float[NumberOfSamplesPerSecond];
    }

    void Update()
    {
        bool tempbool1 = false;
        timer += Time.deltaTime;

        if (timer > 1.0f / NumberOfSamplesPerSecond)
        {
            DeltaXRot[positionInSample] = Mathf.Acos(Mathf.Clamp(Vector3.Dot(transform.forward.normalized, RotationAtLastSample.normalized), -1f, 1f));


            //DeltaRotationSamepls[positionInSample] = (transform.forward.normalized - RotationAtLastSample.normalized) ;
            if (positionInSample++ > DeltaRotationSamepls.Length - 2)
            {
                positionInSample = 0;
            }
            timer = 0;
            RotationAtLastSample = transform.forward;
            tempbool1 = CheckForFlip();
        }

        bool tempbool2 = CheckForSlide();

        if (!tempbool2 && !tempbool1)
        {
            AggregateScore += PendingScore;
            PendingScore = 0;
        }

        // Update UI Scores
        CanvasCameraManager.Instance.SetPlayerScore(m_Index, (int)AggregateScore);
        //CanvasCameraManager.Instance.SetPlayerMultiplier(m_Index, (int)Mulp);
        CanvasCameraManager.Instance.SetPlayerMultiplierScore(m_Index, (int)PendingScore);
    }

    bool CheckForSlide()
    {
        if (OwningPlayer.GetComponent<Rigidbody>().velocity.magnitude < 10)
        {
            slideTimer = 0;
            isSliding = false;
            return false;

        }
        float temp = Mathf.Acos(Vector3.Dot(transform.forward.normalized, OwningPlayer.GetComponent<Rigidbody>().velocity.normalized));
        if (temp > 0.5f)
        {
            PendingScore += 1;
            slideTimer += Time.deltaTime;
            PendingScore *= 1 + (slideTimer / 10);
            isSliding = true;
            return true;
        }
        return false;
    }

    bool CheckForFlip()
    {
        Vector3 temp = Vector3.zero;
        float XFloat = 0;
        foreach (float vec in DeltaXRot)
        {
            XFloat += vec;
        }
        if (Mathf.Abs(XFloat) > Mathf.PI)
        {
            PendingScore += 1;
            spinTimer += Time.deltaTime;
            PendingScore *= Random.Range(1.1f, 1.25f);
            isSpinning = true;
            return true;
        }
        else
        {
            isSpinning = false;
            spinTimer = 0;
            return false;
        }
    }

    /// <summary>
    /// Ghetto ass way to store index
    /// </summary>
    /// <param name="i_index">Index of the i.</param>
    public void SetIndex(int i_index)
    {
        m_Index = i_index;
    }
}

