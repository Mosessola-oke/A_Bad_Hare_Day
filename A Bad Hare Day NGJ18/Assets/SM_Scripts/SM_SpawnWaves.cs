using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_SpawnWaves : MonoBehaviour 
{
    public enum SpawnState { SPAWNING, WATING, COUNTING };

    [System.Serializable] //make editing inside of unity possible
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    //private int enemyCounter;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;

    [SerializeField] private float fl_searchCountdown = 1f;

    [SerializeField] private SpawnState state = SpawnState.COUNTING;
	// Use this for initialization
	void Start () 
    {
        waveCountdown = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state == SpawnState.WATING) //if in waiting state
        {
            if (!bl_enemyisAlive()) //if there are no enemies alive
            {
                Debug.Log("WaveCompleted");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave ( waves[nextWave] ) ); //Start spawning wave
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
	}
    
    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete!"); 
        }
        else
        {
            nextWave++;
        }
    }
    
    bool bl_enemyisAlive()
    {
        fl_searchCountdown -= Time.deltaTime;
        if (fl_searchCountdown <= 0f)
        {
            fl_searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemies(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate); //Spawn an enemy after a specified amount of time
        }

        state = SpawnState.WATING;

        yield break;
    }

    void SpawnEnemies(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
