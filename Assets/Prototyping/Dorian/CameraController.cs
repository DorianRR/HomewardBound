using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private CharacterMovement OwnedPlayer = null;

    [SerializeField]
    private GameObject[] otherPlayers = new GameObject[4];

    [SerializeField]
    [Range(0.01f, 1)]
    private float CameraPositionLag = 1f;

    [SerializeField]
    [Range(0.01f, 1)]
    private float CameraRotationLag = 1f;

    private Vector3 OffSet = Vector3.zero;
    private bool DoOnceRagdoll = true;
    private bool DoOnceDefault = false;


    void Update()
    {

        switch (OwnedPlayer.moveMode)
        {
            case (CharacterMovement.MovementMode.Default):
                //DoOnceRagdoll = true;
                //if(DoOnceDefault)
                //{
                //    transform.rotation = OwnedPlayer.transform.rotation;

                //    //transform.position = new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y + 0.75f, OwnedPlayer.transform.position.z - 0.5f);

                //    DoOnceDefault = false;

                //}
                //HandleDefaultMovement();
                break;

            case (CharacterMovement.MovementMode.Ragdoll):
                transform.parent = null;
                if (DoOnceRagdoll)
                {
                    //OffSet = (transform.position - OwnedPlayer.GetComponent<Rigidbody>().velocity).normalized * 10f;
                    DoOnceRagdoll = false;
                    DoOnceDefault = true;
                }
                HandleRagdollMovement();
                break;
        }
    }

    private void HandleRagdollMovement()
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y + 0.75f, OwnedPlayer.transform.position.z - 0.5f) + OffSet, CameraPositionLag);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((OwnedPlayer.transform.position - transform.position).normalized, transform.up), CameraRotationLag);
        
    }
    

    private void HandleDefaultMovement()
    {
        //transform.parent = OwnedPlayer.transform;

        transform.position = new Vector3(OwnedPlayer.transform.position.x, OwnedPlayer.transform.position.y + (OwnedPlayer.transform.up.y), OwnedPlayer.transform.position.z - (OwnedPlayer.transform.forward.z));
    }

}
