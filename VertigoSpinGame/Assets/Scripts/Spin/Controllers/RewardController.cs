using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{

    public List<RewardItem> earnedObjects;

    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private SpinController spinController;

    [SerializeField] private Transform uiRewardsTransform;
 
    [SerializeField] private Button ExitBtn;
    [SerializeField] private GameObject bombExplodedPanel;

    private LoseScreen loseScreen;
    private RewardVisualEffects rewardVisualEffects;
    private void Awake()
    {
        rewardVisualEffects = GetComponent<RewardVisualEffects>();  
        loseScreen = GetComponent<LoseScreen>();
    }

    private void Start()
    {
        ExitBtn.onClick.AddListener(ExitBtnClick);
    }

    private void OnEnable()
    {
        Events.OnEndSpinAction += SpinResult; 
    }
    private void OnDisable()
    {
        Events.OnEndSpinAction -= SpinResult; 
    }
    private void ExitBtnClick()
    { 
        Events.OnSpinExit?.Invoke(earnedObjects);
    }

    private void SpinResult(int winnerNumber)
    {
        var rewardSO = spinController.SpinItems[winnerNumber].spinItemSO;

        if (rewardSO.IsBomb())
        {
            bombExplodedPanel.SetActive(true); 
            loseScreen.OpenAnimation();
            Events.OnRewardProcessFinished?.Invoke();
            return;
        }

        rewardVisualEffects.RewardCardAnimation(rewardSO);
        rewardVisualEffects.SpinResultParticlePlay(false);

        if (CheckRewardAlreadyEarned(rewardSO)) return;

        var rewardObject = Instantiate(rewardPrefab, uiRewardsTransform);
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
