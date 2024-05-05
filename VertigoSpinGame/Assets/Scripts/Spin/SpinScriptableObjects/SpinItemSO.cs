using UnityEngine;

[CreateAssetMenu(menuName ="WinnableItem")]
public abstract class SpinItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string itemWinCount;

    public ItemType ItemType;

    public GameObject itemPicturePrefab;

    public abstract bool IsBomb();
}
