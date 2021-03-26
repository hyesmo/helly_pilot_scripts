using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UI;

public class FlashTextAndLight : MonoBehaviour
{
    public bool isLight;

    public Light light;

    public RawImage text;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        text = GetComponent<RawImage>();
        Flash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flash()
    {
        if (light != null)
        {
            light.DOIntensity(1.3f, .4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            text.DOColor(new Color(255, 255, 255, 0), .4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}
