using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinExit : MonoBehaviour
{

    private void OnEnable()
    {
        Events.OnSpinExit += AddRewardsToInventory;
    }
    private void OnDisable()
    {
        Events.OnSpinExit -= AddRewardsToInventory;
    }


    private void AddRewardsToInventory(List<RewardItem> rewardItems)
    { 
        StartCoroutine(AddInventory(rewardItems));
    }
    private IEnumerator AddInventory(List<RewardItem> rewardItems)
    {
        if (SpinMovement.Instance.Spinning) yield break;

        foreach (RewardItem item in rewardItems)
        {
            item.RewardItemSO.AddRewardToInventory();
            Destroy(item.gameObject,.05f);
            yield return new WaitForSeconds(.15f);
        }

        rewardItems.Clear();
    }
}
