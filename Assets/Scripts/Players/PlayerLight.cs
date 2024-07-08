using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [Header("FOV")]
    public bool canSeeEnemy;
    public float radius;
    public float angle;
    public LayerMask targeMask;
    public LayerMask obstructionMask;
    public float time;
    public Collider[] rangeChecks;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }


    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            FieldOfViewCheck();
        }
    }
    private void OnEnable()
    {
        StartCoroutine(FOVRoutine());

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void FieldOfViewCheck()
    {

        rangeChecks = Physics.OverlapSphere(transform.position, radius, targeMask);
        foreach (Collider coll in rangeChecks)
        {
            Transform target = coll.transform;
            MinerEnemy enemy = target.GetComponent<MinerEnemy>();

            Vector3 directionTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionTarget, distanceToTarget, obstructionMask))
                {
                    enemy.timerStop = 0f;
                    enemy.detecteEnemyLight = true;


                }

            }
            
        }
        if (canSeeEnemy)
            canSeeEnemy = false;
    }

}
