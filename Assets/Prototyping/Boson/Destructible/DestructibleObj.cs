using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class DestructibleObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyObject()
    {
        ExploderSingleton.Instance.ExplodeObject(gameObject);
    }
}
