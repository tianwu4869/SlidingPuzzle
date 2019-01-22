using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadThumbnails : MonoBehaviour {

    public GameObject menu;
    public GameObject prefab;
    private object[] pictures;

	// Use this for initialization
	void Start () {
        pictures = Resources.LoadAll("Pictures", typeof(Texture2D));
        for(int i = 0; i < pictures.Length; i++)
        {
            Texture2D picture = (Texture2D)pictures[i];
            GameObject thumbnail = GameObject.Instantiate(prefab, menu.transform);
            thumbnail.GetComponent<RectTransform>().localPosition += new Vector3(326.0f * i, 0, 0);
            thumbnail.GetComponent<Image>().sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f), 100.0f);
            thumbnail.name = "thumbnail" + i.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
