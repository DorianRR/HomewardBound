using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingArea : MonoBehaviour
{

    [SerializeField]
    private float fPlusForce = 1200f;
    private float _originForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BounceUp(CharacterMovement _cm)
    {
        if (_cm)
        {
            _cm.ChangeJumpForce(fPlusForce);
        }
    }
    public void BounceDown(CharacterMovement _cm)
    {
        if (_cm)
        {
            _cm.ChangeJumpForce(_originForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CharacterMovement _cm = collision.collider.GetComponent<CharacterMovement>();
            _originForce = _cm.GetJumpForce();
            BounceUp(_cm);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            BounceDown(collision.collider.GetComponent<CharacterMovement>());
        }
    }
}
