using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBtnImage : MonoBehaviour {

    public Sprite[] images = new Sprite[2];

    private int now;

    private void OnMouseEnter()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        var sprite = GetComponent<Image>().sprite;
        if (sprite == images[0])
        {
            GetComponent<Image>().sprite = images[1];
            now = 0;
        } else
        {
            GetComponent<Image>().sprite = images[0];
            now = 1;
        }
    }

    private void OnMouseExit()
    {
        if (!GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<Image>().sprite = images[now];
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}
