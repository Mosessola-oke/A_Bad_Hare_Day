using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
	public float fl_timetoAccelerate = 2f;
	public float fl_maxSpeed = 5f;
	private Vector2 movement;
	private float fl_timeLeft;
	float fl_timer = 1;
	public Rigidbody2D rb;

	//Screen Wrap
	private Renderer[] renderers;
	private bool isWrappingX = false;
	private bool isWrappingY = false;
	// Use this for initialization
	void Start()
	{
		renderers = GetComponentsInChildren<Renderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		fl_timeLeft -= Time.deltaTime;
		if (fl_timeLeft <= 0)
		{
			movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, -1f));
			fl_timeLeft += fl_timer;
		}
	}

	private void FixedUpdate()
	{
		rb.AddForce(movement * fl_maxSpeed);
		ScreenWrap();
	}

	void ScreenWrap()
	{
		bool isVisible = CheckRenderers();
		if (isVisible)
		{
			isWrappingX = false;
			isWrappingY = false;
			return;
		}
		if (isWrappingX && isWrappingY)
		{
			return;
		}

		Vector3 newPosition = transform.position;
		if (newPosition.x > 1 || newPosition.x < 0)
		{
			newPosition.x = -newPosition.x;
			isWrappingX = true;
		}
		if (newPosition.y > 1 || newPosition.y < 0)
		{
			newPosition.y = -newPosition.y;
			isWrappingY = true;
		}

		transform.position = newPosition;
	}

	bool CheckRenderers()
	{
		foreach (Renderer  renderer in renderers)
		{
			if (renderer.isVisible)
			{
				return true;
			}
		}
		return false;
	}
}
