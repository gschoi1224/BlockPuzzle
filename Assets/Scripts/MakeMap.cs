using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMap : MonoBehaviour {

    public GameObject block;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject childBlock = null;
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = transform.position;
            Vector3 mousePos = Input.mousePosition;
            mousePos += new Vector3(-225, -170, 0);
            if (mousePos.x >= pos.x - 35 && mousePos.x <= pos.x + 35)
            {
                if (mousePos.y >= pos.y - 35 && mousePos.y <= pos.y + 35)
                {
                    if (transform.childCount == 0)
                    {
                        childBlock = Instantiate(block, transform.position, Quaternion.identity) as GameObject;
                        childBlock.transform.SetParent(transform);
                    }
                }
            }
        } 
    }
}
