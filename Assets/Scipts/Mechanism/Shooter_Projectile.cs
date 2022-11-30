using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter_Projectile : MonoBehaviour
{
    [SerializeField] float speed = 25;

    Vector3 pos;

    Transform myTransform;
    Rigidbody rBody;
    Shooter shooter;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = myTransform.position;
        pos.z += -speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.gameObject.tag == "Player")
        {
            Vector3 direction = collision.transform.position - collision.contacts[0].point;
            direction.Normalize();
            direction.y = 0.5F;

            if (collision.rigidbody != null)
            {
                collision.rigidbody.AddForce(direction * 150, ForceMode.Impulse);

                Cube_Player player = collision.rigidbody.GetComponent<Cube_Player>();
                player.SetState(GameState.Dead);
            }
        }
    }
}
