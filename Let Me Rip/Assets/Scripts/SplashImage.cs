using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashImage : MonoBehaviour {

   [Header("Indique o tempo da ação")]
    public float tempo;
   [Header("Indique o objeto da ação")]
    public SpriteRenderer sprite;
	public Text creditos;

   [Header("Deseja mudar de cena")]
    public bool trocarcena;
    public string cenaseguinte;


    void Start() {
        sprite.color = Color.clear;
        Debug.Log("fadein");
        StartCoroutine("FadeIn");
        Invoke("StartFadeOut", tempo * 2);
		Invoke("StartTextFadeIn", tempo * 3);
		Invoke("StartTextFadeOut", tempo * 6);
        if (trocarcena == true) Invoke("CenaCarregar", tempo * 7);
    }

	void FixedUpdate(){
		if (Input.GetKey (KeyCode.Escape))
			CenaCarregar ();
	}
    void StartFadeOut(){
        Debug.Log("fadeout");
        StartCoroutine("FadeOut");
    }
	void StartTextFadeIn(){
		Debug.Log ("FadeIn Creditos");
		StartCoroutine ("FadeInText");
	}
	void StartTextFadeOut(){
		Debug.Log ("FadeOut Creditos");
		StartCoroutine ("FadeOutText");
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

	IEnumerator FadeInText(){
		while (creditos.color.a < 0.99f){
			creditos.color = new Color(1f, 1f, 1f, creditos.color.a + (Time.deltaTime / 2));
			yield return new WaitForSeconds(Time.deltaTime / 2);
		}
		yield return new WaitForSeconds(Time.deltaTime / 4);
	}

	IEnumerator FadeOutText(){
		while (creditos.color.a > 0.01f){
			creditos.color = new Color(1f, 1f, 1f, creditos.color.a - (Time.deltaTime / 2));
			yield return new WaitForSeconds(Time.deltaTime / 4);
		}
		yield return new WaitForSeconds(Time.deltaTime / 4);
	}
}
