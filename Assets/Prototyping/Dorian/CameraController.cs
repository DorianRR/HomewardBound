using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private MovementDorian OwnedPlayer = null;

    [SerializeField]
    private GameObject[] otherPlayers = new GameObject[4];

    [SerializeField]
    [Range(0.01f, 1)]
    private float CameraPositionLag;

    [SerializeField]
    [Range(0.01f, 1)]
    private float CameraRotationLag;

    private Vector3 OffSet = Vector3.zero;
    private bool DoOnce = true;

    void Update()
    {

        switch (OwnedPlayer.Move_Mode)
        {
            case (MOVEMENT_MODE.DEFAULT):
                DoOnce = true;
                HandleDefaultMovement();
                break;

            case (MOVEMENT_MODE.RAGDOLL):
                transform.parent = null;
                if (DoOnce)
                {
                    OffSet = transform.position - OwnedPlayer.GetComponent<Rigidbody>().velocity;
                    DoOnce = false;
                }

                HandleRagdollMovement();
                break;
        }
    }

    private void HandleDefaultMovement()
    {

        //transform.position = new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y + 0.75f, OwnedPlayer.transform.position.z - 0.5f);
        //transform.rotation = OwnedPlayer.transform.rotation;

        return;
    }

    private void HandleRagdollMovement()
    {
        transform.position = new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y, OwnedPlayer.transform.position.z);



    }

    private void ReturnToDefault()
    {

    }
}
