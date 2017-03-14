using UnityEngine;
using System.Collections;

public class SplashImage : MonoBehaviour {

   [Header("Indique o tempo da ação")]
    public float tempo;
   [Header("Indique o objeto da ação")]
    public SpriteRenderer sprite;
   [Header("Deseja mudar de cena")]
    public bool trocarcena;
    public string cenaseguinte;


    void Start() {
        sprite.color = Color.clear;
        Debug.Log("fadein");
        StartCoroutine("FadeIn");
        Invoke("StartFadeOut", tempo * 2);
        if (trocarcena == true) Invoke("CenaCarregar", tempo * 3);
    }

    void StartFadeOut(){
        Debug.Log("fadeout");
        StartCoroutine("FadeOut");

    }

    IEnumerator FadeIn(){
        while (sprite.color.a < 0.99f){
            sprite.color = new Color(1f, 1f, 1f, sprite.color.a + (Time.deltaTime / 2));
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }
    }

    IEnumerator FadeOut(){
        while (sprite.color.a > 0.01f){
            sprite.color = new Color(1f, 1f, 1f, sprite.color.a - (Time.deltaTime / 2));
            yield return new WaitForSeconds(Time.deltaTime / 4);
        }
    }

    void CenaCarregar() {
        Application.LoadLevel(cenaseguinte);

    }

}
