using UnityEngine;
public class MSGameStateManager : Singleton<MSGameStateManager>
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
