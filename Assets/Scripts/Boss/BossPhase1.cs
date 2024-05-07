using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPhase1 : IBossPhase
{
    private Boss boss;
    private NavMeshAgent agent;
    private int index;
    public BossPhase1(Boss boss, NavMeshAgent agent,int index)
    {
        this.boss = boss;
        this.agent = agent;
        this.index = index;
    }

    // Update is called once per frame
    public void Execute()
    {
        if (!boss.fov.canSeePlayer)
        {
            agent.SetDestination(boss.phase1points[index - 1].position);
            if (Vector3.Distance(boss.transform.position, boss.phase1points[index - 1].position) < 0.2f)
            {
                boss.LookAround();
            }
        }
        else
        {
            boss.RotateToPlayer();
        }
        
    }
}
