using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWorldSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject worldObj = null;

    [SerializeField]
    private int numObjs = 50;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <numObjs; i++)
        {
            GameObject temp = Instantiate(worldObj, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
            temp.transform.localScale = new Vector3(temp.transform.localScale.x, Random.Range(0.5f, 15), temp.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
