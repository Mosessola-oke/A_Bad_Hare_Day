using UnityEngine;
using System.Collections;

public class SM_SpawnEnemies : MonoBehaviour
{
	public GameObject go_Bunnies;
	public int in_NumberofBunnies;
	[Range(0, 150)] public int in_MaxBunnySpawn;
	public float fl_SpawnTime = 0.5f;
	public Transform[] SpawnPoints;

	public bool bl_infinite;
	// Use this for initialization
	void Start()
	{
		StartCoroutine("VariableSpawn");
		//InvokeRepeating("SpawnEnemy", fl_SpawnTime, fl_SpawnTime);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		VariableSpawnRate();
	}

	public void SpawnEnemy()
	{
		if (in_NumberofBunnies == in_MaxBunnySpawn && !bl_infinite)
		{
			return;
		}
		int SpawnPointIndex = Random.Range(0, SpawnPoints.Length);

		Instantiate(go_Bunnies, SpawnPoints[SpawnPointIndex].position, SpawnPoints[SpawnPointIndex].rotation);
		in_NumberofBunnies++;
	}

	IEnumerator VariableSpawn()
	{
		while (in_NumberofBunnies <= in_MaxBunnySpawn)
		{
			yield return new WaitForSeconds(fl_SpawnTime);
			SpawnEnemy();
		}
	}

	void VariableSpawnRate()
	{
		if (in_NumberofBunnies >= 0 && in_NumberofBunnies <= 20)
		{
			fl_SpawnTime = 0.5f;
		}
		else if (in_NumberofBunnies >= 20 && in_NumberofBunnies <= 50)
		{
			fl_SpawnTime = 1.7f;
		}
		else if (in_NumberofBunnies >= 50 && in_NumberofBunnies <= 80)
		{
			fl_SpawnTime = 2.9f;
		}
		else if (in_NumberofBunnies >= 80 && in_NumberofBunnies <= 100)
		{
			fl_SpawnTime = 4.1f;
		}
		else if (in_NumberofBunnies >= 100 && in_NumberofBunnies <= 130)
		{
			fl_SpawnTime = 5.3f;
		}
		else if (in_NumberofBunnies >= 130 && in_NumberofBunnies <= 150)
		{
			fl_SpawnTime = 6.5f;
		}
	}
}
