using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1.5f;
    public float runspeed = 3.0f;
    public float jumpPower = 8.0f;
    public float Gravity = 20.0f;

    private Animator animator;
    private Vector3 move = Vector3.zero;
    private CharacterController controller;
    //1フレーム前のrotation

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        animator.SetBool("Jump", false);
        //Playerが地面にいるとき
        if (controller.isGrounded)
        {

            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;
            if (Input.GetButton("Run") && !Input.GetKey("s"))
            {
                move *= runspeed;
            }
            if (Input.GetButtonDown("Jump"))
            {
                move.y = jumpPower;
                animator.SetBool("Jump", true);
            }
        }

            move.y -= Gravity * Time.deltaTime;
        controller.Move(move * Time.deltaTime);
    }
}
