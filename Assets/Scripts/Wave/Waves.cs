using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private GameObject debugEnemy;
    [SerializeField]
    private GameObject debugShield;

    private Wave[] waves;

    private void Start()
    {
        waves = new Wave[]
        {
            new Wave() {
                leftSpawn = CombineArray(Repeat(debugEnemy, 10), Repeat(debugShield, 3)),
                rightSpawn = CombineArray(Repeat(debugEnemy, 10), Repeat(debugShield, 3))
            },
            new Wave() {
                leftSpawn = CombineArray(Repeat(debugEnemy, 20), Repeat(debugShield, 5)),
                rightSpawn = CombineArray(Repeat(debugEnemy, 20), Repeat(debugShield, 5))
            },
            new Wave() {
                leftSpawn = CombineArray(Repeat(debugEnemy, 30), Repeat(debugShield, 10)),
                rightSpawn = CombineArray(Repeat(debugEnemy, 30), Repeat(debugShield, 10))
            }
        };
    }

    private List<GameObject> Repeat(GameObject go, int nb)
        => Enumerable.Repeat(go, nb).ToList();

    private GameObject[] CombineArray(params List<GameObject>[] arrays)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (List<GameObject> arr in arrays)
            gos.AddRange(arr);
        return gos.ToArray();
    }
}
