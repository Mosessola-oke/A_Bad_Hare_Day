using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyAI : MonoBehaviour
{
	public GameObject currentTarget;
	public GameObject[] targets;

	NavMeshAgent agent;
	// Use this for initialization

	private void Awake()
	{
		targets = GameObject.FindGameObjectsWithTag("NavTargets");
		int index = Random.Range(0, targets.Length);
		currentTarget = targets[index];
	}
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		
	}

	// Update is called once per frame
	void Update()
	{
		agent.SetDestination(currentTarget.transform.position);
		ChooseDifferentTarget();
	}

	void ChooseDifferentTarget()
	{
		if (transform.position == currentTarget.transform.position)
		{
			int index = Random.Range(0, targets.Length);
			currentTarget = targets[index];

		}
	}
}
