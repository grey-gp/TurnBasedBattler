using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct TargetData {
    public string targetName;
    public int targetHealth;
    public float hitChance;
    public DamageType resistance;
    public DamageType weakness;
}

public class TargetComponent : MonoBehaviour
{
    public List<TargetData> initialTargets;
    
    public List<TargetData> GetActiveTargets()
    {
        return initialTargets.Where(target => target.targetHealth > 0).ToList();
    }
}
