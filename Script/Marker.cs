using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class Marker : MonoBehaviour {

    Image marker;
    public Image markerImage;
    GameObject compass;

    GameObject target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("PlayerTarget");

        //マーカーをレーダー上に表示
        compass = GameObject.Find("CompassMask");
        marker = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
        marker.transform.SetParent(compass.transform, false);
	}
	
	// Update is called once per frame
	void Update() {
        //マーカーをプレイヤー相対位置に表示
        Vector3 position = transform.position - target.transform.position;
        marker.transform.localPosition = new Vector3(position.x, position.z, 0);

        //マーカーがレーダー範囲外なら表示しない
        /*if (Vector3.Distance(target.transform.position, transform.position) <= 150)
        {
            marker.enabled = true;
        }
        else
        {
            marker.enabled = false;
        }
        */
	}

    void OnDestroy()
    {
        Destroy(marker);
    }
}
