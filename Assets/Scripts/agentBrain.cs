using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//includes agent specific stats. Is assigned to each of the agent prefabs. 

public class agentBrain : MonoBehaviour
{
    public float agentEngergy = 7;
    
    public int foodCount = 0;
    public int senseRadius; 
    private int wanderDistance = 8;     
    private float agentFull = 2;

    [SerializeField]
    public Vector3 destination;

    private bool hasDestination;
    public bool returnedToWall = false;

    NavMeshAgent _hungryAgent;
    

    void Start()
    {
        _hungryAgent = GetComponent<NavMeshAgent>();
    }



    //generates a new destination for the hungry agent to travel to 

    private void wander() 
    {
        float angleOfMovement = Random.Range(0f, Mathf.PI * 2);

        float destinationX = Mathf.Cos(angleOfMovement);
        float destinationZ = Mathf.Sin(angleOfMovement);

        destination = new Vector3(transform.position.x + (destinationX * wanderDistance), 0, transform.position.z + (destinationZ * wanderDistance));

        if (destination.x > 25) {
                destination.x = 25;
            } else if (destination.x < -25) {
                destination.x = -25;
            }

            if (destination.z > 25)
            {
                destination.z = 25;
            } else if (destination.z < -25) {
                destination.z = -25;
            }
        
    
 
    }
    


    private void foodSearch() 
    {
        Collider[] Colliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), senseRadius);
            foreach (var hitCollider in Colliders)
                {
                    if (hitCollider.gameObject.CompareTag("food"))
                    {
                        var foodTransform = hitCollider.gameObject.transform;
                        destination = foodTransform.position;
                        hasDestination = true;
                    }
                }
            destination = new Vector3(0, 50, 0);
    }


    /*
    private void returnHome() 
    {
        if 
    }
   */

    



    void Update() 
    {
        /*
         if ( agentposition == destiation) 
            {
        hasDestination = false;
        }

         */
        if (hasDestination == false)
        {
            /*
                if (foodCount == agentFull) 
                {
                    returnhome();
                    //destination = *Closest wall*
                    returnedToWall = true;
                }
            */
            foodSearch();
            if (destination.y == 50) 
            {
                wander();
                hasDestination = true;
            }

            _hungryAgent.SetDestination(destination);

        }
    
    }


    
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("food"))
        {
            foodCount += 1;
            Destroy(other);
        }

    }
    
}

