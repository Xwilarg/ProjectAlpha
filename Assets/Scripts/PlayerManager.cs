using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;

    private ControllerDetection controller;

    private void Start()
    {
        controller = GameObject.Find("IntroController").GetComponent<ControllerDetection>();
    }
}
