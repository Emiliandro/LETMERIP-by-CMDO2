using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SelectScreenManagers : MonoBehaviour {

	public KeyCode p1_left,p1_right,p2_left,p2_right,p1_go,p2_go;
	public int p1_position,p2_position;
	public int[] player_select;

	// Use this for initialization
	void Start () {
		p1_position = player_select [0];
		p2_position = player_select [player_select.Length - 1];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		P1_Seletor ();
		P2_Seletor ();
	}

	protected void P1_Seletor(){
		int player_2_hightlight = p2_position;

		if (Input.GetKeyDown(p1_left)) {
			if (player_2_hightlight == p1_position - 1) {
				p1_position = p1_position - 2;
			} else if (p1_position == 0 && p2_position == 3) {
				p1_position = 2;
			} else if (p1_position <= 0) {
				p1_position = 3;
			} else {
				p1_position = p1_position - 1;
			}
		}

		if (Input.GetKey(p1_right)) {
			if (player_2_hightlight == p1_position + 1) {
				if (p1_position + 2 >= player_select [player_select.Length - 1]) {
					p1_position = 0;
				} else {
					p1_position = p1_position + 2;
				}
			} else {
				p1_position = p1_position + 1;
			}

		}
	}
	protected void P2_Seletor(){
		int player_1_hightlight = p1_position;

		if (Input.GetKeyDown(p2_left)) {
			if (player_1_hightlight == p2_position - 1) {
				if (p2_position - 2 <= 0) {
					p2_position = 3;
				} else {
					p2_position = p2_position - 2;
				}
			} else if (p2_position <= 0 && p1_position == 3) {
				p2_position = 2;
			} else if (p2_position == 0) {
				p2_position = 3;
			} else {
				p2_position = p2_position - 1;
			}
		}

		if (Input.GetKey(p2_right)) {
			
		}
	}

	protected void GetPlayer(int personagem, int jogador){
	}
}
