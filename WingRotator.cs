using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WingRotator : MonoBehaviour
{
    public float speed;

    public bool slowingDown;

    public bool stopped;

    public ParticleSystem[] particles;

    public bool winGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stopped)
            return;
        if (slowingDown)
            speed -= 0.02f;

        if (speed <= 0)
        {
            speed = 0;
            stopped = true;
            if (winGame)
            {
                foreach (var VARIABLE in particles)
                {
                    VARIABLE.Play();
                }
            }
            
        }
        
        transform.localEulerAngles += new Vector3(0, speed, 0);
    }
}
