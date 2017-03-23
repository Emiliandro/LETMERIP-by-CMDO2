using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [Header("General configs")]
    public KeyCode start = KeyCode.End, a = KeyCode.A, z = KeyCode.Z, e = KeyCode.E, back = KeyCode.Escape;
    public GameObject Introducao, Menu01, Menu02;
    public string scene_game = "002 - Select", scene_load = "000 - Logos";

    [Header("Menu 01 configs")]
    public Text menu01_game, menu02_configs;
    public KeyCode down_b = KeyCode.DownArrow, up_b = KeyCode.UpArrow;
	private bool carregar_game = false;

	[Header("Efeitos")]
	public GameObject im_transicao;
	public float posicao = 800;

	void Start () {
        Introducao.SetActive(true);
        Menu01.SetActive(false);
        Menu02.SetActive(false);
	}
	
	void Update () {
        PanelManager();
	}

    protected void PanelManager() {
        if (Introducao.active == true){
            if (Input.GetKeyDown(back)) SceneManager.LoadScene(scene_load);

            if (Input.GetKeyDown(start) || Input.GetKeyDown(a) || Input.GetKeyDown(z) || Input.GetKeyDown(e)){
                Menu01.SetActive(true);
                Introducao.SetActive(false);
                Menu02.SetActive(false);

            }
        }

        if (Menu01.active == true) {

            if (Input.GetKeyDown(back)) {
                Start();
            }
            Menu01Manager();
        }

        if (Menu02.active == true){

            if (Input.GetKeyDown(back)){
                Start();
            }
            Menu02Manager();

        }
    }

    protected void Menu01Manager() {

        if (Input.GetKeyDown(up_b)) {
            menu01_game.color = Color.red;
            menu02_configs.color = Color.white;
        }
        if (Input.GetKeyDown(down_b)) {
            menu02_configs.color = Color.red;
            menu01_game.color = Color.white;
        }

        if (menu01_game.color == Color.red) {
            if (Input.GetKeyDown(start) || Input.GetKeyDown(a) || Input.GetKeyDown(z) || Input.GetKeyDown(e))
            {
				carregar_game = true;
            }
        }

        if (menu02_configs.color == Color.red) {
            if (Input.GetKeyDown(start) || Input.GetKeyDown(a) || Input.GetKeyDown(z) || Input.GetKeyDown(e))
            {
				Menu01.SetActive(false);
				Introducao.SetActive(false);
				Menu02.SetActive(true);

            }
        }

    }
    protected void Menu02Manager() {
		menu02_configs.color = Color.white;
		if (Input.GetKeyDown(KeyCode.Escape)) Start();
    }

	void FixedUpdate() {
		if (carregar_game == true)
			Transicao ();
	}

	protected void Transicao(){
		posicao = im_transicao.transform.position.x;

		if (im_transicao.transform.position.x <= -20) {
			SceneManager.LoadScene (scene_game);
		} else {
			im_transicao.transform.position = new Vector3 (posicao - 20 * Time.deltaTime,0,1);
		}

	}

}
