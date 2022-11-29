using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Restart_Btn : MonoBehaviour
{
    public void OnClick()
    {
        GameplayManager.manager.RestartGame();
    }

}
