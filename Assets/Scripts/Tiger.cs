using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiger : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float Damage = 10f;
    [SerializeField] private float Cooldown = 10f;

    private Player target;

    private float currCooldown;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            currCooldown -= Time.deltaTime;
            if (currCooldown <= 0)
            {
                if (agent.remainingDistance <= 1)
                {
                    target.Damage(Damage);
                }

                currCooldown = Cooldown;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
