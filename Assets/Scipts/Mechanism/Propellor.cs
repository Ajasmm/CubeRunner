using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propellor : MonoBehaviour
{
    [SerializeField] float speed = 30F;
    [SerializeField][Range(-1, 1)] int direction = 1;

    Vector3 euler;

    Transform myTransform;
    Rigidbody rBody;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        myTransform = transform;
        euler = myTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(GamePlayManager.manager.playerTransform.position.z > myTransform.position.z + 1)
        {
            GamePlayManager.manager.propellers.Add(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        euler.x += direction * speed * Time.deltaTime;
        euler.x %= 360F;

        rBody.rotation = Quaternion.Euler(euler);
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
