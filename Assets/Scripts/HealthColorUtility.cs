using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class HealthColorUtility
{
    public enum HealthStatus
    {
        HIGH = 0,
        MEDIUM = 1,
        DEAD = 2,
    }

    static private Color MAX_HEALTH = Color.green;
    static private Color MID_HEALTH = Color.yellow;
    static private Color NO_HEALTH = Color.red;

    const float HIGH_HEALTH_BARRIER = 70f;

    static public Color GetHealthColor(float health)
    {
        const float firstBarrier = HIGH_HEALTH_BARRIER;
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

    static public HealthStatus GetHealthStatus(float health)
    {
        if(health >= HIGH_HEALTH_BARRIER)
        {
            return HealthStatus.HIGH;
        } else if(health > 0f)
        {
            return HealthStatus.MEDIUM;
        } else
        {
            return HealthStatus.DEAD;
        }
    }
}
