using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatEnemyAnimation : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    public CatEnemy cat;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cat = GetComponent<CatEnemy>();
    }

    void Update()
    {
        anim.SetBool("spin", cat.rotating);
        anim.SetBool("run",cat.runAnimation);
        anim.SetBool("Walk",cat.walkAnimation);


        /*anim.SetBool("spin", cat.rotating);
        anim.SetBool("spin", !cat.rotating&&!agent.hasPath&&!cat.canSeePlayer);*/
        //anim.SetBool("run", !cat.canSeePlayer);
        //anim.SetBool("run", !cat.canSeePlayer);


        /*if (agent.hasPath&&cat.canSeePlayer&&!cat.rotating)
        {
            anim.SetBool("run", cat.canSeePlayer);
        }
        if (agent.hasPath && !cat.canSeePlayer && !cat.rotating)
        {
            anim.SetBool("run", !cat.canSeePlayer);

        }*/
    }
}
