using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;
    public float velocity;

    public float timeDestroy = 5f;
    public string tagTarget;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagTarget)){
            CombatBase col = collision.GetComponent<CombatBase>();
            col.TakeDamage(damage);
            print("acertou> " + collision.name);
            DestroyBullet();
        }

        if (collision.CompareTag("Obstacle")){
            DestroyBullet();
        }

    }

    private void DestroyBullet(){
        Destroy(this.gameObject);
    }
}
