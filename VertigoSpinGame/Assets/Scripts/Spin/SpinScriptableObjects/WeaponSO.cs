using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Weapons")]
public class WeaponSO : SpinItemSO
{
    public override void AddRewardToInventory()
    {
        Debug.Log("Weapon added to inventory");
    }

    public override bool IsBomb()
    {
        return false;
    }
}
