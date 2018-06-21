using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class EnemyApGauge : MonoBehaviour {

    GameObject refObj;

    //敵の体力
    public GameObject enemyAp;
    public Image gaugeImage;
    //敵との距離
    public Text textDistance;
    //float Distance;

    Vector3 direction;
    // Use this for initialization
    void Start () {
        enemyAp.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //カーソルを合わせると敵の体力を表示
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            //何に当たったのか表示
            //Debug.Log(hit.collider.gameObject.name);
            //まっすぐに線を表示(Raycastの通り道)
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, Mathf.Infinity);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyAp.SetActive(true);
                refObj = hit.collider.gameObject;
                //敵と残りを計算
                //Distance = Vector3.Distance(transform.position, hit.transform.position);
                textDistance.text = Vector3.Distance(transform.position, hit.transform.position).ToString();
            }
            else
            {
                enemyAp.SetActive(false);
            }
        }
        //Enemyのタグに反応
        if (refObj.tag == "Enemy")
        {
            //そいつからコンポネントをゲットするためのGameObject
            GameObject enemy = refObj;
            //敵の頭上にHP表示
            Vector3 imageTargetPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, enemy.transform.position.z);
            Enemy targetScript = enemy.GetComponent<Enemy>();
            //ゲットしたコンポネントをもとにHPゲージの縮小
            gaugeImage.transform.localScale = new Vector3((float)targetScript.Armorpoint / targetScript.ArmorpointMax, 1, 1);
            //Debug.Log(targetScript.Armorpoint);
            enemyAp.transform.position = Camera.main.WorldToScreenPoint(imageTargetPosition);
            if (targetScript.Armorpoint / targetScript.ArmorpointMax <= 0)
            {
                refObj = null;
            }
        }
        else
        {

            //探せなかったらデバッグログにて探せないと表示
            Debug.Log("can\'t find enemies");
        }
    }
}
