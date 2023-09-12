using System;
using UnityEngine;

[Serializable]
public class  TargetData {
    public string targetName;
    public int maxHealth;
    public int currentHealth;
    public float hitChance;
    public DamageType resistance;
    public DamageType weakness;


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

public class TargetComponent : MonoBehaviour
{
    public TargetData[] initialTargets;

    private void Start() 
    {
        for (int i = 0; i < initialTargets.Length; ++i)
        {
            initialTargets[i].currentHealth = initialTargets[i].maxHealth;
        }
    }
}
