using UnityEngine;

public enum DamageType
{
    None,
    Pierce,
    Blunt,
    Slash,
    Fire,
    Frost,
    Lightning
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    public DamageType damageType;
    public string weaponName;
    public int baseDamage;
    public Sprite weaponSprite;
}
