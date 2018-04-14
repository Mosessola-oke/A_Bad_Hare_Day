using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private PositionQueue pastPositions;
	Vector3 velocity;
	void Awake()
	{
		pastPositions = new PositionQueue(10);
	}

	void Update()
	{
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 newPosition = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);

		transform.position = newPosition;

		if (Input.GetMouseButtonDown(0))
		{
			//transform.GetComponent<Rigidbody2D>().isKinematic=true;  
			pastPositions.Clear();
		}
		// User is dragging the object around
		if (Input.GetMouseButton(0))
		{
			pastPositions.Enqueue(newPosition);
			//transform.position = newPosition;                      
			//transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * 25f);
			transform.GetComponent<Rigidbody2D>().velocity = (newPosition - transform.position) * 10;
		}
		if (Input.GetMouseButtonUp(0))
		{
			//transform.GetComponent<Rigidbody2D>().isKinematic = false;   
			velocity = (newPosition - pastPositions.Peek()) / pastPositions.Count;
			transform.GetComponent<Rigidbody2D>().velocity = velocity * 20; // Also tried with transform.GetComponent<Rigidbody2D>().velocity = velocity; and transform.position += velocity; // The later works but doesn't allow for physics later on
		}
	}
}

class PositionQueue
{
	private Queue<Vector3> _queue;
	private int _maxSize;
	public int Count
	{
		get
		{
			return _queue.Count;
		}
	}
	public PositionQueue(int maxSize)
	{
		_maxSize = maxSize;
		_queue = new Queue<Vector3>();
	}
	public void Enqueue(Vector3 v)
	{
		if (_queue.Count >= _maxSize)
		{
			_queue.Dequeue();
		}
		_queue.Enqueue(v);
	}
	public Vector3 Peek()
	{
		return _queue.Peek();
	}
	public void Clear()
	{
		_queue.Clear();
	}
}
