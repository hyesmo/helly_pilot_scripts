using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public bool didNearMissThis;

    private bool uiPoppedUp;

    public bool isNearMissing;

    public float timer;

    private UIControl uiControl;
    // Start is called before the first frame update
    void Start()
    {
        uiControl = GameObject.FindGameObjectWithTag("UIControl").GetComponent<UIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiPoppedUp)
            return;
        
        if (didNearMissThis)
        {
            print("yakından geçti la yavaş");
            uiControl.NearMiss();
            uiPoppedUp = true;
        }
        
        switch (isNearMissing)
        {
            case true:
                timer+=Time.deltaTime;
                break;
            case false when timer > 1.2f:
                didNearMissThis = true;
                break;
            case false:
                timer = 0;
                break;
        }
    }
}