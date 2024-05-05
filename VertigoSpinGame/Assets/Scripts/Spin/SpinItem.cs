using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinItem : MonoBehaviour
{

    private TextMeshProUGUI _earnedItemCountText;
    private Image _itemImage;

    public SpinItemSO spinItemSO {  get; private set; }

    private void Awake()
    {
        _itemImage = GetComponentInChildren<Image>();
        _earnedItemCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillTheItem(string earnedItemCountText, Sprite itemImage, SpinItemSO SO)
    {
        _itemImage.sprite = itemImage;
        _earnedItemCountText.text = earnedItemCountText;
        spinItemSO = SO;
    }

}
