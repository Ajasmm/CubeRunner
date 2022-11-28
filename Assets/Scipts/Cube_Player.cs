using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube_Player : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float speedAddition = 5;
    [SerializeField] float jumpForce = 50;

    float deltaTime = 0, currentSpeed = 0;
    Vector3 movement = Vector3.zero;

    Transform myTransform;
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;

        currentSpeed = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * speedAddition;
        movement.z = (currentSpeed + speed) * deltaTime;
        movement.x = Input.GetAxis("Horizontal") * speed * deltaTime;

        myTransform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blocks")
        {
            Time.timeScale = 0;
        }
    }
}
