using System.Collections;
using UnityEngine;

public enum BattleState 
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}


public class BattleManager : MonoBehaviour
{
    public BattleState state;

    private FightComponent _playerUnit;
    private FightComponent _enemyUnit;
    
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;


    public void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint);
        _playerUnit = playerObject.GetComponent<FightComponent>();

        GameObject enemyObject = Instantiate(enemyPrefab, enemySpawnPoint);
        _enemyUnit = enemyObject.GetComponent<FightComponent>();

        yield return new WaitForSeconds(2f);

        state = _playerUnit.InitialSpeed >= _enemyUnit.InitialSpeed ? BattleState.PLAYERTURN : BattleState.ENEMYTURN;
        if (state == BattleState.PLAYERTURN)
        {
            PlayerTurn();
        } 
        else
        {
            EnemyTurn();
        }
    }

    public void SelectTarget()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        _playerUnit.SelectTarget();
    }

    public void OnPlayerAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        _playerUnit.Attack();
        yield return new WaitForSeconds(2f);
        
    }

    private void PlayerTurn()
    {

    }

    private void EnemyTurn()
    {

    }

}
