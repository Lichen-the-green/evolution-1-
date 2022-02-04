using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabSpawner : MonoBehaviour
{
    [SerializeField, Range(10, 200)]
    int foodAmount = 100;
    
    public int agentAmount = 20;
    public GameObject foodPrefab;
    public GameObject hungryAgentPrefab; 

    void Start()
    {
        for (var i = 0; i < foodAmount; i++) 
        {
            Vector3 foodPosition = new Vector3(Random.Range(-23f,23f), 0.5f, Random.Range(-23f,23f));
            GameObject food = Instantiate(foodPrefab, foodPosition, Quaternion.identity) as GameObject;
            food.transform.parent = GameObject.Find("prefabSpawner").transform;
        }

        for (var i = 0; i < agentAmount; i++) 
        {
           int sideChoice = Random.Range(0,4);

            if (sideChoice == 0) {
                Instantiate(hungryAgentPrefab, new Vector3(Random.Range(-24f,24f), 1f, 24f), Quaternion.identity);
            }  
            else if (sideChoice == 1) {
                Instantiate(hungryAgentPrefab, new Vector3(Random.Range(-25f,25f), 1f, -25f), Quaternion.identity);
            } 
            else if (sideChoice == 2) {
                Instantiate(hungryAgentPrefab, new Vector3(25f ,1f, Random.Range(-25f,25f)), Quaternion.identity);
            }
            else if (sideChoice == 3) {
                Instantiate(hungryAgentPrefab, new Vector3(-25f ,1f, Random.Range(-25f,25f)), Quaternion.identity);
            } 

         }


    }


}






