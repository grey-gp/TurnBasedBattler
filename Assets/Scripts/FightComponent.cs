using UnityEngine;

public class FightComponent : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int currentSpeed;
    public int InitialSpeed { get; private set; }
    public int attackPower;
    public WeaponScriptableObject weaponData;

    // Start is called before the first frame update
    void Start()
    {
        if (weaponData == null)
        {
            weaponData = Resources.Load<WeaponScriptableObject>("Weapons/Dagger");
        }

        currentHealth = maxHealth;
        currentSpeed = InitialSpeed;
    }


    public void Attack()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

}
