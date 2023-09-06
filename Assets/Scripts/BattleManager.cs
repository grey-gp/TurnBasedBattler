using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public GameObject playerObject;
    public bool bInBattle = false;

    private List<GameObject> _turnOrder;

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

    public void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }


    private void Battle()
    {
        while (bInBattle)
        {
            
        }
    }
    
    public GameObject StartBattle(GameObject enemy, Transform spawnPoint)
    {
        bInBattle = true;
        var instancedEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        if (instancedEnemy.GetComponent<FightComponent>().InitialSpeed >
            playerObject.GetComponent<FightComponent>().InitialSpeed)
        {
            _turnOrder.Add(instancedEnemy);
            _turnOrder.Add(playerObject);
        }
        else
        {
            _turnOrder.Add(playerObject);
            _turnOrder.Add(instancedEnemy);
        }

        return instancedEnemy;
    }

    public void EndBattle()
    {
        bInBattle = false;
    }

}
