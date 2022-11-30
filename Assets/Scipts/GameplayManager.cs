using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [SerializeField] GameObject gameStartWindow;
    [SerializeField] GameObject gameRunningWindow;
    [SerializeField] GameObject gameEndWindow;

    public Action<GameState> OnGameState;

    public Cube_Player gamePlayer;
    public Transform playerTransform;

    public float score;

    public static GameplayManager manager;

    GameState gameState;

    private void OnEnable()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this);

        Readygame();

    }

    public void Readygame()
    {
        if (gameStartWindow != null)
            gameStartWindow.SetActive(true);

        if (gameRunningWindow != null)
            gameRunningWindow.SetActive(false);

        if (gameEndWindow != null)
            gameEndWindow.SetActive(false);
    }

    public void StartGame()
    {
        if (gameStartWindow != null)
            gameStartWindow.SetActive(false);

        if (gameRunningWindow != null)
            gameRunningWindow.SetActive(true);

        if (gameEndWindow != null)
            gameEndWindow.SetActive(false);
    }

    public void EndGame()
    {
        if (gameStartWindow != null)
            gameStartWindow.SetActive(false);

        if (gameRunningWindow != null)
            gameRunningWindow.SetActive(false);

        if (gameEndWindow != null)
            gameEndWindow.SetActive(true);
    }

    public GameState GetGameState()
    {
        return gameState;
    }
    public void SetGameState(GameState state)
    {
        this.gameState = state;
        if (OnGameState != null)
            OnGameState(state);
    }

    public void RestartGame()
    {
        Readygame();
        gamePlayer.SetState(GameState.Ready);

        Rigidbody rBody = gamePlayer.GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.ResetInertiaTensor();
        gamePlayer.transform.position = Vector3.zero + Vector3.forward;
        gamePlayer.transform.rotation = Quaternion.identity;

    }
}
