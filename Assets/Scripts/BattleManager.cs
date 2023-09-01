using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    
    public bool bInBattle = false;

    public void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        } 
        else
        {
            Instance = this;
        }
    }


    public GameObject StartBattle(GameObject enemy, Transform spawnPoint)
    {
        bInBattle = true;
        return Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public void EndBattle()
    {
        bInBattle = false;
    }

}
