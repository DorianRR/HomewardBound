using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;
using UnityEngine.Audio;

public class DestructibleObj : MonoBehaviour
{



    private GameObject AudioController = null;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioController = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {

            AudioController.SetActive(true);
            AudioController.transform.parent = null;
            DestroyObject();

        }
    }



    public void DestroyObject()
    {
        ExploderSingleton.Instance.ExplodeObject(gameObject);
    }

    

  
}
