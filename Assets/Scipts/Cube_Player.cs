using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube_Player : MonoBehaviour
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float addedSpeed = 15;

    [SerializeField] GameState playerState;
    Vector3 movement;
    PlayerInput playerInput;

    Rigidbody rbody;
    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        SetState(GameState.Ready);
        rbody = GetComponent<Rigidbody>();
        myTransform = transform;

        GamePlayManager.manager.gamePlayer = this;
        GamePlayManager.manager.playerTransform = myTransform;

        playerInput = new PlayerInput(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == GameState.Dead)
            return;

        if(myTransform.position.y < -2)
            SetState(GameState.Dead);

        InputHandle();
    }

    private void FixedUpdate()
    {
        rbody.MovePosition(myTransform.position + playerInput.movement);
        playerInput.movement = Vector3.zero;
    }

    void Jump(Vector3 direction)
    {
        SetState(GameState.Jumping);
        rbody.AddForce(Vector3.up * 10 * rbody.mass, ForceMode.Impulse);
        playerInput.movement += direction;
    }
    protected void Move(Vector3 direction)
    {
        playerInput.movement += direction;

        // if not looking at forward rotate the cube towards the forward direction
        if(Vector3.Dot(myTransform.forward, Vector3.forward) != 1)
            myTransform.LookAt(Vector3.Lerp(myTransform.forward, myTransform.position + Vector3.forward, 0.5f));
    }

    private void InputHandle()
    {
        
        switch (playerState)
        {
            case GameState.Running:
                movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                movement.z = (speed + (Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * addedSpeed)) * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space)) Jump(movement);
                else Move(movement);
                break;
            case GameState.Jumping:
                movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                movement.z = (speed + (Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * addedSpeed)) * Time.deltaTime;
                Move(movement);
                break;
        }

    }

    public void SetState(GameState state)
    {
        playerState = state;
        GamePlayManager.manager.SetGameState(playerState);

        if(state == GameState.Dead)
        {
            GamePlayManager.manager.EndGame();
        }
    }

    public GameState GetState()
    {
        return playerState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" && !(playerState == GameState.Ready || playerState == GameState.Dead))
            SetState(GameState.Running);
    }
}
