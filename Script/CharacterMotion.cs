using UnityEngine;
using System.Collections;

public class CharacterMotion : MonoBehaviour {

    private Animator animator;
    private Animation resultAnimation;
    private float x, y;
    private int xi, yi;
    private int Blendid;
    public static int integer;

    public static int result;


	// Use this for initialization
	void Start () {
        integer = 0;
        animator = GetComponent<Animator>();
        Blendid = Animator.StringToHash("Blend");
    }
	
	// Update is called once per frame
	void Update () {
        animator.SetInteger("Result",integer);
        /*if (integer != 0)
        {
            animator.SetInteger("Result", 0);
        }*/
        //歩行アニメーションブレンドの細かな調整
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (y >0.1f || x!=0.0f)
        {
            animator.SetInteger("Vertical",1);
            animator.SetInteger("Horizontal", 1);
            animator.SetFloat(Blendid, x);
            if (Input.GetButton("Run") && !Input.GetKey("s"))
            {
                animator.SetBool("Run",true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
        else if (y < -0.1f)
        {
            animator.SetInteger("Vertical", -1);
        }
        else
        {
            animator.SetInteger("Vertical", 0);
            animator.SetInteger("Horizontal", 0);
            animator.SetBool("Run", false);
        }

        //終了時アニメーション
/*        if (result == 1)
        {
            integer = 1;
        }
        if (result == 2)
        {
            animator.SetInteger("Result", 2);
            integer = 2;
        }
        */
	}
}
