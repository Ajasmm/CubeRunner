using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinishWindow : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;

    private void OnEnable()
    {
        if (GameplayManager.manager != null)
            m_Text.text = "Won\n" + GameplayManager.manager.score + "\nPress 'Space' to start";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GameplayManager.manager.RestartGame();
    }

}
