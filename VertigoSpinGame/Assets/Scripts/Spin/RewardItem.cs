using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{

    private TextMeshProUGUI _earnedItemCountText;
    private Image _itemImage;

    public SpinItemSO RewardItemSO { get; private set; }
    public int ItemCount = 0;
    private void Awake()
    { 
        _itemImage = GetComponentInChildren<Image>();
        _earnedItemCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillTheItem(string earnedItemCountText, Sprite itemImage , SpinItemSO rewardItemsSO)
    {
        _itemImage.sprite = itemImage;
        _earnedItemCountText.text = earnedItemCountText;
        RewardItemSO = rewardItemsSO;
        ItemCount = int.Parse(earnedItemCountText);
    }

    public void UpdateTheUI()
    {
        _earnedItemCountText.text = ItemCount.ToString();
    }
}
