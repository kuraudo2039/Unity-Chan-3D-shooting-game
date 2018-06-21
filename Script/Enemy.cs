using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    GameObject target;

    public GameObject EnemyMuzzle;
    public GameObject shot;
    public GameObject explosion;
    //GameObject system;

    public int Armorpoint;
    public static int defaultArmorpointMax=1000;
    public int ArmorpointMax = 1000;
    int damage=100;

    float shotInterval=0.0f;
    float shotIntervalMax=1.0f;
    float timer = 0;

    int enemylevel = 0;

    AudioSource hitAudio;
    AudioSource enemyShot;
	// Use this for initialization
	void Start () {
        defaultArmorpointMax = 1000;
        //system = GameObject.Find("System");
        target = GameObject.Find("PlayerTarget");
        ArmorpointMax = Random.Range(defaultArmorpointMax-500,defaultArmorpointMax+1000);
        Armorpoint = ArmorpointMax;
        transform.localScale = new Vector3((float)ArmorpointMax / 1000f * 1.3f, (float)ArmorpointMax / 1000f * 1.3f, (float)ArmorpointMax / 1000f * 1.3f);
        AudioSource[] audioSouces = gameObject.GetComponents<AudioSource>();
        hitAudio = audioSouces[0];
        enemyShot = audioSouces[1];

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer < 5)
        {
            enemylevel = 1;
        }
        else if (timer < 10)
        {
            enemylevel = 2;
        }
        else if (timer < 15)
        {
            enemylevel = 3;
        }
        else if (timer >= 15)
        {
            enemylevel = 4;
            //レベル4:攻撃間隔が短くなる
            shotIntervalMax = 0.5f;
        }
        if (enemylevel >= 2)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= 30.0f)
            {
                if (DetectTarget())
                {
                    Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

                    shotInterval += Time.deltaTime;

                    if (shotInterval > shotIntervalMax)
                    {
                        //Debug.Log(DetectTarget());

                        enemyShot.PlayOneShot(enemyShot.clip);
                        Instantiate(shot, EnemyMuzzle.transform.position, EnemyMuzzle.transform.rotation);
                        shotInterval = 0.0f;
                    }
                }
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

                transform.position += transform.forward * Time.deltaTime * 20;
            }
            }
	}

    private void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.tag == "Shot")
        {
            hitAudio.PlayOneShot(hitAudio.clip);
            // damage = Random.Range(50,150);
            damage = Random.Range(shotPlayer.damage-10,shotPlayer.damage+20);//collider.gameObject.GetComponent<shotPlayer>().damage;

            Armorpoint -= damage;

            BattleManager.scorePoints += damage;

            //Debug.Log(Armorpoint);
            if (Armorpoint <= 0)
            {
                //EnemyInstantiate script = system.GetComponent<EnemyInstantiate>();
                //静的オブジェクトのためゲットする必要がない
                EnemyInstantiate.instantiateValue++;
                //リザルト用のスコア追加:撃破数
                BattleManager.score++;
                BattleManager.scorePoints += ArmorpointMax;
                //Debug.Log(EnemyInstantiate.instantiateValue);
                Debug.Log(BattleManager.score);
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }

        }
    }

    bool DetectTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    return true;
                }
            }

        return false;
    }
}
