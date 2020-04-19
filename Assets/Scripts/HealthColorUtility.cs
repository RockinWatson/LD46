using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class HealthColorUtility
{
    static private Color MAX_HEALTH = Color.green;
    static private Color MID_HEALTH = Color.yellow;
    static private Color NO_HEALTH = Color.red;

    static public Color GetHealthColor(float health)
    {
        const float firstBarrier = 70f;
        if (health >= firstBarrier)
        {
            float t = (health - firstBarrier) / (100f - firstBarrier);
            return Color.Lerp(MID_HEALTH, MAX_HEALTH, t);
        }
        else
        {
            float t = (health - 0f) / firstBarrier;
            return Color.Lerp(NO_HEALTH, MID_HEALTH, t);
        }
    }
}
