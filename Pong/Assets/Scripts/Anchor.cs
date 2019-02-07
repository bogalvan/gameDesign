using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour {


	// Use this for initialization
	void Start () {
        InvokeRepeating("Move", .5f, 3.0f);
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void Move()
    {
        transform.position = new Vector3(Random.Range(-17f, -4f), Random.Range(-6.5f, -13f), 0);
    }


}
