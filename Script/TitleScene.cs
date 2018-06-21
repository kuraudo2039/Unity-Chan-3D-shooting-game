using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

    public Text blinkText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //ボタンを押したら遷移
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Sample");
        }

        //ボタンを押させるためのmwっセージを点滅させる
        blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
	}
}
