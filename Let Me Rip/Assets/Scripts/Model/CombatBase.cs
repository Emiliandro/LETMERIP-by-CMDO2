using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player))]
public class CombatBase : MonoBehaviour {

    public float damageBase;
    public float speedShootBase;
    public float cooldownShoot;

    public bool isVulnerable;
    public float lifeCurrent;

    public bool isCooldown;

    public GameObject shoot;

    private Rigidbody2D rb2D;

    private Player myPlayer;
    public Transform shootPointReference;

    public float dashForce;
    public float dashCooldown;
    public bool canDash = true;

    [SerializeField]
    private string PlayerEnemy;

    private void Start()
    {
        PlayerEnemy = (this.gameObject.tag == "Player1") ? "Player2" : "Player1";
        rb2D = GetComponent<Rigidbody2D>();
        myPlayer = GetComponent<Player>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.X) && canDash && (myPlayer.StaminaDash < myPlayer.Stamina)){
            StartCoroutine(DashAction(0.2f));
            myPlayer.Stamina -= myPlayer.StaminaDash;
        }


        if (Input.GetKeyUp(KeyCode.Space) && isCooldown){
            Shoot();
        }



    }


    private IEnumerator DashAction(float dashDur)
    {
        float time = 0;
        canDash = false;

        myPlayer.cantMove = true;

        isVulnerable = false;

        while(dashDur > time){
            time += Time.deltaTime;
            if (!myPlayer.getFacingRight())
                rb2D.velocity = new Vector2(-dashForce, 0);
            else
                rb2D.velocity = new Vector2(dashForce, 0);
            yield return 0;
        }
        rb2D.velocity = Vector2.zero;
        myPlayer.cantMove = false;
        isVulnerable = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        
    }

    public void TakeDamage(float damage){
        if (!isVulnerable)
            return;
        print("Tomei " + damage + " de dano");
        lifeCurrent -= damage;
        if(lifeCurrent > 0){
            myPlayer.Die();
        }
        isVulnerable = false;
        Invoke("ResertVulnerable", 1f);

    }

    private void ResertVulnerable(){
        isVulnerable = true;
    }

    private void ResertCooldown(){
        isCooldown = true;
    }


    private void Shoot(){
        if (!isCooldown){
            return;
        }

        isCooldown = false;

        if (myPlayer.getFacingRight()){
            Rigidbody2D bulletRb2D = (Instantiate(shoot, shootPointReference.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();

            bulletRb2D.velocity = new Vector2(speedShootBase, bulletRb2D.velocity.y);
            bulletRb2D.GetComponent<Bullet>().tagTarget = PlayerEnemy;
            bulletRb2D.GetComponent<Bullet>().damage = damageBase;
        }
        else{
            Rigidbody2D bulletRb2D = (Instantiate(shoot, shootPointReference.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();

            bulletRb2D.velocity = new Vector2(-speedShootBase, bulletRb2D.velocity.y);
            bulletRb2D.GetComponent<Bullet>().tagTarget = PlayerEnemy;
        }

        Invoke("ResertCooldown", cooldownShoot);
    }

    


}
