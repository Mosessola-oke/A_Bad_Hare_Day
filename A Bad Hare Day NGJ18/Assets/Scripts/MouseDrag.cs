using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
	public float fl_distance = 10;
	private Vector3 velocity;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnMouseDrag()
	{
		Vector3 mouseposition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, fl_distance);
		Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mouseposition);

		transform.position = objectPosition;
	}
}
