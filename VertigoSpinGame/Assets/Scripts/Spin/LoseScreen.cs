using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{

    private RewardController rewardController;

    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject loseCard;

    [SerializeField] private Button giveUpBtn,reviveWithMoneyBtn,reviveWithAdBtn;

    private void Awake()
    {
        rewardController = GetComponent<RewardController>();

        giveUpBtn.onClick.AddListener(GiveUpBtn);
        reviveWithAdBtn.onClick.AddListener(ReviveButton);
        reviveWithMoneyBtn.onClick.AddListener(ReviveWithMoneyButton);
    }

    public void OpenAnimation()
    {
        var animm  = loseCard.GetComponent<Animator>();
        animm.SetTrigger("play");

    }

    private void GiveUpBtn()
    {
        var items = rewardController.earnedObjects;
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
        losePanel.SetActive(false); 
    }

    private void ReviveButton()
    {
        losePanel.SetActive(false);
    }

    private void ReviveWithMoneyButton()
    {
        if (UIManager.instance.Score > 0)
        {
            UIManager.instance.ScoreAdd(-1);
            losePanel.SetActive(false);
        }
    }
   
}
