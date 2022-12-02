using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        Vector3 direction = collision.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0.5F;

        if (collision.attachedRigidbody.gameObject.tag == "Player")
        {
            collision.attachedRigidbody.AddForce(direction * 50, ForceMode.Impulse);

            Cube_Player player = collision.attachedRigidbody.GetComponent<Cube_Player>();
            player.SetState(GameState.Dead);
        }
    }
}
