using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//includes agent specific stats. Is assigned to each of the agent prefabs. 

public class agentBrain : MonoBehaviour
{
    public float agentEngergy = 7;
    
    public int foodCount = 0;
    public int senseRadius = 50; 
    public bool seesFood;
    private int wanderDistance = 8;     
    private float agentFull = 2;

    [SerializeField]
    public Vector3 destination ;

    public bool hasDestination;
    public bool returnedToWall = false;

    NavMeshAgent _hungryAgent;
    

    void Start()
    {
        _hungryAgent = GetComponent<NavMeshAgent>();
        destination = new Vector3(0, 0, 5);
        hasDestination = false;
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
        Collider[] Colliders = Physics.OverlapSphere(new Vector3(_hungryAgent.transform.position.x, 0, _hungryAgent.transform.position.z), senseRadius);
            foreach (var hitCollider in Colliders)
                {
                    Debug.Log("collider +1");
                    if (hitCollider.gameObject.CompareTag("food"))
                    {
                        Debug.Log("I see food");
                        var foodTransform = hitCollider.gameObject.transform;
                        destination = foodTransform.position;
                        seesFood = true;
                        return;
                    }
                }
            //destination = new Vector3(0, 999, 0);
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
        if (_hungryAgent.transform.position.x == destination.x && _hungryAgent.transform.position.z == destination.z && returnedToWall == false) 
        {
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
            foodSearch();
            if (destination.y == 999) 
            {
                wander();
            }
            _hungryAgent.SetDestination(destination);
            hasDestination = false;
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

