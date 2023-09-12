using System.Collections;
using UnityEngine;
using TMPro;

public enum BattleState 
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    END
}


public class BattleManager : MonoBehaviour
{
    public BattleState state;

    private FightComponent _playerUnit;
    private FightComponent _enemyUnit;

    private TargetData[] _playerTargets;
    private TargetData[] _enemyTargets;
    private int _targetIndex = 0;
    
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public TMP_Text dialogueText;

    public void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint);
        _playerUnit = playerObject.GetComponent<FightComponent>();
        _playerTargets = playerObject.GetComponent<TargetComponent>().initialTargets;

        GameObject enemyObject = Instantiate(enemyPrefab, enemySpawnPoint);
        _enemyUnit = enemyObject.GetComponent<FightComponent>();
        _enemyTargets = enemyObject.GetComponent<TargetComponent>().initialTargets;

        yield return new WaitForSeconds(2f);

        state = _playerUnit.InitialSpeed >= _enemyUnit.InitialSpeed ? BattleState.PLAYERTURN : BattleState.ENEMYTURN;
        if (state == BattleState.PLAYERTURN)
        {
            PlayerTurn();
        } 
        else
        {
            StartCoroutine(EnemyAttack());
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "The player has won!";
            Destroy(_enemyUnit);
        } 
        else 
        {
            dialogueText.text = "Game Over";
        }
    }

    public void SelectTarget()
    {
        if (state != BattleState.PLAYERTURN)
            return;

    }

    public void NextTarget()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        _targetIndex = ++_targetIndex % _enemyTargets.Length;
    }

    public void OnAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        _enemyUnit.TakeDamage(_playerUnit.attackPower * _playerUnit.weaponData.baseDamage);
        dialogueText.text = $"Enemy has taken {_playerUnit.attackPower * _playerUnit.weaponData.baseDamage} damage";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "";

        if (_enemyUnit.currentHealth <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        } 
        else 
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
        }
    }

    private void PlayerTurn()
    {

    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(2f);
        _playerUnit.TakeDamage(_enemyUnit.attackPower * _enemyUnit.weaponData.baseDamage);
        
        dialogueText.text = $"Player has taken {_playerUnit.attackPower * _playerUnit.weaponData.baseDamage} damage";

        yield return new WaitForSeconds(1f);

        dialogueText.text = "";

        if (_playerUnit.currentHealth <= 0)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else 
            state = BattleState.PLAYERTURN;
    }
}
