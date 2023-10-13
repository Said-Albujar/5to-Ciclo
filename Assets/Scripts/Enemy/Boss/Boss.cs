using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int rutine;
    public float timer;
    public Animator animator;
    public Quaternion angle;
    public float grade;
    public int moveSpeed = 1;

    public GameObject target;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("PlayerGroup");
    }

    void Update()
    {
        Boss_Rutine();
    }

    public void Boss_Rutine()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > 20)
        {
            animator.SetBool("run", false);
            timer += 1 * Time.deltaTime;
            if (timer >= 4)
            {
                rutine = Random.Range(0, 2);
                timer = 0;
            }

            switch (rutine)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;

                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    rutine++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position = transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            animator.SetBool("walk", false);

            animator.SetBool("run", true);
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
    }
}
