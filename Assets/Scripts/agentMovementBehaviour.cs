using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    public Vector3 destination;
    
    NavMeshAgent _hungryAgent;


    // Start is called before the first frame update
    void Start()
    {
        _hungryAgent = GetComponent<NavMeshAgent>();
        destination = new Vector3(0f, 0f, 0f);
        randoWanderPlace();
    }

    void randoWanderPlace()
    {
        if (destination != null)
        {
            _hungryAgent.SetDestination(destination);
        } 
        else
        {
            Debug.Log("destination is null");
        }
    }



  
}
