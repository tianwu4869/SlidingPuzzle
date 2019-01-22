using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapSprites : MonoBehaviour {

    public List<string> names = new List<string>();
    public bool horizontal = false;
    public bool vertical = false;
    public string blank;
    GameObject pic1;
    GameObject pic2;
    Vector3 pos1;
    Vector3 pos2;
    Vector3 vel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (names.Count == 1)
        {
            pic1 = GameObject.Find(names[0]);
            pic2 = GameObject.Find(blank);
            pos1 = pic1.GetComponent<RectTransform>().localPosition;
            pos2 = pic2.GetComponent<RectTransform>().localPosition;
            if (pos1.x == pos2.x && Mathf.Abs(pos1.y - pos2.y) == 307)
            {
                vel = (pos2 - pos1) / 0.25f;
                vertical = true;
            }
            else if (pos1.y == pos2.y && Mathf.Abs(pos1.x - pos2.x) == 307)
            {
                vel = (pos2 - pos1) / 0.25f;
                horizontal = true;
            }
            names.RemoveRange(0, 1);
        }
        if (vertical)
        {
            pic1.GetComponent<RectTransform>().localPosition += Time.deltaTime * vel;
            pic2.GetComponent<RectTransform>().localPosition -= Time.deltaTime * vel;
            if(Vector3.Dot(-vel, pic1.GetComponent<RectTransform>().localPosition - pos2) < 0)
            {
                pic1.GetComponent<RectTransform>().localPosition = pos2;
                pic2.GetComponent<RectTransform>().localPosition = pos1;
                vertical = false;
            }
        }
        if (horizontal)
        {
            pic1.GetComponent<RectTransform>().localPosition += Time.deltaTime * vel;
            pic2.GetComponent<RectTransform>().localPosition -= Time.deltaTime * vel;
            if (Vector3.Dot(-vel, pic1.GetComponent<RectTransform>().localPosition - pos2) < 0)
            {
                pic1.GetComponent<RectTransform>().localPosition = pos2;
                pic2.GetComponent<RectTransform>().localPosition = pos1;
                horizontal = false;
            }
        }
    }
}
