using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Material[] materials;

    private ControllerDetection controller;

    private List<Transform> enemies;
    public void AddEnemy(Transform t) => enemies.Add(t);
    public void DeleteEnemy(Transform t) => enemies.Remove(t);
    public IReadOnlyCollection<Transform> GetEnemies() => enemies.AsReadOnly();

    private void Start()
    {
        enemies = new List<Transform>();
        controller = GameObject.Find("IntroController").GetComponent<ControllerDetection>();
        var users = controller.GetUsers();
        GameObject.Find("RpcManager")?.GetComponent<RpcManager>().StartGame(users.Count);
        Transform[] humans = new Transform[users.Count];
        int i = 0;
        foreach (var user in users)
        {
            GameObject go = SpawnPlayer(i);
            go.AddComponent<PlayerHuman>().SetUser(controller.GetUsers()[i]);
            go.GetComponent<NavMeshAgent>().enabled = false;
            go.tag = "PlayerHuman";
            go.name = "Player " + i + " - Human";
            humans[i] = go.transform;
            i++;
        }
        for (; i < 4; i++)
        {
            GameObject go = SpawnPlayer(i);
            go.AddComponent<PlayerAI>().Init(humans);
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.tag = "PlayerAI";
            go.name = "Player " + i + " - AI";
        }
    }

    private GameObject SpawnPlayer(int index)
    {
        GameObject go = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.identity);
        go.GetComponent<Renderer>().material = materials[index];
        go.layer = 11 + index; // First "Player" layer
        return go;
    }
}
