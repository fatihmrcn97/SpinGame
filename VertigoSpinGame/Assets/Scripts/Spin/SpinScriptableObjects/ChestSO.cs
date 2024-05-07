
using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Chests")]
public class ChestSO : SpinItemSO
{
    public override void AddRewardToInventory()
    {
        Debug.Log("Chest Added to inventory!");
    }

    public override bool IsBomb()
    {
        return false;
    }
}
