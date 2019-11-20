using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time before 2 waves")]
    private float spawnTimerRef;

    [SerializeField]
    private Transform[] leftSpawnPoints, rightSpawnPoints;

    [SerializeField]
    private Text waveText;
    private Fade waveFade;

    private float spawnTimer;
    private PlayerManager pm;
    private Transform[] humans;
    private Waves waves;
    private int waveNb;

    private void Start()
    {
        List<Transform> t = GameObject.FindGameObjectsWithTag("PlayerHuman").Select(x => x.transform).ToList();
        t.AddRange(GameObject.FindGameObjectsWithTag("PlayerAI").Select(x => x.transform));
        humans = t.ToArray();
        spawnTimer = 2f;
        waveNb = 0;
        waves = GetComponent<Waves>();
        waveFade = waveText.GetComponent<Fade>();
        pm = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0f)
        {
            StartCoroutine("SpawnWave");
            spawnTimer = spawnTimerRef;
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave currWave = waves.GetWaves()[waveNb];
        waveNb++;
        waveText.text = "Wave " + waveNb;
        waveFade.Restore();
        List<GameObject> leftWave = new List<GameObject>(currWave.leftSpawn);
        List<GameObject> rightWave = new List<GameObject>(currWave.rightSpawn);
        while (leftWave.Count > 0 || rightWave.Count > 0)
        {
            foreach (var spawn in leftSpawnPoints)
            {
                if (leftWave.Count == 0)
                    break;
                SpawnEnemy(leftWave[0], spawn.position);
                leftWave.RemoveAt(0);
            }
            foreach (var spawn in rightSpawnPoints)
            {
                if (rightWave.Count == 0)
                    break;
                SpawnEnemy(rightWave[0], spawn.position);
                rightWave.RemoveAt(0);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private void SpawnEnemy(GameObject go, Vector3 pos)
    {
        GameObject spawned = Instantiate(go, pos, Quaternion.identity);
        spawned.GetComponent<EnemyAI>().Humans = humans;
        pm.AddEnemy(spawned.transform);
    }
}
