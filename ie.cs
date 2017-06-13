using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ie : MonoBehaviour {
	public float deathDistatnce ;
	public float distanceAway;
	public Transform thisObject;
	public Transform target;
    private NavMeshAgent agent;



    void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
        agent = GetComponent<NavMeshAgent> ();



	}

    void Update()
    {
        float dist = Vector3.Distance(target.position,transform.position);
       
        if (target)
        {
            agent.SetDestination(target.position);
        }
        if ( dist <= deathDistatnce)
        {
            Debug.Log("kkkk");
        }
       
    }
	
}
