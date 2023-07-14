using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] int weaponlevel;

    public int WeaponLevel => weaponlevel;
}
