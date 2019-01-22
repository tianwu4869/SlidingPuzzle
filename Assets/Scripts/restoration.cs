using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restoration : MonoBehaviour {

    public bool playing;
    public float upleft_x;
    public float upleft_y;

	// Use this for initialization
	void Start () {
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (playing)
        {
            bool success = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int digit = i * 3 + j + 1;
                    Vector3 pieceLocation;
                    pieceLocation = GameObject.Find("Cropped" + " (" + digit + ")").GetComponent<RectTransform>().localPosition;
                    if(pieceLocation.x != upleft_x + 307 * i || pieceLocation.y != upleft_y - 307 * j)
                    {
                        success = false;
                        break;
                    }
                }
            }
            if (success)
            {
                GameObject.Find("Canvas").transform.GetChild(12).gameObject.SetActive(true);
                playing = false;
            }
        }
	}
}
