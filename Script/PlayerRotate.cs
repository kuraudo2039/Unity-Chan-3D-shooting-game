using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour {
    public float Speed=3.0f;
    GameObject OldCameraParent;
    private GameObject CameraParent;
    Quaternion defaultCameraRot;
    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        //カメラ初期方向の記憶
        OldCameraParent = Camera.main.transform.parent.gameObject;
        defaultCameraRot = OldCameraParent.transform.localRotation;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {

        CameraParent = Camera.main.transform.parent.gameObject;

        transform.Rotate(0.0f, Input.GetAxis("Horizontal2") * Speed, 0.0f);
        
        //テンキーが利くようにする
        if (Input.GetButton("Horizontal2") || Input.GetButton("Vertical2"))
        {
            GetComponent<T_MouseCamera>().enabled = false;
        }
        else
        {
            GetComponent<T_MouseCamera>().enabled = true;
        }

        //一定角度でカメラ移動を停止させる
        if (CameraParent.transform.localRotation.x < 0.6999f && CameraParent.transform.localRotation.x > -0.6999f)
        {
            CameraParent.transform.Rotate(Input.GetAxis("Vertical2") * Speed, 0.0f, 0.0f);
        }
        else if (CameraParent.transform.localRotation.x >= 0.6999f)
        {
            //一度停止させて反対方向を入力したら復帰させる
            if (Input.GetAxis("Vertical2") < 0)
            {
                CameraParent.transform.Rotate(-1, 0, 0);
            }
        }
        else if (CameraParent.transform.localRotation.x <= -0.6999f)
        {
            //一度停止させて反対方向を入力したら復帰させる
            if (Input.GetAxis("Vertical2") > 0)
            {
                CameraParent.transform.Rotate(1, 0, 0);
            }
        }

        //カメラの回転をリセット
        if (Input.GetButton("CamReset"))
        {
            timer = 0.2f;
            //スムーズにカメラの回転を戻す
        }
        if (timer > 0)
        {
            OldCameraParent.transform.localRotation = Quaternion.Slerp(OldCameraParent.transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

            timer -= Time.deltaTime;
        }
    }
}
