using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReadyWindow : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameplayManager.manager.gamePlayer.SetState(GameState.Running);
            GameplayManager.manager.RunGame();
        }
    }
}
