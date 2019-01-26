using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float fJumpForce;

    // Parameter
    [SerializeField]
    private bool bIsGrounded;

    // Component
    private Rigidbody rb;
    private Transform camParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camParent = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        bIsGrounded = IsGrounded();
    }

    private void PerformJumping()
    {
        if (Input.GetButtonDown("Jump"))
            rb.AddForce(new Vector3(0, fJumpForce, 0));
    }

    private void PerformMovement()
    {

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");


    }
    private void PerformRotation()
    {
        
    }
    private bool IsGrounded()
    {
        bool _groudned = false;

        return _groudned;
    }


}
