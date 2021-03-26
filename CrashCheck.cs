using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using ElephantSDK;

public class CrashCheck : MonoBehaviour
{
    private JoystickController joyCon;

    private bool isLanding;

    public GameObject kirmiziEkran;

    public Animator anim;

    public WingRotator wingRotator;

    public GameObject joystick;
    
    public GameObject loseUI;

    public SceneManagerScript sceneManage;
    // Start is called before the first frame update
    void Start()
    {
        joyCon = GameObject.FindGameObjectWithTag("JoyCon").GetComponent<JoystickController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bina":
                joyCon.canPlay = false;
                joyCon.mainMovementObj.GetComponent<Rigidbody>().isKinematic = false;
                joyCon.mainMovementObj.GetComponent<Rigidbody>().useGravity = true;
                joyCon.mainMovementObj.GetComponent<BoxCollider>().enabled = true;
                kirmiziEkran.SetActive(true);
                anim.enabled = false;
                wingRotator.slowingDown = true;
                print("öldüm");
                Invoke(nameof(openLose), 2f);
                break;
            case "Coin":
                sceneManage.coinCount += 50;
                Destroy(other.gameObject);
                print("para kazandım");
                break;
            case "Helipad" when !isLanding:
                isLanding = true;
                joyCon.CheckNearHelipad();
                break;
        }
    }

    void openLose()
    {
        Elephant.LevelFailed(PlayerPrefs.GetInt("Level"));
        loseUI.SetActive(true);
        joystick.SetActive(false);
    }
}
