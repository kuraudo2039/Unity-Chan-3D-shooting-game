using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject shot;
    public GameObject muzzle;
    public GameObject CameraParent;
    public GameObject muzzleFlash;

    private Animator animator;

    private Quaternion muzzleangle;
    private float shotInterval = 0.0f;
    private float shotangle;
    private int shotid;
    //構えるときのカメラズームの場所(ターゲット)
    float timer_1 = 0.0f;
    float timer_2 = 0.0f;
    float x, z,t_1,t_2;

    public static float shotIntervalMax=0.30f;

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    public AudioClip shotAudio;
    public AudioClip setAudio_S;
    public AudioClip setAudio_E;

	// Use this for initialization
	void Start () {
        shotIntervalMax=0.30f;
        animator = GetComponent<Animator>();
        shotid = Animator.StringToHash("Shot");
        animator.SetFloat(shotid, -0.5735f);

        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];
	}
	
	// Update is called once per frame
	void Update () {
        //カーソルを合わせると敵の体力を表示



        shotangle = CameraParent.transform.localRotation.x;
        shotInterval += Time.deltaTime;
        //カメラズームのクールタイム
        if(Input.GetButtonDown("TrueHandGunSet"))
        {
            timer_1 = 0.2f;
            timer_2 = 0.2f;
            t_1 = 0f;
            t_2 = 0f;

            audioSource2.PlayOneShot(setAudio_S);
        }
        if (Input.GetButton("TrueHandGunSet"))
        {

            animator.SetBool("T_HandGun", true);
            //メインカメラをターゲットローカルポジションへ移動
            if (timer_1 > 0)
            {
               // x = Mathf.Lerp(0.5f, 0.23f, Time.deltaTime*10);
              //  z = Mathf.Lerp(-3.16f, -0.5f, Time.deltaTime*10);
                t_1 += Time.deltaTime;
                Camera.main.transform.localPosition = new Vector3(Mathf.Lerp(0.27f, 0.35f, t_1*5), 0.086f, Mathf.Lerp(-3.16f, -0.5f, t_1*5));
                timer_1 -= Time.deltaTime;
            }
            animator.SetFloat(shotid, shotangle);

            if (Input.GetButton("Fire1"))
            {

                if (shotInterval > shotIntervalMax)
                {
                    //muzzle(発射開始位置)指定
                    muzzleangle = new Quaternion(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y+0.003f, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);
                    Instantiate(shot, muzzle.transform.position, muzzleangle);
                    shotInterval = 0.0f;
                    Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);

                    audioSource1.PlayOneShot(shotAudio);
                }
            }
        }
        else
        {
            animator.SetBool("T_HandGun", false);
            //カメラを初期位置へ
            if (timer_2 > 0)
            {
                //  x = Mathf.Lerp(0.23f, 0.5f, 1-Time.deltaTime);
                // z = Mathf.Lerp(-0.5f, -3.16f,1- Time.deltaTime);
               // Debug.Log(t_2);
                t_2 += Time.deltaTime;
                Camera.main.transform.localPosition = new Vector3(Mathf.Lerp(0.35f, 0.27f, t_2*5), 0.086f, Mathf.Lerp(-0.5f, -3.16f, t_2*5));
                timer_2 -= Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("TrueHandGunSet"))
        {
            //audioSource.clip = shotAudio;
            audioSource2.PlayOneShot(setAudio_E);
        }
    }
}
