using UnityEngine;

public class FightComponent : MonoBehaviour
{

    public GameObject enemy; 
    public Transform spawnPoint;
    
    private TargetComponent _targetComponent;

    [SerializeField]
    private WeaponScriptableObject _weaponData;

    [SerializeField]
    private int attackPower;

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
        if (BattleManager.Instance.bInBattle && _targetComponent != null)
            _targetComponent.SelectTarget();
        if (BattleManager.Instance.bInBattle && Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (!BattleManager.Instance.bInBattle && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Start Battle!");
            var _instancedEnemy = BattleManager.Instance.StartBattle(enemy, spawnPoint);
            _targetComponent = _instancedEnemy.GetComponent<TargetComponent>();
            _targetComponent.StartBattle();
        }
    }

    void Attack()
    {
        _targetComponent.AttackTarget(_weaponData.baseDamage * attackPower);
        Debug.Log($"{gameObject.name} is attacking {_targetComponent.SelectedTarget.targetName} with {_weaponData.weaponName} for {_weaponData.baseDamage} damage!");
    }

}
