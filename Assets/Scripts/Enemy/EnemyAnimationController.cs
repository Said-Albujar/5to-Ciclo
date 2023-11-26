using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    public Enemy enemy;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (agent.hasPath && !enemy.canSeePlayer)
            anim.SetFloat("Speed", agent.velocity.magnitude);
        else
            anim.SetFloat("Speed", 0);

        anim.SetBool("Run", enemy.canSeePlayer);


    }
}
