using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickOnSprites : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        GameObject control = GameObject.Find("Control");
        if(control.GetComponent<swapSprites>().names.Count == 0 && !control.GetComponent<swapSprites>().horizontal && !control.GetComponent<swapSprites>().vertical)
        {
            control.GetComponent<swapSprites>().names.Add(this.name);
        }
    }
}
