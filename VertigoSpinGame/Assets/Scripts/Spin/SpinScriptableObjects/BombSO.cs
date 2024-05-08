using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Bombs")]
public class BombSO : SpinItemSO
{
    public override void AddRewardToInventory()
    {
        Debug.Log("Failed inventry exloped");
    }

    public override bool IsBomb()
    {
        return true;
    }
}
