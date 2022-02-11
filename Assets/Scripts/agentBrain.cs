using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


//includes agent specific stats. Is assigned to each of the agent prefabs. 

public class agentBrain : MonoBehaviour
{
    public float agentEngergy = 7;
    
    public int foodCount = 0;
    public int senseRadius = 100; 
    private int wanderDistance = 8;     
    private float agentFull = 2;
    public bool seesFood;

    [SerializeField]
    public Vector3 destination;

    public bool hasDestination;
    public bool returnedToWall = false;

    NavMeshAgent _hungryAgent;
    

    void Start()
    {
        _hungryAgent = GetComponent<NavMeshAgent>();
        hasDestination = false;
        returnedToWall = false;
    }



    //generates a new destination for the hungry agent to travel to 

    void wander() 
    {
        float angleOfMovement = Random.Range(0f, Mathf.PI * 2);

        float destinationX = Mathf.Cos(angleOfMovement);
        float destinationZ = Mathf.Sin(angleOfMovement);

        destination = new Vector3(transform.position.x + (destinationX * wanderDistance), 0, transform.position.z + (destinationZ * wanderDistance));

        if (destination.x > 25) {
                destination.x = 24;
            } else if (destination.x < -25) {
                destination.x = -24;
            }

            if (destination.z > 25)
            {
                destination.z = 24;
            } else if (destination.z < -25) {
                destination.z = -24;
            }
    }
    


    void foodSearch() 
    {
        Collider[] Colliders = Physics.OverlapSphere(new Vector3(0,0,0), 50);
            Colliders = Colliders.OrderBy(
                x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToArray();
            
            foreach (var hitCollider in Colliders)
                {
                    if (hitCollider.CompareTag("food"))
                    {
                        Debug.Log("I see food" + hitCollider.transform.position);
                        var foodTransform = hitCollider.gameObject.transform;
                        destination = foodTransform.position;
                        seesFood = true;
                        return;
                    }else {
                        seesFood = false;
                    }
                }
    }






    void returnHome() 
    {
        UnityEngine.AI.NavMeshHit edge;
        if (UnityEngine.AI.NavMesh.FindClosestEdge(transform.position, out edge, UnityEngine.AI.NavMesh.AllAreas))
        {
            destination =  edge.position;
        }

    } 



    void Update() 
    {
        //If the agent has reached it's destination and it has not returned to the wall find a new destination
        if ((_hungryAgent.transform.position.x == destination.x) && (_hungryAgent.transform.position.z == destination.z) && (returnedToWall == false)) 
        {
            Debug.Log("reached location"); 
            
            hasDestination = false;
        }
        
        if (hasDestination == false) 
        {
            /*
            if (foodCount == agentFull) 
            {
                returnhome();
                returnedToWall = true;
            }
            */
            //need to implement a function that stops agents from going to a locaion if the food is alread eaten
           foodSearch();
            if (seesFood == false) 
            {
                wander();
            }
            _hungryAgent.SetDestination(destination);
            hasDestination = true;
        }

        
        
            
        
    
    }


    
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("food"))
        {
            foodCount += 1;
            Destroy(other.gameObject);
        }

    }
    
}

