using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour{


    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    float accelarationTimeAirbone = .2f;
    float accelarationTimeGrounded = .1f;

    private float gravity = -20;
    private Vector3 velocity;

    float velocityXSmoothing;

    private float jumpVelocity;
    public float moveSpeed;


    Controller2D controller;

    void Start(){
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        Debug.Log("Gravity: " + gravity);
        Debug.Log("JumpVelocity: " + jumpVelocity);

    }

    private void Update(){

        if(controller.collisions.above || controller.collisions.bellow)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Z) && controller.collisions.bellow){
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.bellow)?accelarationTimeGrounded:accelarationTimeAirbone);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}