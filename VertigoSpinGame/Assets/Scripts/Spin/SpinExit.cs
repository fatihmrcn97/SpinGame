using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinExit : MonoBehaviour
{
    [SerializeField] private Transform rewardCollectionPanel;

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

        rewardCollectionPanel.gameObject.SetActive(true);

        foreach (RewardItem item in rewardItems)
        {
            item.RewardItemSO.AddRewardToInventory();
            Destroy(item.gameObject,.05f);
            yield return new WaitForSeconds(.15f);
            var rewardCard = item.RewardItemSO.InstantiateRewardCard(rewardCollectionPanel);
            rewardCard.transform.localScale = Vector3.zero;
            rewardCard.transform.localPosition = Vector3.zero;

            var seq = DOTween.Sequence();
            seq.Join(rewardCard.transform.DOScale(Vector3.one, .25f));
            seq.Join(rewardCard.transform.DOScale(Vector3.one, .25f));
            seq.Join(rewardCard.transform.DOLocalMove(Vector3.zero, .25f));
            yield return new WaitForSeconds(1.35f);
            Destroy(rewardCard);

        }

        rewardItems.Clear();
        yield return new WaitForSeconds(.55f);
        rewardCollectionPanel.gameObject.SetActive(false);
    }
}
