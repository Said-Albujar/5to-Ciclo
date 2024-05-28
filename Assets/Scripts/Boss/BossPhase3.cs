using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossPhase3 : IBossPhase
{
    private Boss boss;
    private NavMeshAgent agent;
    private int index;
    public BossPhase3(Boss boss, NavMeshAgent agent, int index)
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
            agent.SetDestination(boss.phase3points[index - 1].position);
            if (Vector3.Distance(boss.transform.position, boss.phase3points[index - 1].position) < 0.2f)
            {
                boss.RotateBodyToDesk(180);
                boss.isAttacking = true;
                boss.Attack(index);
            }
            else
            {
                boss.DesactiveArms();
                boss.isAttacking = false;
            }

            if (!boss.fov.canSeePlayer)
            {
                boss.StopVfxPlayer();
                boss.timer = 0;
                if (Vector3.Distance(boss.transform.position, boss.phase3points[index - 1].position) < 0.2f)
                {
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
