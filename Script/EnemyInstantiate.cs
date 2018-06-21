using UnityEngine;
using System.Collections;

public class EnemyInstantiate : MonoBehaviour {

    float timer;
    float instantiateInterval = 3.0f;//生成間隔

    public static int instantiateValue;//最大生成数

    public GameObject enemy;

    void Awake()
    {
        instantiateValue = 20;
    }

	// Use this for initialization
	void Start () {
        //instantiateValue = 50;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        //一定時間ごとに敵を生成
        if (timer < 0)
        {

            if (instantiateValue > 0)
            {
                //敵をランダムない位置に生成
                Instantiate(enemy, new Vector3(Random.Range(-50.0f, 450.0f), Random.Range(1.0f, 20.0f), Random.Range(-300.0f, 200.0f)), Quaternion.identity);
                instantiateValue--;
            }

            //生成時間を減らしていく
            instantiateInterval -= 0.1f;
            instantiateInterval = Mathf.Clamp(instantiateInterval, 1.0f, float.MaxValue);

            timer = instantiateInterval;
        }
	}
}
