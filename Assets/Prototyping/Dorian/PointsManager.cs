using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

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
            if(positionInSample++ > DeltaRotationSamepls.Length - 2)
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

    }

    void CheckForFlip()
    {
        Vector3 temp = Vector3.zero;
        float tempfloat = 0;
        foreach(float vec in DeltaXRot)
        {
            tempfloat += vec;
        }

        //if (Mathf.Abs(temp.x)> 180)
        //{
        //    PendingScore *= Random.Range(95, 105);
        //}
        //if (Mathf.Abs(temp.y) > 180)
        //{
        //    PendingScore *= Random.Range(95, 105);
        //}
        //if (Mathf.Abs(temp.z) > 180)
        //{
        //    PendingScore *= Random.Range(95, 105);
        //}

        if (Mathf.Abs(tempfloat) > Mathf.PI/2)
        {
            PendingScore *= Random.Range(9, 11);

        }


        if (temp.magnitude < 100)
        {
            PendingScore = 1;
        }


        print(PendingScore);
        //print(positionInSample);
    }

  
}

