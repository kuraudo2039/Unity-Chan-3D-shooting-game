using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Compass : MonoBehaviour {

    public Image compassImage;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        compassImage.transform.rotation = Quaternion.Euler(compassImage.transform.rotation.z, compassImage.transform.rotation.y, transform.eulerAngles.y);
	}
}
