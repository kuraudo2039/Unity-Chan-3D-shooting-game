using UnityEngine;
using System.Collections;

public class ShotEnemy : MonoBehaviour {

    public GameObject explosion;

    //private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        //一定時間で弾を消滅させる
        Destroy(gameObject, 2.0f);
        //FixedUpdateのfps指定
        Application.targetFrameRate = 240;
        //1フレームにかける秒数を指定
        Time.fixedDeltaTime = 0.0044f;

        //audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// FixedUpdateは固定fps
	void FixedUpdate () {
        transform.position += transform.forward * Time.fixedDeltaTime * 100f;
    }

    //衝突判定
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Terrain")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
