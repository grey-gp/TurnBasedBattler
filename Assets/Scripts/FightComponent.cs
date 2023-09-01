using System.Collections.Generic;
using UnityEngine;

public class FightComponent : MonoBehaviour
{

    public GameObject enemy; 
    public Transform spawnPoint;
    
    private GameObject _instancedEnemy;
    private TargetData _selectedTarget;
    private int targetIndex = 0;

    private List<TargetData> targets;

    [SerializeField]
    private WeaponScriptableObject _weaponData;
    // Start is called before the first frame update
    void Start()
    {
        if (_weaponData == null)
        {
            _weaponData = Resources.Load<WeaponScriptableObject>("Weapons/Dagger");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectTarget();
        if (BattleManager.Instance.bInBattle && Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _instancedEnemy = BattleManager.Instance.StartBattle(enemy, spawnPoint);
            targets = _instancedEnemy.GetComponent<TargetComponent>().GetActiveTargets();
        }
    }

    void Attack()
    {
        Debug.Log($"{gameObject.name} is attacking {_selectedTarget.targetName} with {_weaponData.weaponName} for {_weaponData.baseDamage} damage!");
    }

    void SelectTarget()
    {

        if (!BattleManager.Instance.bInBattle)
            return;
        _selectedTarget = targets[targetIndex];
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            targetIndex = ++ targetIndex % targets.Count;
            _selectedTarget = targets[targetIndex];
            Debug.Log($"Currently selected target {_selectedTarget.targetName}");
        }
    }
}
