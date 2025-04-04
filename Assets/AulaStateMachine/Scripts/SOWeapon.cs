using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class SOWeapon : ScriptableObject
{
    public int damage;
    public int range;
}
