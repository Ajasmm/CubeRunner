using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube_Player : MonoBehaviour
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float addedSpeed = 15;

    [SerializeField] PlayerState playerState;
    Vector3 movement;

    Rigidbody rbody;
    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.Ready;
        rbody = GetComponent<Rigidbody>();
        myTransform = transform;

        GameplayManager.manager.gamePlayer = this;
        GameplayManager.manager.playerTransform = myTransform;

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        InputHandle();
    }

    void Jump(Vector3 direction)
    {
        playerState = PlayerState.Jumping;
        rbody.AddForce(Vector3.up * 10 * rbody.mass, ForceMode.Impulse);
        Move(direction);
    }
    void Fly()
    {
        
    }
    protected void Move(Vector3 direction)
    {
        rbody.MovePosition(myTransform.position + movement);
        if(Vector3.Dot(myTransform.forward, Vector3.forward) != 1)
            myTransform.LookAt(Vector3.Lerp(myTransform.forward, myTransform.position + Vector3.forward, 0.5f));
    }

    private void InputHandle()
    {
        
        switch (playerState)
        {
            case PlayerState.Ready:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerState = PlayerState.Running;
                    GameplayManager.manager.StartGame();
                }
                break;
            case PlayerState.Running:
                movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                movement.z = (speed + (Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * addedSpeed)) * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space)) Jump(movement);
                else Move(movement);
                break;
            case PlayerState.Jumping:
                movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                movement.z = (speed + (Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * addedSpeed)) * Time.deltaTime;
                Move(movement);
                break;
            case PlayerState.Flying:
                movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                movement.z = (speed + (Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * addedSpeed)) * Time.deltaTime;
                Move(movement);
                break;
            case PlayerState.Dead:
                break;
        }

    }

    public void SetState(PlayerState state)
    {
        playerState = state;

        if(state == PlayerState.Dead)
        {
            GameplayManager.manager.EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && !(playerState == PlayerState.Ready || playerState == PlayerState.Dead))
            playerState = PlayerState.Running;
    }
}

public enum PlayerState
{
    Ready,
    Running,
    Jumping,
    Flying,
    Dead
}
