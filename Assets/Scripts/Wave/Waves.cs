using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public Wave[] GetWaves()
        => waves;

    private void Start()
    {
        waves = new Wave[]
        {
            new Wave() {
                leftSpawn = CombineLists(Repeat(debugShield, 3), Repeat(debugEnemy, 10)),
                rightSpawn = CombineLists(Repeat(debugShield, 3), Repeat(debugEnemy, 10))
            },
            new Wave() {
                leftSpawn = CombineLists(Repeat(debugShield, 5), Repeat(debugEnemy, 20)),
                rightSpawn = CombineLists(Repeat(debugShield, 5), Repeat(debugEnemy, 20))
            },
            new Wave() {
                leftSpawn = CombineLists(Repeat(debugShield, 10), Repeat(debugEnemy, 30)),
                rightSpawn = CombineLists(Repeat(debugShield, 10), Repeat(debugEnemy, 30))
            }
        };
    }

    private List<GameObject> Repeat(GameObject go, int nb)
        => Enumerable.Repeat(go, nb).ToList();

    private ReadOnlyCollection<GameObject> CombineLists(params List<GameObject>[] arrays)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (List<GameObject> arr in arrays)
            gos.AddRange(arr);
        return gos.AsReadOnly();
    }
}
