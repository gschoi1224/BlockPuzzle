using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public new Camera camera;
    public GameObject block;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit)
            {
                if (hit.collider.gameObject.layer == 12)
                    Instantiate(block, hit.collider.transform);
                else if (hit.collider.gameObject.layer == 10)
                    Destroy(hit.collider.gameObject);
            }
        }
	}
}
