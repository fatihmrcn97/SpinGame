
using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Chests")]
public class ChestSO : SpinItemSO
{ 
    public override bool IsBomb()
    {
        return false;
    }
}
