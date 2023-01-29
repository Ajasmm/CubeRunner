using UnityEngine;
using TMPro;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;

    private void OnEnable()
    {
        if(GameplayManager.manager != null)
            m_Text.text = "Game Over\n" + GameplayManager.manager.score + "\nPress 'Space' to start";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameplayManager.manager.RestartGame();
            Debug.Log("In GameOverWIndow");
        }
        Debug.Log("Game over window Finished");
    }

}
