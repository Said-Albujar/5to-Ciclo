using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinerAnimationController : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    MinerEnemy miner;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        miner = GetComponent<MinerEnemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath && !miner.canSeePlayer)
            anim.SetFloat("speed", agent.velocity.magnitude);
        else
            anim.SetFloat("speed", 0);

        anim.SetBool("run", miner.canSeePlayer);

    }
}
