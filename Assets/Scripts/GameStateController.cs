using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static GameStateController stateController;

    public GameObject losing_screen;
    public GameObject winning_screen;

    private PlayerController player;
    private GameStates gameState = GameStates.Playing;
    private int enemy_count = 5;

    void Awake()
    {
        if (GameStateController.stateController != null)
        {
            Debug.LogError("GameState Already Inicialized");
            return;
        }
        GameStateController.stateController = this;
        this.player = PlayerController.player;
    }

    public void enemyKilled()
    {
        this.gameState = GameStates.Playing;
        AudioManager.audioManager.changeBackgroundTheme("main");
        if(--enemy_count <= 0)
        {
            this.endGame(true);
        }
    }

    public void battleStateHandler(GameStates state)
    {
        if (state != gameState)
        {
            if(state == GameStates.Battle)
            {
                this.gameState = state;
                AudioManager.audioManager.changeBackgroundTheme("battle");
            }
            else if(state == GameStates.Playing)
            {
                this.gameState = state;
                AudioManager.audioManager.changeBackgroundTheme("main");
            }
        }
    }

    public void playerDied()
    {
        endGame(false);
    }

    public void autoWin()
    {
        endGame(true);
    }

    private void endGame(bool player_won)
    {
        if (this.gameState == GameStates.Playing || this.gameState == GameStates.Battle)
        {
            if (!player_won)
            {
                AudioManager.audioManager.changeBackgroundTheme("death");
                this.losing_screen.SetActive(true);
            }
            else
            {
                AudioManager.audioManager.changeBackgroundTheme("win");
                this.winning_screen.SetActive(true);
            }
        }
    }

}

public enum GameStates
{
    Playing = 0,
    Lost=1,
    Won=2,
    Battle=3
}