using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakObjectParticleEffect : MonoBehaviour
{
    public ParticleSystem breakParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            //breakParticleSystem.EmitParams.transform = this.transform;
            //breakParticleSystem.Play();
        }

    }
}
