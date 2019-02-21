using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
	public Transform target; //the target object
	public float speedMod=30.0f; // speed modifier
	private Vector3 point; //coord point where camera looks
	private int angle=0;

	// Use this for initialization
	void Start () {		
		point = target.transform.position;
        //transform.LookAt(target);
        //target.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.RotateAround(point,Vector3.one,0);
	}



	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.forward * Time.deltaTime * speedMod);
        //transform.RotateAround(point,Vector3.down+Vector3.left, speedMod * Time.deltaTime);
        
        transform.RotateAround(point,Vector3.left, speedMod * Time.deltaTime);
		angle++;
		if (angle>451){//(speedMod * Time.deltaTime > 360) {
			UnityEditor.EditorApplication.ExecuteMenuItem ("Edit/Play");
		}
		//var go = GameObject.Find("Cerebellum");
		//go.renderer.material.color.a = .5;
	}


}
