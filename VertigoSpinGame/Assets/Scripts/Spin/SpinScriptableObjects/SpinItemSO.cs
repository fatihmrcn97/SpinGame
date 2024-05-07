using UnityEngine;
using UnityEngine.UI;

public abstract class SpinItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string itemWinCount;

    public ItemType ItemType;

    public GameObject itemPicturePrefab;
    public GameObject rewardCardUIPrefab;

    public abstract bool IsBomb();

    public abstract void AddRewardToInventory();

    public GameObject InstantiateRewardCard(Transform parent)
    {
        var obj = Instantiate(rewardCardUIPrefab, parent);
        obj.transform.GetChild(2).GetComponent<Image>().sprite = itemImage;
        return obj;
    }


    public GameObject InstantiateSlippingUI(Transform parent)
    {
        var obj = Instantiate(itemPicturePrefab, parent);
        obj.GetComponent<Image>().sprite = itemImage;
        return obj;
    }
}
