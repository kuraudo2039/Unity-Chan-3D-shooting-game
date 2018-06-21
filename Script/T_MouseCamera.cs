using UnityEngine;
using System.Collections;

public class T_MouseCamera : MonoBehaviour {

    private GameObject CameraParent;

    public float Speed = 1.0f;
    public float Set = 0.5f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void FixedUpdate()
    {
        CameraParent = Camera.main.transform.parent.gameObject;

        if (Input.GetButton("TrueHandGunSet")) { 
            //マウスでカメラ回転
            transform.Rotate(0.0f, Input.GetAxis("Mouse X") * Speed*Set, 0.0f);
            //一定角度でカメラの回転を停止
            if (CameraParent.transform.localRotation.x < 0.6999f && CameraParent.transform.localRotation.x > -0.6999f)
            {
                CameraParent.transform.Rotate(Input.GetAxis("Mouse Y") * Speed*Set, 0.0f, 0.0f);
            }
            else if (CameraParent.transform.localRotation.x >= 0.6999f)
            {
                //一度停止させて反対方向を入力したら復帰させる
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    CameraParent.transform.Rotate(-1, 0, 0);
                }
            }
            else if (CameraParent.transform.localRotation.x <= -0.6999f)
            {
                //一度停止させて反対方向を入力したら復帰させる
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    CameraParent.transform.Rotate(1, 0, 0);
                }
            }
        }
        else {
            //マウスでカメラ回転
            transform.Rotate(0.0f, Input.GetAxis("Mouse X") * Speed, 0.0f);
            //一定角度でカメラの回転を停止
            if (CameraParent.transform.localRotation.x < 0.6999f && CameraParent.transform.localRotation.x > -0.6999f)
            {
                CameraParent.transform.Rotate(Input.GetAxis("Mouse Y") * Speed, 0.0f, 0.0f);
            }
            else if (CameraParent.transform.localRotation.x >= 0.6999f)
            {
                //一度停止させて反対方向を入力したら復帰させる
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    CameraParent.transform.Rotate(-1, 0, 0);
                }
            }
            else if (CameraParent.transform.localRotation.x <= -0.6999f)
            {
                //一度停止させて反対方向を入力したら復帰させる
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    CameraParent.transform.Rotate(1, 0, 0);
                }
            }
        }
    }
}
