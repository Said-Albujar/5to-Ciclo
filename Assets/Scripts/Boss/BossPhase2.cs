using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPhase2 : IBossPhase
{
    private Boss boss;
    private NavMeshAgent agent;
    private int index;
    public BossPhase2(Boss boss, NavMeshAgent agent, int index)
    {
        this.boss = boss;
        this.agent = agent;
        this.index = index;
    }
    public void Execute()
    {
        boss.isAttacking = false;
        boss.DesactiveArms();
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
                agent.SetDestination(boss.phase2points[index - 1].position);
                if (Vector3.Distance(boss.transform.position, boss.phase2points[index - 1].position) < 0.2f)
                {
                    boss.RotateBodyToDesk(90);
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
