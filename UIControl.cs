using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIControl : MonoBehaviour
{
    public GameObject nearMissObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NearMiss()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(nearMissObj.transform.DOScale(Vector3.one * 2, .3f).SetEase(Ease.Linear))
            .AppendInterval(1f)
            .Append(nearMissObj.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.Linear));
    }

    void HighAltitudePopUp()
    {
        
    }

    void LowAltitudePopUp()
    {
        
    }
}
