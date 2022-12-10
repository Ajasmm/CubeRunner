using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField][Range(0, 10)] float speed = 5;
    [SerializeField] float range = 45F;
    [SerializeField] float time;


    float sine;
    Vector3 localEuler;

    Transform myTransform;
    Rigidbody rBody;    

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        localEuler = Vector3.zero;


        rBody = myTransform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime * speed / 10;
        time %= 2;
        sine = Mathf.Sin(time * Mathf.PI);

        localEuler.z = range * sine;

        rBody.rotation = Quaternion.Euler(localEuler);
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
