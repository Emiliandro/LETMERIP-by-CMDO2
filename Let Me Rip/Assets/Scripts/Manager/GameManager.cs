using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    OnPause,
    Finish,
    Loading
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameState gameState;

    public Player[] PlayersInGame;

    public float TimeToFinish;

    private int PlayersNumber;

    private void Awake()
    {
        if(instance != null){
            Destroy(this.gameObject);
            return;
        }else{
            instance = this;
        }

        gameState = GameState.Loading;
    }



    void FixedUpdate()
    {
        if(TimeToFinish > 0){
            TimeToFinish -= Time.fixedDeltaTime;
        }

        if(TimeToFinish <= 0){
            EndGame();
        }

    }

    public void PauseGame(){
        if (gameState != GameState.OnPause)
            gameState = GameState.OnPause;
        else
            gameState = GameState.Playing;
    }

    private void EndGame(){
        print("Acabou o jogo");
    }


}
