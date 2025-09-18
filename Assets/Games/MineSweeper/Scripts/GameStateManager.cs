using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public GameState CurrentState { get; private set; }



    public enum GameState
    {
        Lobby,
        Game,
        GameOver,
        Paused
    }

    void Start()
    {
        CurrentState = GameState.Game;
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
    }

}
