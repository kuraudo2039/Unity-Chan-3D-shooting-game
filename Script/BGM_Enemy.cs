using UnityEngine;
using System.Collections;

public class BGM_Enemy : MonoBehaviour {

    AudioSource field;
    AudioSource battle;

    //GameObject player;
    GameObject target;
    public GameObject infinityDistance;
    GameObject BGMswitch;

    bool setBGM;
    bool stopBGM;

    float fadeDeltaTime=0;
    float fadeDeltaTime1=0;
    public float fadeSeconds;

    float sourceVolume=0.4f;
	// Use this for initialization
	void Start () {
        //player = GameObject.FindWithTag("Player");

        setBGM = false;
        stopBGM = false;
        AudioSource[] audioSoures = gameObject.GetComponents<AudioSource>();
        field = audioSoures[0];
        battle = audioSoures[1];
	}
	
	// Update is called once per frame
	void Update () {
        target = GameObject.FindWithTag("Enemy");

        //Debug.Log(BGMswitch);
        //Debug.Log(fadeDeltaTime);
        //Debug.Log(FindClosestEnemy().name);
        /* Enemy targetScript = FindClosestEnemy().GetComponent<Enemy>();
         if (targetScript.Armorpoint <= 0)
         {
             Debug.Log("ExitEnemy");
         }*/


        //Debug.Log(fadeDeltaTime);
        if (setBGM == true&&field.enabled==true)
        {
            battle.enabled = true;
            fadeDeltaTime += Time.deltaTime;
            battle.volume = (fadeDeltaTime / fadeSeconds) * sourceVolume;
            field.volume = (1.0f-fadeDeltaTime / fadeSeconds) * sourceVolume;
            if (fadeDeltaTime > fadeSeconds||battle.volume>=sourceVolume)
            {
                //if(field.enabled==true)
                fadeDeltaTime = 0f;
                field.enabled = false;
                setBGM = false;

            }
        }
        else
        {
            if (stopBGM==true&&battle.enabled==true)
            {
                field.enabled = true;
                fadeDeltaTime += Time.deltaTime;
                battle.volume = (1 - fadeDeltaTime / fadeSeconds) * sourceVolume;
                field.volume = (fadeDeltaTime / fadeSeconds) * sourceVolume;
                if (fadeDeltaTime > fadeSeconds || battle.volume <= 0)
                {
                   // if (battle.enabled == true)
                    //{
                        fadeDeltaTime = 0.0f;
                    //}
                    battle.enabled = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ("Enemy")&&BGMswitch==null)
        {
            setBGM = true;
            stopBGM = false;
            //fadeDeltaTime1 = 0;
        }
    }

    /*void OnTriggerStay(Collider collider)
    {
        if (FindClosestEnemy() == null)
        {
            setBGM = false;
            //battle.enabled = false;
        }
    }
    */

    /*void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == ("Enemy"))
        {

            setBGM = false;
            stopBGM = true;
        }
    }*/

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = player.transform.position;

        //gos(配列)をgo(変数)を使って順に比較
        //敵の距離を比較して一番近い敵をclosestに入れる
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;

                BGMswitch = closest;
                distance = curDistance;
            }
        }

        //距離が離れたらロックを解除
        if (target != null)
        {
            if (Vector3.Distance(closest.transform.position, transform.position) > 42.0000000000000f)
            {
                setBGM = false;
                stopBGM = true;
                BGMswitch = null;
            }
        }

        return closest;
    }
}
