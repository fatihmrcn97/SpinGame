using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinItem : MonoBehaviour
{

    private TextMeshProUGUI _earnedItemCountText;
    public SpinItemSO spinItemSO {  get; private set; }

    private void Awake()
    {
 
        _earnedItemCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillTheItem(SpinItemSO SO)
    {
        var imageObject = Instantiate(SO.itemPicturePrefab, transform);
        var _itemImage = imageObject.GetComponent<Image>();

        _itemImage.sprite = SO.itemImage;
        _earnedItemCountText.text = SO.itemWinCount;
        spinItemSO = SO;
    }

}
