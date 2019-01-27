using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiger : MonoBehaviour, Entity, IEnemy
{
    private NavMeshAgent agent;
    [SerializeField] float Health = 100;
    [SerializeField] private float DamageDeal = 10f;
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
                    target.Damage(DamageDeal);
                }

                currCooldown = Cooldown;
            }
        }
    }

    public void Damage(float hit)
    {
        Health -= hit;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target.GetComponent<Player>();
    }
}
