using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackController : MonoBehaviour , IItemList
{
    public List<GameObject> stackedMaterials; 

    public List<Transform> stackTransform;

    [SerializeField] private int maxStackCountBase = 20;

    [HideInInspector] public int maxStackCount;

    private void OnEnable()
    {
        Events.MaterialStackedEvent += CheckMax;
    }

    private void OnDisable()
    {
        Events.MaterialStackedEvent -= CheckMax;
    }

    private void Start()
    {
        maxStackCount = maxStackCountBase + PlayerPrefs.GetInt("maxCollected");
    }

    public void MaxStackCountUpdated()
    {
        maxStackCount = maxStackCountBase + PlayerPrefs.GetInt("maxCollected");
    }

    public void StackPositionHandler()
    {
        for (int i = 0; i < stackedMaterials.Count; i++)
        {
            stackedMaterials[i].transform.SetParent(stackTransform[i]);
            stackedMaterials[i].transform.DOLocalMove(Vector3.zero, .1f);
        }
    }
 
    private void CheckMax()
    {
        Vibration.Vibrate(10);
        UIManager.instance.maxUI.SetActive(stackedMaterials.Count >= maxStackCount);
    }

    #region INTERFACE IMPLEMENTATIONS

    public List<GameObject> StackedMaterialList()
    {
        return stackedMaterials;
    }

    public List<Transform> StackTransforms()
    {
        return stackTransform;
    }
    public int MaxStackCount()
    {
        return maxStackCount;
    }

    #endregion

}

