using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time before 2 spawn")]
    private float spawnTimerRef;

    [SerializeField]
    private Spawnable[] toSpawn;

    private float spawnTimer;
    private int totalChance;

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
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0f)
            Spawn();
    }

    private void Spawn()
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
        GameObject spawned = Instantiate(go, transform.position, Quaternion.identity);
        spawned.GetComponent<EnemyAI>().Humans = humans;
        spawnTimer = spawnTimerRef;
    }
}
