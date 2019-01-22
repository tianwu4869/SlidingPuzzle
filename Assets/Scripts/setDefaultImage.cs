using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDefaultImage : MonoBehaviour {

    private bool once;

	// Use this for initialization
	void Start () {
        once = false;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject go = GameObject.Find("thumbnail0");
        if (go)
        {
            if (!once)
            {
                go.GetComponent<cropImage>().crop();
                once = true;
            }
        }
	}
}
