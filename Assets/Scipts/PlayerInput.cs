using UnityEngine;

public struct PlayerInput
{
    public bool jump;
    public Vector3 movement;

    public PlayerInput(bool temp)
    {
        jump = false;
        movement = Vector3.zero;
    }
}
