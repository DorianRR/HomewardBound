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

    private Vector3 MyVelocity = Vector3.zero;

    void Update()
    {

        switch (OwnedPlayer.Move_Mode)
        {
            case (MOVEMENT_MODE.DEFAULT):
                HandleDefaultMovement();
                break;

            case (MOVEMENT_MODE.RAGDOLL):
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
        transform.parent = null;
        transform.position = new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y, OwnedPlayer.transform.position.z);



    }

    private void ReturnToDefault()
    {

    }
}
