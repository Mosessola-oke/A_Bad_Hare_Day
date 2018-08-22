using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragRabbit : MonoBehaviour
{
	public BunnyAI aiBunny;
	private Rigidbody rb;
	public List<Vector3> listOfPositions = new List<Vector3>();
	private Vector3 currentPosition;
	private Collider groundCollider;
	public GameObject ground;
	public int captureFrames = 8;
	public float throwPower = 5;
	public float maxMagnitude = 10;


	public int in_usedflick = 0;
	public int in_usedDrop = 0;

	public float startingSpeed;
	NavMeshAgent rabbit;

	public Vector3 preUpdateMousePos;
	public Vector3 UpdateMousePos;
	float distanceCheck;

	private void Awake()
	{
		aiBunny = FindObjectOfType<BunnyAI>();
		rabbit = GetComponent<NavMeshAgent>();
		ground = GameObject.FindGameObjectWithTag("Ground");
	}

	void Start()
	{
		startingSpeed = rabbit.speed;
		rb = GetComponent<Rigidbody>();
		groundCollider = ground.GetComponent<Collider>();
	}

	void OnMouseDown()
	{
		readPosition();
		listOfPositions.Add(currentPosition);
		rb.velocity = rb.angularVelocity = Vector3.zero;
	}


	void OnMouseDrag()
	{
		readPosition();
		if (listOfPositions.Count >= captureFrames)
		{
			listOfPositions.RemoveAt(0);
			listOfPositions.TrimExcess();
		}
	}

	void OnMouseUp()
	{
		var oldPosition = listOfPositions[0];
		var forceMagnitude = (oldPosition - transform.position).magnitude;
		var forceDirection = (oldPosition - transform.position).normalized;
		float newMagnitude = Mathf.Min(forceMagnitude, maxMagnitude);
		rb.AddForce(forceDirection * -newMagnitude * throwPower, ForceMode.Impulse);
		listOfPositions.Clear();
	}

	void readPosition()
	{
		RaycastHit hit;
		if (groundCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200.0F))
		{
			currentPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			transform.position = currentPosition;
		}
		listOfPositions.Add(currentPosition);
	}
}
