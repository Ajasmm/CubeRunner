using System.Collections;
using UnityEngine;

public class GameReadyWindow : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForEndOfFrame();

        GameplayManager.manager.gamePlayer.SetState(GameState.Running);
        GameplayManager.manager.RunGame();
    }
}
