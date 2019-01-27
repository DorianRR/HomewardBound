using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyArea : MonoBehaviour
{

    [SerializeField]
    private float fSlippryForce;
    private float _originForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerProperties"))
        {
            CharacterMovement _cm = collision.collider.GetComponent<CharacterMovement>();
            collision.collider.GetComponent<Rigidbody>().drag = 0;
            collision.collider.GetComponent<Rigidbody>().angularDrag = 0;

            _originForce = _cm.fMovementForce;
            fSlippryForce = _originForce/5;
            _cm.fMovementForce = fSlippryForce;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerProperties"))
        {
            CharacterMovement _cm = collision.collider.GetComponent<CharacterMovement>();
            _cm.fMovementForce = _originForce;
            collision.collider.GetComponent<Rigidbody>().drag = 0.5f;
            collision.collider.GetComponent<Rigidbody>().angularDrag = 0.5f;
        }
    }
}
