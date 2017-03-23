using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour{


    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    float accelerationTimeGrounded = .2f;
    float accelerationTimeAirborne = .1f;

    private float gravity = -20;
    private Vector3 velocity;

    private float maxStamina = 20;
    public float Stamina;

    public float StaminaJump = 1;
    public float StaminaWallJump = 2;
    public float StaminaDash = 2;
    public float StaminaDoubleJump = 2;

    float velocityXSmoothing;

    private float jumpVelocity;
    public float moveSpeed;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    [SerializeField]
    private bool facingRight = true;

    public ScriptableObject MyCharDataBase;


    Controller2D controller;

    public bool getGrounded(){
        return controller.collisions.below;
    }
    

    public bool isDie = false;

    public bool getFacingRight(){
        return facingRight;
    }

    public bool cantMove = false;
    public bool canDoubleJump;
    private bool doubleJumpEnable = false;
    public bool isActivated = false;


    void Start(){
        controller = GetComponent<Controller2D>();

        Stamina = maxStamina;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;


    }

    private void Update(){
        if (isDie){
            return;
        }

        if(Stamina < maxStamina){
            Stamina += Time.deltaTime;
        }


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (cantMove){
            input = Vector2.zero;
        }
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        if (input.x > 0 && !facingRight) Flip();
        if (input.x < 0 && facingRight) Flip();

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            doubleJumpEnable = true;
        }



        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (wallSliding && (StaminaWallJump < Stamina))
            {
                if(input.x == wallDirX){
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
                Stamina -= StaminaWallJump;
            }
            if (controller.collisions.below && (StaminaJump < Stamina))
            {
                velocity.y = jumpVelocity;
                Stamina -= StaminaJump;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && velocity.y < 0 && doubleJumpEnable){
            if (canDoubleJump && (StaminaDoubleJump < Stamina)){

                velocity.y = jumpVelocity;
                Stamina -= StaminaDoubleJump;

                doubleJumpEnable = false;


            }

        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void Flip()
    {
        facingRight = !facingRight;
        this.gameObject.transform.localScale = new Vector3(-this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
    }


    public void Die(){
        isDie = true;
    }


}