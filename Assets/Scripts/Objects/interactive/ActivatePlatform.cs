using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    public GameObject objectToMove;
    public float speed = 2.0f;        
    public float distanceX = 5.0f;     
    public float distanceZ = 5.0f;      
    public bool moveForwardAndBack = true; 
    public bool moveLeftAndRight = false;  

    private Vector3 initialPoint;       
    private Vector3 endPointX;          
    private Vector3 endPointZ;          
    private Vector3 targetPoint;
    

    void Start()
    {
        initialPoint = objectToMove.transform.position;
        endPointX = objectToMove.transform.position + Vector3.right * distanceX;
        endPointZ = objectToMove.transform.position + Vector3.forward * distanceZ;
        targetPoint = endPointX;
    }

    void Update()
    {
        if (moveForwardAndBack)
        {
            
            Vector3 directionX = (targetPoint - objectToMove.transform.position).normalized;

            
            Vector3 newPositionX = objectToMove.transform.position + directionX * speed * Time.deltaTime;

            
            if (Vector3.Distance(objectToMove.transform.position, targetPoint) <= 0.01f)
            {
                
                targetPoint = (targetPoint == endPointX) ? initialPoint : endPointX;
            }


            objectToMove.transform.position = newPositionX;
        }

        if (moveLeftAndRight)
        {
            
            Vector3 directionZ = (endPointZ - objectToMove.transform.position).normalized;

            
            Vector3 newPositionZ = objectToMove.transform.position + directionZ * speed * Time.deltaTime;

           
            if (Vector3.Distance(objectToMove.transform.position, endPointZ) <= 0.01f)
            {
               
                endPointZ = (endPointZ == (initialPoint + Vector3.forward * distanceZ)) ?
                    (initialPoint + Vector3.back * distanceZ) :
                    (initialPoint + Vector3.forward * distanceZ);
            }


            objectToMove.transform.position = newPositionZ;
        }
    }
}
