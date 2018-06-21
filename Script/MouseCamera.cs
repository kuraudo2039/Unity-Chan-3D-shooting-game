using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {

    private Vector3 MousePosition = Vector3.zero;
    private Vector3 ScreenToWorld = Vector3.zero;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        MousePosition = Input.mousePosition;
        MousePosition.z = 10.0f;
        ScreenToWorld = Camera.main.ScreenToWorldPoint(MousePosition);
        gameObject.transform.position = ScreenToWorld;
	}
}
