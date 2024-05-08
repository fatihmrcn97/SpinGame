
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RewardVisualEffects : MonoBehaviour
{

    [SerializeField] private Transform spinTransform, leftPanel;

    [SerializeField] private GameObject confettiParticle, deathParticle;

    [SerializeField] private Transform rewardStartPosition;

    public void RewardCardAnimation(SpinItemSO rewardSO)
    {
        StartCoroutine(RewardCardAnimationCorotine(rewardSO));
    }

    private IEnumerator RewardCardAnimationCorotine(SpinItemSO rewardSO)
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
        for (int i = 0; i < 4; i++)
        {
            var uiSlipping = rewardSO.InstantiateSlippingUI(spinTransform);
            uiSlipping.transform.localScale = Vector3.zero;
            uiSlipping.transform.DOScale(Vector3.one * 1.5f, .15f); 
            uiSlipping.transform.localPosition = new Vector3(Random.Range(-125, 125), Random.Range(-125, 125), 0);
            yield return new WaitForSeconds(.1f);
            uiSlipping.transform.DOMove(leftPanel.position+new Vector3(Random.Range(-25, 25), Random.Range(-55, 55),0), .45f).OnComplete(() => Destroy(uiSlipping));
        }
        yield return new WaitForSeconds(.56f);
        Events.OnRewardProcessFinished?.Invoke();
        Destroy(rewardCard);
    }


    public void SpinResultParticlePlay(bool isDeath)
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
