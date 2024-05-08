using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinController : MonoBehaviour
{ 

    [SerializeField] private List<SpinItem> spinItems;

    [SerializeField] private List<SpinItemSO> spinItemsSO;

    [SerializeField] private List<SpinItemSO> betterSpinItemsSO;

    [SerializeField] private SpinItemSO bombSO;

    [SerializeField] private Image wheelImage, indicatorImage;

    [SerializeField] private Sprite bronzWhell, bronzIndicator, silverWheel, silerIndicator, goldWheel, goldIndicator;

    [SerializeField] private TextMeshProUGUI whellColorTxt;

    private int spinCount = 0;

    public List<SpinItem> SpinItems => spinItems;

    private void Start()
    {
        // Fill the spin.

        var i = 0;
        foreach (var item in spinItems)
        {
            var randomItem = spinItemsSO[i];
            var winnableItem = item.GetComponent<SpinItem>();
            winnableItem.FillTheItem(randomItem);
            i++;
        }
    }

    private void OnEnable()
    {
      Events.OnRewardWinned += ReFillSpin;
        Events.OnSpinExitFinished += SpinCountReset;
    }

    private void OnDisable()
    {
       Events.OnRewardWinned -= ReFillSpin;
        Events.OnSpinExitFinished += SpinCountReset;
    }


    private void ReFillSpin()
    {
        DestroyAllItems();

        spinCount++;
        if(spinCount % 5 == 0)
        {
            if(spinCount % 30 == 0)
            {
                SetupWheelType(2);
                Fill(false);
                return;
            }
            SetupWheelType(1);
            Fill(false);
            return;
        }
        SetupWheelType(0);
        Fill(true);
    }

    private void Fill(bool isBombIncluded)
    {

        int betterSpintItemCount = 0;
        foreach (var item in spinItems)
        {
            var randomItem = spinItemsSO[Random.Range(0, spinItemsSO.Count)];
              
            if(spinCount>3 && betterSpintItemCount<spinCount/3)
            {
                randomItem = betterSpinItemsSO[Random.Range(0, betterSpinItemsSO.Count)];
                betterSpintItemCount++;
            }


            var winnableItem = item.GetComponent<SpinItem>();
            if (isBombIncluded)
            {
                isBombIncluded = false;
                randomItem = bombSO; 
            }
            winnableItem.FillTheItem(randomItem);
        }
    }
 
    private void SpinCountReset()
    {
        spinCount = 0;
    }


    private void DestroyAllItems()
    {
        foreach (var item in spinItems)
        {
            var imagePrefab = item.GetComponentInChildren<Image>().gameObject;
            Destroy(imagePrefab);
        }
    }
   

    private void SetupWheelType(int wheelIndex)
    {
        if(wheelIndex == 0)
        {
            // Bronz default whell
            wheelImage.sprite = bronzWhell;
            indicatorImage.sprite = bronzIndicator;
            whellColorTxt.text = "BRONZ WHEEL";
            whellColorTxt.color = new Color(.92f , .45f, 0,1);
        }
        else if(wheelIndex == 1)
        {
            wheelImage.sprite = silverWheel;
            indicatorImage.sprite = silerIndicator;
            whellColorTxt.text = "SILVER WHEEL";
            whellColorTxt.color = Color.gray;
        }
        else
        {
            wheelImage.sprite = goldWheel;
            indicatorImage.sprite = goldIndicator;
            whellColorTxt.text = "GOLDEN WHEEL";
            whellColorTxt.color = Color.yellow;

        }
    }
}
