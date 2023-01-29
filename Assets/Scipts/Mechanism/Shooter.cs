using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float timeGap = 1;
    [SerializeField] GameObject projectile;

    Queue<GameObject> projectileQueue = new Queue<GameObject>();
    List<GameObject> activeProjectiles = new List<GameObject>();

    int centerPos = 0;
    Vector3 shootPos = Vector3.forward;
    float timeMesure = 0;

    Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateProjectileList());
        myTransform = transform;

        GameplayManager.manager.OnGameState += OnGameState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameplayManager.manager.GetGameState() == GameState.Ready || GameplayManager.manager.GetGameState() == GameState.Dead)
            return;

        timeMesure += Time.fixedDeltaTime;

        if ((myTransform.position - GameplayManager.manager.playerTransform.position).magnitude <= 20)
            return;

        if(timeMesure > timeGap)
        {
            timeMesure %= timeGap;
            centerPos = Random.Range(-1, 2) * 2;
            shootPos.x = centerPos;

            if (projectileQueue.Count < 1)
                return;

            GameObject projObj = projectileQueue.Dequeue();
            activeProjectiles.Add(projObj);
            projObj.SetActive(true);
            projObj.GetComponent<Rigidbody>().position = shootPos + myTransform.position;
            projObj.GetComponent<Rigidbody>().rotation = Quaternion.identity;
        }
    }

    void OnGameState(GameState state)
    {
        if(state == GameState.Dead)
        {
            foreach(GameObject obj in activeProjectiles)
            {
                obj.SetActive(false);
                projectileQueue.Enqueue(obj);
            }
            activeProjectiles.Clear();
        }
    }

    IEnumerator CreateProjectileList()
    {
        projectileQueue = new Queue<GameObject>();

        while(projectileQueue.Count < 10)
        {
            GameObject gmObj = Instantiate(projectile);
            gmObj.SetActive(false);
            projectileQueue.Enqueue(Instantiate(gmObj));
            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            other.transform.position = shootPos + myTransform.position;
            projectileQueue.Enqueue(other.gameObject);
            activeProjectiles.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
