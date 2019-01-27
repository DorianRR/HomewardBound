using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject OwningPlayer = null;

    [SerializeField]
    [Range(1,10)]
    private int NumberOfSamplesPerSecond = 2;

    public float AggregateScore;

    public float PendingScore = 1;

    private float timer;
    private int positionInSample = 0;

    private Vector3[] DeltaRotationSamepls;

    private float[] DeltaXRot;
 
    private Vector3 RotationAtLastSample;



    private void Start()
    {
        timer = 0;
        DeltaRotationSamepls = new Vector3[NumberOfSamplesPerSecond];
        DeltaXRot = new float[NumberOfSamplesPerSecond];
        

    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1.0f/NumberOfSamplesPerSecond)
        {
            DeltaXRot[positionInSample] = Mathf.Acos(Mathf.Clamp(Vector3.Dot(transform.forward.normalized, RotationAtLastSample.normalized), -1f, 1f));
            


            //DeltaRotationSamepls[positionInSample] = (transform.forward.normalized - RotationAtLastSample.normalized) ;
            if (positionInSample++ > DeltaRotationSamepls.Length - 2)
            {
                positionInSample = 0;
            }
            timer = 0;
            RotationAtLastSample = transform.forward;
            CheckForFlip();
        }

        CheckForSlide();
    }

    void CheckForSlide()
    {
        if(OwningPlayer)
    }

    void CheckForFlip()
    {
        Vector3 temp = Vector3.zero;
        float XFloat = 0;
        float YFloat = 0;
        float ZFloat = 0;

        foreach (float vec in DeltaXRot)
        {
            XFloat += vec;
        }
     


        if (Mathf.Abs(XFloat) > Mathf.PI)
        {
            PendingScore *= Random.Range(1.1f, 1.25f);

        }
     





        print(PendingScore);
        //print(positionInSample);
    }

  
}

