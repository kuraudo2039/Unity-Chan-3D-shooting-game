using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PlayerAp : MonoBehaviour {

    public static int armorpoint;
    public static int armorpointMax = 5000;
    //表示体力
    int displayArmorPoint;

    int damage = 100;

    public Text armorText;

    public Color myWhite;
    public Color myYellow;
    public Color myRed;

    public Image gaugeImage;

    private AudioSource damageAudio;

	// Use this for initialization
	void Start () {
        armorpointMax = 5000;
        armorpoint = armorpointMax;
        displayArmorPoint = armorpoint;

        AudioSource[] audioSouces = GetComponents<AudioSource>();
        damageAudio = audioSouces[2];
	}
	
	// Update is called once per frame
	void Update () {
        //現在の体力と表示する体力が異なっていれば、表示されている体力まで加減算する。
        if (displayArmorPoint != armorpoint)
        {
            displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorpoint, 0.14f);
        }
        //表示するフォーマット(1000->0900)のように右揃え
        armorText.text = string.Format("{0:0000}/{1:0000}",displayArmorPoint,armorpointMax);

        //残り体力により色を変える
        float percentageArmorPoint = (float)displayArmorPoint / armorpointMax;
        //五割以上なら白、未満なら黄色、三割未満なら赤
        //直接指定する場合は、
        /*
         armorText.color=new Color(float(R),float(G),float(B),float(アルファ値))
         ※アルファ値は透明度、省略すれば自動的に不透明
         */
        if (percentageArmorPoint > 0.5f)
        {
            //armorText.color = Color.white;
            armorText.color = myWhite;
            gaugeImage.color = new Color(0.25f, 0.7f, 0.6f);
        }
        else if (percentageArmorPoint > 0.3f)
        {
            //armorText.color = Color.yellow;
            armorText.color = myYellow;
            gaugeImage.color = myYellow;
        }
        else
        {
            //armorText.color = Color.red;
            armorText.color = myRed;
            gaugeImage.color = myRed;
        }

        //ゲージの長さを体力に合わせて伸縮
        gaugeImage.transform.localScale = new Vector3(percentageArmorPoint, 1, 1);
	}

    private void OnCollisionEnter(Collision collider)
    {
        //敵の弾と衝突したらダメージ
        if (collider.gameObject.tag == "ShotEnemy")
        {
            damageAudio.PlayOneShot(damageAudio.clip);

            armorpoint -= damage;
            armorpoint = Mathf.Clamp(armorpoint, 0, armorpointMax);
        }
    }
}
