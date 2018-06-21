using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class LockOn : MonoBehaviour {

    GameObject target = null;

    bool isSearch;

    public Image lockOnImage;

    //敵の体力

    float timer = 0.0f;
    float lockSpeed=0f;

    Vector3 targetPosition = Vector3.zero;
    Vector3 oldLockOnImagePosition;
    // Use this for initialization
    void Start () {
        isSearch = false;

        lockOnImage.enabled = false;

        lockSpeed = Time.deltaTime * (10f + timer);
        oldLockOnImagePosition = lockOnImage.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      /*  //カーソルを合わせると敵の体力を表示
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            //何に当たったのか表示
            Debug.Log(hit.collider.gameObject.name);
            //まっすぐに線を表示(Raycastの通り道)
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, Mathf.Infinity);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyAp.SetActive(true);
                refObj = hit.collider.gameObject;
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
            GameObject enemy=refObj;
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
        }*/

        if (Input.GetButtonDown("Lock"))
        {
            //ロックオンモード切替
            isSearch = !isSearch;
            if (!isSearch)
            {
                GetComponent<PlayerRotate>().enabled = true;
                GetComponent<T_MouseCamera>().enabled = true;
                target = null;
                timer = 0.0f;
            }
            /*else
            {
                //近くの敵をロックする関数
                //target = FindClosestEnemy();

                //敵を探す
                //target = GameObject.FindWithTag("Enemy");
            }
            */
        }

        //ロックオンモードで敵がいれば敵のほうを向く
        if (isSearch == true)
        {
            if (target != null)
            {
                GetComponent<PlayerRotate>().enabled = false;
                GetComponent<T_MouseCamera>().enabled = false;
                //targetのほうを見る関数(ただしカメラに依存しないため攻撃があたらない)
                //transform.LookAt(target.transform);

                //ロックがだんだん固くなっていく、最大五秒
                if (timer < 60f)
                {
                    lockSpeed = Time.deltaTime * (10f + timer);
                }


                //ロックオン左右(Playerの向き)
                targetPosition = target.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetPosition - Camera.main.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockSpeed);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                //ロックオン上下(親カメラの向き)

                Transform CameraParent = Camera.main.transform.parent;
                Quaternion targetRotation2 = Quaternion.LookRotation(target.transform.position - CameraParent.position);
                CameraParent.localRotation = Quaternion.Slerp(CameraParent.localRotation, targetRotation2, lockSpeed);
                CameraParent.localRotation = new Quaternion(CameraParent.localRotation.x, 0, 0, CameraParent.localRotation.w);
                timer += Time.deltaTime*12f;

            }

            //ロックオンモードでロックしていなければ敵を探す
            else
            {
                GetComponent<PlayerRotate>().enabled = true;
                GetComponent<T_MouseCamera>().enabled = true;
                timer = 0.0f;
                target = FindClosestEnemy();
            }
        }
        

        //距離が離れたらロックを解除する
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > 25)
            {
                target = null;
                timer = 0.0f;
            }
        }

        //ターゲットがいたらロックオンカーソルを表示
        //ロックしているかどうかの変数
        //bool isLocked = false;

        if (target != null)
        {
           // isLocked = true;

            lockOnImage.transform.rotation = Quaternion.identity;

            //ターゲットの表示位置にロックオンカーソルを合わせる
            targetPosition = new Vector3(targetPosition.x, targetPosition.y+0.08f, targetPosition.z);
            lockOnImage.transform.position = Camera.main.WorldToScreenPoint(targetPosition);
        }
        else
        {
            //ロックオンモード時はカーソルを回転する
            lockOnImage.transform.position = oldLockOnImagePosition;
            lockOnImage.transform.Rotate(0, 0, Time.deltaTime * 100);
        }

        lockOnImage.enabled = isSearch;

    }

    //近くの敵を探す関数
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        //gos(配列)をgo(変数)を使って順に比較
        //敵の距離を比較して一番近い敵をclosestに入れる
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        //距離が離れたらロックを解除
        if (target != null)
        {
            if (Vector3.Distance(closest.transform.position, transform.position) > 100)
            {
                closest = null;
            }
        }

        return closest;
    }
}
