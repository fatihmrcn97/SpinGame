using DG.Tweening;
using System.Collections.Generic;
using Cysharp.Threading.Tasks; 
using UnityEngine;

public class GetMaterialFromMachine : MonoBehaviour
{
    public List<GameObject> singleMaterial;

    private bool _isInTrigger;

    private MachineController _machineController;

    private const float ProgressTime = .2f;

    private void Start()
    {
        _machineController = GetComponentInParent<MachineController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.PLAYER_TAG)) return;
        _isInTrigger = true;
        PlayerGettingStackMaterials(other.GetComponent<PlayerStackController>()).Forget();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.PLAYER_TAG))
            _isInTrigger = false;
    } 

    private async UniTaskVoid PlayerGettingStackMaterials(IItemList stackController)
    {
        while (_isInTrigger)
        { 
            if (singleMaterial.Count <= 0)
            {
                await UniTask.Delay(250); 
                continue;
            } 
            if (stackController.StackedMaterialList().Count >= stackController.MaxStackCount()) return;

            var currentSingleMaterial = singleMaterial[^1];
            singleMaterial.Remove(currentSingleMaterial);
            _machineController._stackSystem.SetTheStackPositonBack(singleMaterial.Count);
            
            currentSingleMaterial.transform.DOLocalJump(Vector3.zero, .5f, 1, ProgressTime);
            currentSingleMaterial.transform.DOLocalRotate(Vector3.zero, ProgressTime);
            currentSingleMaterial.transform.SetParent(stackController.StackTransforms()[stackController.StackedMaterialList().Count]);
      
            stackController.StackedMaterialList().Add(currentSingleMaterial);
            Events.MaterialStackedEvent?.Invoke();
            await UniTask.Delay(250);
        }
    }
}
