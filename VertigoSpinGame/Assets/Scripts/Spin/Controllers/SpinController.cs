using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{

    [SerializeField] private List<SpinItem> spinItems;

    [SerializeField] private List<SpinItemSO> spinItemsSO;

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
            winnableItem.FillTheItem(randomItem.itemWinCount, randomItem.itemImage,randomItem);
            i++;
        }
    }

    private void OnEnable()
    {
       // Events.OnEndSpinAction += ReFillSpin;
    }

    private void OnDisable()
    {
      //  Events.OnEndSpinAction -= ReFillSpin;
    }




    private void ReFillSpin()
    {
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

        Fill(false);
    }

    private void Fill(bool isBombIncluded)
    {
        foreach (var item in spinItems)
        {
            var randomItem = spinItemsSO[Random.Range(0, spinItemsSO.Count)];
            var winnableItem = item.GetComponent<SpinItem>();
            winnableItem.FillTheItem(randomItem.itemWinCount, randomItem.itemImage, randomItem);
        }
    }
 

    // 5th spin make bronz 30th gold

    // etc.

}
