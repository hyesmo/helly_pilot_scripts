using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRadarRot : MonoBehaviour
{
    public Transform originalRadar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = originalRadar.transform.localEulerAngles;
    }
}
