using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propellor : MonoBehaviour
{
    [SerializeField] float speed = 30F;
    [SerializeField][Range(-1, 1)] int direction = 1;

    Vector3 euler;

    Transform myTransform;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        euler = myTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        euler.x += direction * speed * Time.deltaTime;
        euler.x %= 360F;

        myTransform.eulerAngles = euler;
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
