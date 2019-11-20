using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time before 2 spawn")]
    private float spawnTimerRef;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Spawnable[] toSpawn;

    private float spawnTimer;
    private int totalChance;
    private PlayerManager pm;
    private Transform[] humans;

    [Serializable]
    private struct Spawnable
    {
        [Tooltip("Chance to be spawned")]
        public int chance;
        [Tooltip("GameObject to be spawned")]
        public GameObject toSpawn;
    }

    private void Start()
    {
        List<Transform> t = GameObject.FindGameObjectsWithTag("PlayerHuman").Select(x => x.transform).ToList();
        t.AddRange(GameObject.FindGameObjectsWithTag("PlayerAI").Select(x => x.transform));
        humans = t.ToArray();
        spawnTimer = spawnTimerRef;
        totalChance = toSpawn.Sum(x => x.chance);
        pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0f)
            foreach (Transform t in spawnPoints)
                Spawn(t.position);
    }

    private void Spawn(Vector3 pos)
    {
        int nb = UnityEngine.Random.Range(0, totalChance);
        GameObject go = null;
        for (int i = 0; i < toSpawn.Length; i++)
        {
            var elem = toSpawn[i];
            nb -= elem.chance;
            if (nb <= 0)
            {
                go = elem.toSpawn;
                break;
            }
        }
        GameObject spawned = Instantiate(go, pos, Quaternion.identity);
        spawned.GetComponent<EnemyAI>().Humans = humans;
        pm.AddEnemy(spawned.transform);
        spawnTimer = spawnTimerRef;
    }

    /*private IEnumerator SpawnWave()
    {
        yield return new 
    }*/
}
