using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Blocks : MonoBehaviour
{
    [SerializeField] float displacementDistance = 2;
    [SerializeField][Range(1, 10)] float speed = 1;

    Vector3 currentPos, initPos;
    [SerializeField] float timeFactor = 0, currentSpeed = 0;

    Transform myTransform;
    Rigidbody rBody;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        initPos = myTransform.position;
        currentPos = initPos;
        currentSpeed = speed / 10F;

        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeFactor += Time.fixedDeltaTime * currentSpeed;
        timeFactor %= 2F;
        currentPos.x = Mathf.Sin(timeFactor * Mathf.PI) * displacementDistance;
        currentPos.x += initPos.x;

        rBody.position = currentPos;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody == null)
            return;

        Vector3 direction = collision.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0.5F;

        if (collision.attachedRigidbody.gameObject.tag == "Player")
        {
            collision.attachedRigidbody.AddForce(direction * 100, ForceMode.Impulse);

            Cube_Player player = collision.attachedRigidbody.GetComponent<Cube_Player>();
            player.SetState(GameState.Dead);
        }
    }
}
