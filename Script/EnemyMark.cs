using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class EnemyMark : MonoBehaviour {

    public GameObject enemyMark_o;
    public GameObject place;
    GameObject target;

    GameObject enemyMark;

    float markScale=0;
    float markScaleConnect=0;

	// Use this for initialization
	void Start() {
        target = GameObject.FindWithTag("Player");
        //enemyMark.enabled = false;
        enemyMark = Instantiate(enemyMark_o, new Vector3(transform.position.z, transform.position.y + 1.4f, transform.position.z), Quaternion.identity) as GameObject;
        enemyMark.transform.SetParent(place.transform, false);
        enemyMark.transform.localRotation = new Quaternion(45f, 45f, 45f, 0.5f);
        //enemyMark.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(DetectTarget());

        //Debug.DrawLine(transform.position, target.transform.position, DetectTarget() ? Color.red : Color.green);

        //enemyMark.SetActive(DetectTarget());
        //enemyMark.transform.position = new Vector3(transform.localPosition.z,transform.localPosition.y+1.4f, transform.localPosition.z);
        //Vector3 imageTargetPosition = new Vector3(transform.position.z, transform.position.y + 1.4f, transform.position.z);
        enemyMark.transform.Rotate(5f, -5f, 5f);
        enemyMark.transform.localPosition = new Vector3(0, 0.9f, 0);

        markScale = Vector3.Distance(target.transform.position, this.transform.position);
        //Debug.Log(markScale);
        markScaleConnect = markScale/100f;
        enemyMark.transform.localScale = new Vector3(markScaleConnect,markScaleConnect,markScaleConnect);
    }

   /* bool DetectTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        RaycastHit hit;

        if (Vector3.Angle(transform.forward, direction) < 50f)
        {
            if(Physics.Raycast(transform.position,direction,out hit))
            {
                if (hit.transform == target.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }
    */
}
