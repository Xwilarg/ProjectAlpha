using UnityEngine;

public static class AIUtilities
{
    public static Transform GetClosestTransform(Vector3 me, Transform[] others, out float closestDist)
    {
        Transform closest = null;
        closestDist = float.MaxValue;
        foreach (Transform t in others)
        {
            float dist = Vector3.Distance(me, t.position);
            if (closest == null || dist < closestDist)
            {
                closest = t;
                closestDist = dist;
            }
        }
        return closest;
    }
}
