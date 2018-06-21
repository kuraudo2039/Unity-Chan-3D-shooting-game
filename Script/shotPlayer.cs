using UnityEngine;
using System.Collections;

public class shotPlayer : MonoBehaviour {

    public GameObject explosion;
    public static int damage=200;
    public static int damageDefault = 200;

    //private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        damageDefault=200;
        damage = damageDefault;
        //一定時間で弾を消滅させる
        Destroy(gameObject, 5.0f);
        //FixedUpdateのfps指定
        Application.targetFrameRate = 240;
        //1フレームにかける秒数を指定
        Time.fixedDeltaTime = 0.0044f;

        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    // FixedUpdateは固定fps
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * 100f;
                //デフォルトダメージにブレ追加
    }
    void Update()
    {
        //デフォルトダメージにブレ追加
        damage = Random.Range(damageDefault - 10, damageDefault + 10);
        //距離でdamageを減算
        /*damage--;
        if (damage < 1) {
            damage = 1;
        }*/
    }
    //衝突判定
    private void OnCollisionEnter(Collision collider)
    {

        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);

    }
}
