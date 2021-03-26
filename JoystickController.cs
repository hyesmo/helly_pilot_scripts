using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Transform joystickTransform;

    public Joystick joystickObj;

    public Transform heliObj;

    public Transform mainMovementObj;
    public float verticalSpeed = 1f;
    public float rotationSpeed = 0.1f;
    public float heliForwardSpeed;

    private bool didFirstInput;

    public List<BuildingScript> _buildingScripts;

    public bool canPlay = true;

    public int ringCount;
    public int coinCount;

    public GameObject heliPadObj;

    [SerializeField]
    private PanelScript panelScript;

    [SerializeField]
    private Texture[] panelTextures;

    public GameObject rayObj;
    public GameObject tapToPlay;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("Bina"))
        {
            BuildingScript a = VARIABLE.GetComponent<BuildingScript>();
            _buildingScripts.Add(a);
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0) && !didFirstInput)
        {
            didFirstInput = true;
            tapToPlay.SetActive(false);
            
        }
        if(!didFirstInput)
            return;
        if(!canPlay)
            return;
        
        AnimateRobotJoystick();
        AnimateHeli();
        HeliUpDown();
        HeliLeftRight();
        HeliForward();
        ClampPos();
        //CheckNearMiss();
        CheckCollisionAlert();
    }
    
    public void AnimateRobotJoystick()
    {
            float xRot = Mathf.Sign(joystickObj.Vertical) * Mathf.Lerp(0, 15f, Mathf.Abs(joystickObj.Vertical));
            float zRot = -Mathf.Sign(joystickObj.Horizontal) * Mathf.Lerp(0, 10f, Mathf.Abs(joystickObj.Horizontal));
            joystickTransform.localEulerAngles = new Vector3(xRot, 0, zRot);
    }

    void AnimateHeli()
    {
        float zRot = -Mathf.Sign(joystickObj.Horizontal) * Mathf.Lerp(0, 10f, Mathf.Abs(joystickObj.Horizontal));
        heliObj.localEulerAngles = new Vector3(0, 180, zRot);
    }

    void HeliUpDown()
    {
        float yPos = Mathf.Sign(joystickObj.Vertical) * Mathf.Lerp(0, verticalSpeed, Mathf.Abs(joystickObj.Vertical));
        mainMovementObj.position += new Vector3(0, yPos, 0);
    }

    void ClampPos()
    {
        if (mainMovementObj.position.y > 7f)
        {
            var position = mainMovementObj.position;
            position = new Vector3(position.x, 7f, position.z);
            mainMovementObj.position = position;
        }
        
        
        if (mainMovementObj.position.y < -1.5f)
        {
            var position = mainMovementObj.position;
            position = new Vector3(position.x, -1.5f, position.z);
            mainMovementObj.position = position;
        }
    }

    void HeliLeftRight()
    {
        float yRot = Mathf.Sign(joystickObj.Horizontal) * Mathf.Lerp(0, rotationSpeed, Mathf.Abs(joystickObj.Horizontal));
        mainMovementObj.eulerAngles += new Vector3(0, yRot, 0);
    }

    void HeliForward()
    {
        mainMovementObj.position += mainMovementObj.forward * heliForwardSpeed;
    }

    void CheckNearMiss()
    {
        for (int i = 0; i < _buildingScripts.Count; i++)
        {
            if (Vector3.Distance(mainMovementObj.position, _buildingScripts[i].transform.position) < 6.4f)
            {
                _buildingScripts[i].isNearMissing = true;
            }
            else
            {
                _buildingScripts[i].isNearMissing = false;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void CheckNearHelipad()
    {
        canPlay = false;
        heliPadObj.GetComponent<EndHandler>().LandAirCraft(mainMovementObj);
    }

    public void CheckCollisionAlert()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayObj.transform.position, rayObj.transform.right, out hit, 4)
            || Physics.Raycast(rayObj.transform.position, -rayObj.transform.right, out hit, 4))
        {
            if (hit.transform.tag == "Bina")
            {
                hit.transform.GetComponent<BuildingScript>().isNearMissing = true;
                panelScript.ChangePanelTexture(panelTextures[1]);
                panelScript.setLights(1);
            }
        }
        else
        {
            for (int i = 0; i < _buildingScripts.Count; i++)
            {
                _buildingScripts[i].isNearMissing = false;
            }
            
            if (mainMovementObj.position.y > 5.4f)
            {
                panelScript.ChangePanelTexture(panelTextures[2]);
                panelScript.setLights(2);
                print("highAltitude");
            }
            else if (mainMovementObj.position.y < 1f)
            {
                panelScript.ChangePanelTexture(panelTextures[0]);
                panelScript.setLights(0);
                print("lowAltitude");
            }
            else
            {
                panelScript.ChangePanelTexture(panelTextures[3]);
                panelScript.setLights(4);
                print("noLight");
            }
        }
    }

    public void CloseAllAlerts()
    {
        panelScript.ChangePanelTexture(panelTextures[3]);
        panelScript.setLights(4);
        print("noLight");
    }
}
