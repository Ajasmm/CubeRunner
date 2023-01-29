using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [SerializeField] GameObject gameStartWindow;
    [SerializeField] GameObject gameRunningWindow;
    [SerializeField] GameObject gameEndWindow;
    [SerializeField] GameObject gameFinishWindow;

    public List<GameObject> propellers = new List<GameObject>();

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
        ResetWindows();

        if (gameStartWindow != null)
            gameStartWindow.SetActive(true);
    }
   

    public void RunGame()
    {
        ResetWindows();

        if (gameRunningWindow != null)
            gameRunningWindow.SetActive(true);
    }

    public void EndGame()
    {
        ResetWindows();
        
        if (gameEndWindow != null)
            gameEndWindow.SetActive(true);
    }


    public void WonGame()
    {
        ResetWindows();

        if (gameFinishWindow != null)
            gameFinishWindow.SetActive(true);

        gamePlayer.SetState(GameState.Ready);
    }

    private void ResetWindows()
    {
        if (gameStartWindow != null)
            gameStartWindow.SetActive(false);

        if (gameRunningWindow != null)
            gameRunningWindow.SetActive(false);

        if (gameEndWindow != null)
            gameEndWindow.SetActive(false);

        if (gameFinishWindow != null)
            gameFinishWindow.SetActive(false);

    }


    public void RestartGame()
    {
        Readygame();

        foreach(GameObject obj in propellers) 
            obj.SetActive(true);
        
        propellers.Clear();

        Rigidbody rBody = gamePlayer.GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.ResetInertiaTensor();
        playerTransform.position = Vector3.zero + Vector3.forward;
        playerTransform.rotation = Quaternion.identity;
        Physics.SyncTransforms();   
        gamePlayer.SetState(GameState.Ready);

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
}
