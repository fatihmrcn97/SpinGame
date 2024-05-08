using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Money")]
public class MoneySO : SpinItemSO
{
    public override void AddRewardToInventory()
    {
        UIManager.instance.ScoreAdd(int.Parse(itemWinCount));
    }

    public override bool IsBomb()
    {
        return false;
    }
}
