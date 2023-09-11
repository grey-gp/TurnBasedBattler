using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class  TargetData {
    public string targetName;
    public int maxHealth;
    public int currentHealth;
    public float hitChance;
    public DamageType resistance;
    public DamageType weakness;
}

public class TargetComponent : MonoBehaviour
{
    public TargetData[] initialTargets;
    public TargetData SelectedTarget { get; private set; } 
    
    private int targetIndex = 0;
    private TargetData[] targets;

    private void Start() 
    {
        for (int i = 0; i < initialTargets.Length; ++i)
        {
            initialTargets[i].currentHealth = initialTargets[i].maxHealth;
        }
    }

    public void AttackTarget(int damage)
    {
        SelectedTarget.currentHealth -= damage;
        targets[targetIndex] = SelectedTarget;
        this.GetActiveTargets();
        if (SelectedTarget.currentHealth <= 0) 
        {
            targetIndex = 0;
            this.SelectedTarget = this.targets[targetIndex];
        }
    }

    public void StartBattle()
    {
        this.GetActiveTargets();
        SelectedTarget = this.targets[this.targetIndex];
    }

    public void GetActiveTargets()
    {
        TargetData[] activeTargets =  initialTargets.Where(target => target.currentHealth > 0).ToArray();
        if (activeTargets.Length == 0)
        {
            Destroy(this.gameObject);
            return;
        } 
        targets =  activeTargets;
    }

    public void SelectTarget()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            targetIndex = ++ targetIndex % targets.Length;
            SelectedTarget = targets[targetIndex];
            Debug.Log($"Currently selected target {SelectedTarget.targetName}");
        }
    }
}
