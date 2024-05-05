using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    // Kazanilan object ui büyüp kart olarak gözükecek ve sonra solt tarafa particle olarak geçicek ordada kücük kart olarak satýr satýr birikece. card game benzeri

    public List<RewardItem> earnedObjects;

    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private SpinController spinController;

    private void OnEnable()
    {
        Events.OnEndSpinAction += RewardEarned;
    }
    private void OnDisable()
    {
        Events.OnEndSpinAction -= RewardEarned;
    }

    private void RewardEarned(int winnerNumber)
    {
        var rewardSO = spinController.SpinItems[winnerNumber].spinItemSO;
         
        if(CheckRewardAlreadyEarned(rewardSO)) return;

        var rewardObject = Instantiate(rewardPrefab, transform);
        var reward = rewardObject.GetComponent<RewardItem>();
        reward.FillTheItem(rewardSO.itemWinCount, rewardSO.itemImage,rewardSO);
        earnedObjects.Add(reward);
    }


    private bool CheckRewardAlreadyEarned(SpinItemSO rewardSO)
    {
        foreach (var item in earnedObjects)
        {
            if (item.RewardItemSO.ItemType == rewardSO.ItemType)
            {
                item.ItemCount += int.Parse(rewardSO.itemWinCount);
                item.UpdateTheUI();
                return true;
            }
        }
        return false;
    }
}
