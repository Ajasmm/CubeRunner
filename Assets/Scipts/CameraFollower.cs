using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetPos;


    Vector3 currentPos;

    Transform myTransform;

    private void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
            return;

        if (GameplayManager.manager.gamePlayer.GetState() == GameState.Dead)
            return;

        currentPos = target.position + offsetPos;
        myTransform.position = currentPos;
        myTransform.LookAt(target);
    }
}
