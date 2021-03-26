using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField]
    private RawImage renderer;

    public Light[] lights;

    private int lastLight;

    public Texture nullText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanelTexture(Texture _texture)
    {
        renderer.texture = _texture;
    }

    public void setLights(int whichLight)
    {
        switch (whichLight)
        {
            case 0:
                lights[0].enabled = true;
                lights[1].enabled = false;
                lights[2].enabled = false;
                break;
            case 1:
                lights[1].enabled = true;
                lights[0].enabled = false;
                lights[2].enabled = false;
                break;
            case 2:
                lights[2].enabled = true;
                lights[0].enabled = false;
                lights[1].enabled = false;
                break;
            case 4:
                lights[0].enabled = false;
                lights[1].enabled = false;
                lights[2].enabled = false;
                break;
        }

    }
}