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
    private bool _targetAttack = false;
    
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public HUDManager hudManager;
    public TMP_Text dialogueText;

    public void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        bool playersTurn = state == BattleState.PLAYERTURN;
        hudManager.EnableUI(playersTurn);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint);
        _playerUnit = playerObject.GetComponent<FightComponent>();
        _playerTargets = playerObject.GetComponent<TargetComponent>().initialTargets;

        GameObject enemyObject = Instantiate(enemyPrefab, enemySpawnPoint);
        _enemyUnit = enemyObject.GetComponent<FightComponent>();
        _enemyTargets = enemyObject.GetComponent<TargetComponent>().initialTargets;

        hudManager.SetupHUD();

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
        hudManager.targetUI.SetActive(true);
        hudManager.SetSelection(_enemyTargets[_targetIndex].targetName);
    }

    public void NextTarget()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        _targetIndex = ++_targetIndex % _enemyTargets.Length;
        hudManager.SetSelection(_enemyTargets[_targetIndex].targetName);
    }

    public void OnTargetAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        _targetAttack = true;
        StartCoroutine(PlayerAttack());
    }

    public void OnBack()
    {
        _targetAttack = false;
        hudManager.SetSelection("");
        hudManager.targetUI.SetActive(false);
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
        var playerAttackDamage = _playerUnit.attackPower * _playerUnit.weaponData.baseDamage;
        if (_targetAttack) 
        {
            _enemyTargets[_targetIndex].TakeDamage(playerAttackDamage);
            dialogueText.text = $"Enemy's {_enemyTargets[_targetIndex].targetName} has taken {playerAttackDamage} points of damage";
            OnBack();
        }
        else
        {
            _enemyUnit.TakeDamage(playerAttackDamage);
            dialogueText.text = $"Enemy has taken {playerAttackDamage} damage";
        }
        yield return new WaitForSeconds(1f);
        dialogueText.text = "";
        _targetAttack = false;
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

        hudManager.EnableUI(false);
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Now is the time to strike!";
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
