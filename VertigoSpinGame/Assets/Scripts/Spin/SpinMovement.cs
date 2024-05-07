using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinMovement : SingletonMonobehaviour<SpinMovement>
{

    [SerializeField] private Button spinBtn;

    [Range(0, 10)]
    [SerializeField] private float speed;

    [Range(0, 10)]
    [SerializeField] private int randomTime;

    [SerializeField] private Transform rotateTransfrom;

    [SerializeField] private List<AnimationCurve> animationCurves;
     
    public bool Spinning { get; private set; }
    private float anglePerItem;
    private int itemNumber;


    private void OnEnable()
    {
        Events.OnRewardProcessFinished += MakeSpinableAgain;  
            }
    private void OnDisable()
    {
        Events.OnRewardProcessFinished -= MakeSpinableAgain;
    }

    private void Awake()
    {
        base.Awake();
        spinBtn.onClick.AddListener(Spin);
    }

    void Start()
    {
        Spinning = false;
        anglePerItem = 360 / 8;
    }

    private void Spin()
    {

        if (Spinning) return;

     
        Events.OnStartSpinAction?.Invoke();

        itemNumber = Random.Range(0, 8);
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);
        StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));


    }
    IEnumerator SpinTheWheel(float time, float maxAngle)
    {


        Spinning = true;

        float timer = 0.0f;
        float startAngle = rotateTransfrom.eulerAngles.z;
        maxAngle = maxAngle - startAngle;
            
        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += speed * Time.deltaTime;
            yield return 0;
        }

        rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle); 
         

        Events.OnEndSpinAction?.Invoke(itemNumber);
   

    }

    private void MakeSpinableAgain()
    { 
        Spinning = false;
    }
}
