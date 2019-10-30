using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Material[] materials;

    private ControllerDetection controller;
    private Rigidbody rb;

    private void Start()
    {
        controller = GameObject.Find("IntroController").GetComponent<ControllerDetection>();
        var users = controller.GetUsers();
        GameObject.Find("RpcManager")?.GetComponent<RpcManager>().StartGame(users.Count);
        int i = 0;
        foreach (var user in users)
        {
            GameObject go = Instantiate(playerPrefab, spawns[i].transform.position, Quaternion.identity);
            go.GetComponent<Renderer>().material = materials[i];
            go.GetComponent<PlayerController>().SetUser(controller.GetUsers()[i]);
            i++;
        }
        rb = GetComponent<Rigidbody>();
    }
}
