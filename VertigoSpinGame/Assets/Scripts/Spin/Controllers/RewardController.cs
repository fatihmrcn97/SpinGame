using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    // Kazanilan object ui büyüp kart olarak gözükecek ve sonra solt tarafa particle olarak geçicek ordada kücük kart olarak satýr satýr birikece. card game benzeri

    public List<RewardItem> earnedObjects;

    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private SpinController spinController;

    [SerializeField] private Transform rewardStartPosition;
    [SerializeField] private Transform spinTransform,leftPanel, uiRewardsTransform;
 

    [SerializeField] private GameObject confettiParticle, deathParticle;
    [SerializeField] private Button ExitBtn;

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
            AllRewardLosed();
            SpinResultParticlePlay(true);
            Events.OnRewardProcessFinished?.Invoke();
            return;
        }

        StartCoroutine(RewardCardAnimation(rewardSO));
        SpinResultParticlePlay(false);

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

    private void AllRewardLosed()
    {
        foreach (var item in earnedObjects)
        {
            Destroy(item.gameObject);
        }
        earnedObjects.Clear();
    }
     

    private IEnumerator RewardCardAnimation(SpinItemSO rewardSO)
    {
        yield return new WaitForSeconds(1.25f);
        // Animation for reward
        var rewardCard = rewardSO.InstantiateRewardCard(spinTransform);
        rewardCard.transform.localScale = Vector3.zero;

        rewardCard.transform.localPosition = rewardStartPosition.localPosition;

        var seq = DOTween.Sequence();
        seq.Join(rewardCard.transform.DOScale(Vector3.one, .25f));
        seq.Join(rewardCard.transform.DOScale(Vector3.one, .25f));
        seq.Join(rewardCard.transform.DOLocalMove(Vector3.zero, .25f));

        yield return new WaitForSeconds(.9f);
        Events.OnRewardWinned?.Invoke();
        for (int i = 0; i < 4; i++) {
            var uiSlipping = rewardSO.InstantiateSlippingUI(spinTransform);
            uiSlipping.transform.localScale = Vector3.one * 1.5f;
            uiSlipping.transform.localPosition = new Vector3(Random.Range(-125,125), Random.Range(-125, 125),0);
            yield return new WaitForSeconds(.05f);
            uiSlipping.transform.DOMove(leftPanel.position, .45f).OnComplete(()=>Destroy(uiSlipping));
        }
        yield return new WaitForSeconds(.56f);
        Events.OnRewardProcessFinished?.Invoke();   
        Destroy(rewardCard);
    }

    private void SpinResultParticlePlay(bool isDeath)
    {
        if (isDeath)
        {
            var deathPs = Instantiate(deathParticle, spinTransform);
            deathPs.transform.localPosition = rewardStartPosition.localPosition;
        }
        else
        {
            var confettiPs = Instantiate(confettiParticle, spinTransform);
            confettiPs.transform.localPosition = rewardStartPosition.localPosition;
        }
    }
     
}
