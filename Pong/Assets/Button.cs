using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}


    void OnMouseDown()
    {
        print("pressed");
        GameManager.buttonClicked();

    }


}
