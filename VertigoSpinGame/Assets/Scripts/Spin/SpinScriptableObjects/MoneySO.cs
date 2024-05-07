using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Money")]
public class MoneySO : SpinItemSO
{
    public override void AddRewardToInventory()
    {
        UIManager.instance.ScoreAdd(1);
    }

    public override bool IsBomb()
    {
        return false;
    }
}
