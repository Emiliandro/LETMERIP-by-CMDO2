using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentBase : MonoBehaviour {


    private Animator anim;
    private Transform tf;
    private Rigidbody2D rb2D;

    [SerializeField]
    private Transform groundedCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private bool facingRight;

    public float jumpForce = 10.0f;
    public float speed;
    public float maxSpeed;

    public bool canDash = true;
    public float dashCooldown = 2f;
    public float dashSpeed = 3;



    // Use this for initialization
    void Start () {

        rb2D = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.OverlapCircle(groundedCheck.position, 0.2f, groundLayer);

        float h = Input.GetAxis("Horizontal");

        Move(h);
        if (Input.GetKeyDown(KeyCode.Z)){
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X) && canDash){
            StartCoroutine(DashBoost(dashCooldown));
        }


	}

    public void Move(float direction){

        rb2D.AddForce((Vector2.right * speed) * direction);

        if (rb2D.velocity.x > maxSpeed){
            rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
        }
        if(rb2D.velocity.x < -maxSpeed){
            rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
        }

        if (direction > 0 && !facingRight)
            Flip();
        if (direction < 0 && facingRight)
            Flip();


    }

    private void Flip(){
        facingRight = !facingRight;
        
    }

    public void Jump(){
        if (isGrounded){
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    public IEnumerator DashBoost(float dashDur){

        canDash = false;

        if (facingRight){
            rb2D.AddForce(Vector2.right * dashSpeed);
        }else{
            rb2D.AddForce(Vector2.right * -dashSpeed);
        }

        yield return new WaitForSeconds(dashDur);
        canDash = true;

    }


}
