using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyThrowerAnimationController : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    EnemyThrower enemy;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyThrower>();
    }

    void Update()
    {


        if (agent.velocity != Vector3.zero )
            anim.SetFloat("Speed", agent.velocity.magnitude);
        else
            anim.SetFloat("Speed", 0);

        if (enemy.currentBullets<=0)
        {
            anim.SetBool("Run", enemy.canSeePlayer);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        




    }
}
