using UnityEngine;

public class FightComponent : MonoBehaviour
{

    public float currentSpeed;
    public float InitialSpeed { get; private set; }

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

        currentSpeed = InitialSpeed;
    }

    public void SelectTarget()
    {
        _targetComponent.SelectTarget();
    }

    public void Attack()
    {
        _targetComponent.AttackTarget(_weaponData.baseDamage * attackPower);
        Debug.Log($"{gameObject.name} is attacking {_targetComponent.SelectedTarget.targetName} with {_weaponData.weaponName} for {_weaponData.baseDamage} damage!");
    }

}
