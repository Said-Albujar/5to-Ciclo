using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Execute()
    {
        if (boss.playerInGround)
        {
            boss.ChasePlayer();
        }
        else
        {
            if (!boss.fov.canSeePlayer)
            {
                boss.StopVfxPlayer();
                boss.timer = 0;
                agent.SetDestination(boss.phase1points[index - 1].position);
                if (Vector3.Distance(boss.transform.position, boss.phase1points[index - 1].position) < 0.2f)
                {
                    boss.RotateBodyToDesk(0);
                    boss.LookAround();
                }
            }
            else
            {
                boss.KillPlayer();
                boss.RotateHeadToPlayer();
            }
        }
        
        
    }
}
