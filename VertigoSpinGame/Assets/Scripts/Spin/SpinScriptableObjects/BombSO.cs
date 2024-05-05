using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpinItems/Bombs")]
public class BombSO : SpinItemSO
{
    public override bool IsBomb()
    {
        return true;
    }
}
