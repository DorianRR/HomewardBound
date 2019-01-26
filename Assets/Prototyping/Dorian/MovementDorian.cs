using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum MOVEMENT_MODE
{ DEFAULT, RAGDOLL }


public class MovementDorian : MonoBehaviour
{


    public MOVEMENT_MODE Move_Mode = MOVEMENT_MODE.DEFAULT;



    void Update()
    {
        switch (Move_Mode)
        {
            case MOVEMENT_MODE.DEFAULT:
                HandleInput();
                break;
            case MOVEMENT_MODE.RAGDOLL:
                HandleRagdoll();
                break;
        }
        
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * 50, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(-transform.up * 10, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(transform.up * 10, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(5, 20), Random.Range(5, 20), Random.Range(5, 20)), ForceMode.Impulse);
            GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10)), ForceMode.Impulse);
            Move_Mode = MOVEMENT_MODE.RAGDOLL;
        }

    }

    private void HandleRagdoll()
    {
        if(!(GetComponent<Rigidbody>().velocity.magnitude > 10))
        {
            Move_Mode = MOVEMENT_MODE.DEFAULT;
        }


    }

}
