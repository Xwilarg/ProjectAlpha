using UnityEngine;

public class DebugInitEnemies : MonoBehaviour
{
    public void InitEnemies(Transform[] humans)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<EnemyAI>().Humans = humans;
        }
    }
}
