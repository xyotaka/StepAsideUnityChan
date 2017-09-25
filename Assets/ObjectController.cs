using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    //unitychanのGameObject
    private GameObject unityChan;
    //unitychanのz position
    float zPos;

	// Use this for initialization
	void Start () {
        this.unityChan = GameObject.Find("unitychan");
	}
	
	// Update is called once per frame
	void Update () {
        zPos = unityChan.transform.position.z;

        if(this.transform.position.z < zPos)
        {
            Destroy(this.gameObject);
        }
	}
}
