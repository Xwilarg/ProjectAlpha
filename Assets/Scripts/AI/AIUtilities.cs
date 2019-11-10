using System.Collections.Generic;
using UnityEngine;

public static class AIUtilities
{
    public static Transform GetClosestTransform(Vector3 me, IEnumerable<Transform> others, out float closestDist)
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

    public static Transform GetClosestTransformInSight(Vector3 me, IEnumerable<Transform> others, out float closestDist, int layer)
    {
        Transform closest = null;
        closestDist = float.MaxValue;
        foreach (Transform t in others)
        {
            float dist = Vector3.Distance(me, t.position);
            if (closest == null || dist < closestDist)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(me, new Vector3(t.position.x - me.x, 0f, t.position.z - me.z), out hitInfo, float.MaxValue, layer) && hitInfo.collider.transform == t)
                {
                    closest = t;
                    closestDist = dist;
                }
            }
        }
        return closest;
    }
}
