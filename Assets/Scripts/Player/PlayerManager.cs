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
        rb = GetComponent<Rigidbody>();
        int i = 0;
        foreach (var user in controller.GetUsers())
        {
            GameObject go = Instantiate(playerPrefab, spawns[i].transform.position, Quaternion.identity);
            go.GetComponent<Renderer>().material = materials[i];
            go.GetComponent<PlayerController>().SetUser(controller.GetUsers()[i]);
            i++;
        }
    }
}
