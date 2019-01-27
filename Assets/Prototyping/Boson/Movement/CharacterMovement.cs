using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public enum MovementMode
    {
        Default,
        Sliding,
        Snagged,
        Ragdoll
    }

    [Header("Force")]
    [SerializeField]
    private float fJumpForce;
    //[SerializeField]
    public float fMovementForce;
    [Header("Restrict")]
    [SerializeField]
    private float fRestrictAngle;
    [SerializeField]
    private float fMaxSpeed;
    [SerializeField]
    private float fJoyStickSensitivity;
    public MovementMode moveMode = MovementMode.Default;
    [SerializeField]
    private float fRaycastDistance = 0.01f;
    [SerializeField]
    private float fSlidingForce;

    // Parameter
    [SerializeField]
    private bool bIsGrounded;
    [SerializeField]
    private int iControlID;

    private string horiAxis = "Horizontal";
    private string vertAxis = "Vertical";
    private string rotXaxis = "Mouse X";
    private string rotYaxis = "Mouse Y";

    private string aButton = "Jump";

    // Component
    private Rigidbody rb;
    private CapsuleCollider capCollider;
    private Collider[] colliders;
    private Transform camParent;
    
    [SerializeField]
    private GameObject hip = null;

    [SerializeField]
    private GameObject temp = null;

    private Transform LookAtParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
        camParent = transform.GetChild(0);
        LookAtParent = transform.GetChild(1);
        colliders = hip.GetComponentsInChildren<Collider>();
        IgnoreCollision();
        hip.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.Space) && moveMode == MovementMode.Default)
        {
            moveMode = MovementMode.Ragdoll;
            RagdollUpdate();
        }
       else if (Input.GetKeyDown(KeyCode.Space) && moveMode == MovementMode.Ragdoll)
        {
            moveMode = MovementMode.Default;
            temp.SetActive(true);
            hip.SetActive(false);
            LookAtParent = transform.GetChild(1);

        }

        bIsGrounded = IsGrounded();
        PerformRotation();
        PerformMovement();
        PerformJumping();
        if (transform.position.y < .1f)
        {
           // transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        switch (moveMode)
        {

            case MovementMode.Default:
                break;
            case MovementMode.Ragdoll:
                hip.transform.GetChild(0).GetComponent<Rigidbody>().MovePosition(hip.transform.parent.position + new Vector3(0,1f,0));
              
                break;
        }
    }

    void IgnoreCollision()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.IgnoreCollision(colliders[i], capCollider, true);
            colliders[i].GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void SetControllerID(int _ID)
    {
        iControlID = _ID;

        // Movement
        horiAxis = "J" + iControlID + "Horizontal";
        vertAxis = "J" + iControlID + "Vertical";

        // Rotation
        rotXaxis = "J" + iControlID + "Mouse X";
        rotYaxis = "J" + iControlID + "Mouse Y";

        // Jump
        aButton = "J" + iControlID + "aButton";
    }

    private void PerformJumping()
    {

        if (Input.GetButtonDown(aButton) && bIsGrounded)
        {
            Debug.Log("Press a button");
            rb.AddForce(new Vector3(0, fJumpForce, 0));
        }
    }

    private void PerformMovement()
    {

        float hori = Input.GetAxis(horiAxis);
        float vert = Input.GetAxis(vertAxis);

        Vector3 veloRef = rb.velocity;
        veloRef.y = 0;
        if (veloRef.magnitude < fMaxSpeed)
        {
            rb.AddRelativeForce(new Vector3(hori, 0, vert) * (bIsGrounded ? fMovementForce : fMovementForce / 3), ForceMode.Force);
        }

    }
    private void PerformRotation()
    {
        float hori = Input.GetAxis(rotXaxis);
        float vert = Input.GetAxis(rotYaxis);

        //if (hori < 0.1f || hori > -0.1f)
        //    hori = 0;
        //if (vert < 0.1f || vert > -0.1f)
        //    vert = 0;

        if (iControlID == 0)
            fJoyStickSensitivity = 1;

        transform.Rotate(transform.up, hori * fJoyStickSensitivity);
        //camParent.Rotate(new Vector3(-vert * fJoyStickSensitivity, 0, 0));
        camParent.RotateAround(LookAtParent.position, camParent.right, -vert * fJoyStickSensitivity);
        Debug.Log(camParent.eulerAngles);
        if (camParent.localEulerAngles.x > 40 && camParent.localEulerAngles.x < 180)
        {
            camParent.localEulerAngles = new Vector3(40, 0, 0);
            camParent.localPosition = new Vector3(camParent.localPosition.x, 2.1f, -1.9f);
        }
        if (camParent.localEulerAngles.x < 360 - 10 && camParent.localEulerAngles.x > 180)
        {
            camParent.localEulerAngles = new Vector3(360 - 10, 0, 0);
            camParent.localPosition = new Vector3(camParent.localPosition.x, 0f, -2.8f);


        }
    }
    private bool IsGrounded()
    {
        bool _groudned = false;

        Ray[] ray = new Ray[3];
        for (int i = -1; i < ray.Length - 1; i++)
        {
            ray[i + 1] = new Ray(transform.position + transform.forward * 0.5f * i, Vector3.down);
        }
        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction);

        }
        for (int i = 0; i < ray.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray[i], out hit, fRaycastDistance))
            {
                if (!hit.collider.isTrigger)
                {
                    _groudned = true;
                    break;
                }

            }
        }

        return _groudned;
    }

    public void ChangeJumpForce(float _force)
    {
        fJumpForce = _force;
    }
    public float GetJumpForce()
    {
        return fJumpForce;
    }

    private void RagdollUpdate()
    {
       // transform.GetComponent<CapsuleCollider>().enabled = false;
        temp.SetActive(false);
        hip.SetActive(true);
        LookAtParent = transform.GetChild(1).GetChild(1).GetChild(0);
    }

}
