using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnagArea : MonoBehaviour
{

    private Vector3 snagPoint;
    [SerializeField]
    private float fSnaggedForce;

    private List<Rigidbody> snaggedPlayers = new List<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        snagPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PerformSnagging();
    }

    void PerformSnagging()
    {
        if (snaggedPlayers.Count > 0)
        {
            for (int i = 0; i < snaggedPlayers.Count; i++)
            {
                Rigidbody _rb = snaggedPlayers[i];
                Vector3 dir = (snagPoint - _rb.transform.position).normalized;
                Debug.DrawRay(_rb.transform.position, dir, Color.red);
                _rb.AddForce(dir * fSnaggedForce,ForceMode.Force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name + " Enter");
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb)
                snaggedPlayers.Add(rb);
            else
                Debug.Log("Can not find component");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (snaggedPlayers.Contains(other.GetComponent<Rigidbody>()))
            {
                Debug.Log(other.name + " Leave");

                snaggedPlayers.Remove(other.GetComponent<Rigidbody>());
            }

        }
    }
}
