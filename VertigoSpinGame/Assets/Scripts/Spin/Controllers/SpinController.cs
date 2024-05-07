using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinController : MonoBehaviour
{ 

    [SerializeField] private List<SpinItem> spinItems;

    [SerializeField] private List<SpinItemSO> spinItemsSO;

    [SerializeField] private List<SpinItemSO> betterSpinItemsSO;

    [SerializeField] private SpinItemSO bombSO;

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
    }

    private void OnDisable()
    {
       Events.OnRewardWinned -= ReFillSpin;
    }




    private void ReFillSpin()
    {
        DestroyAllItems();

        spinCount++;
        if(spinCount % 5 == 0)
        {
            if(spinCount % 30 == 0)
            {
                Fill(false);
                return;
            }

            Fill(false);
            return;
        }

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
 


    private void DestroyAllItems()
    {
        foreach (var item in spinItems)
        {
            var imagePrefab = item.GetComponentInChildren<Image>().gameObject;
            Destroy(imagePrefab);
        }
    }
   

}
