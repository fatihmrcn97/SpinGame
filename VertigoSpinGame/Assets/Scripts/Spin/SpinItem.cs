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
 
        _earnedItemCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillTheItem(string earnedItemCountText, Sprite itemImage, SpinItemSO SO)
    {
        var imageObject = Instantiate(SO.itemPicturePrefab, transform);
        var _itemImage = imageObject.GetComponent<Image>();

        _itemImage.sprite = itemImage;
        _earnedItemCountText.text = earnedItemCountText;
        spinItemSO = SO;
    }

}
