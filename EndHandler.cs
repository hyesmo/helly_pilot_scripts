using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Facebook;
using Facebook.Unity;
using ElephantSDK;

public class EndHandler : MonoBehaviour
{
    [SerializeField]
    private float landTime;

    [SerializeField]
    private Ease easeType;

    public WingRotator wingRotate;

    public Animator animateHeli;

    public GameObject heliObj;

    private bool didplaysmoke;
    public ParticleSystem smokeparticle;

    public GameObject winUI;
    public GameObject joystick;

    public JoystickController joyCon;
    // Start is called before the first frame update

    private void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() => {
                FB.ActivateApp();
            });
        }
    }

    void Start()
    {
        joyCon = GameObject.FindGameObjectWithTag("JoyCon").GetComponent<JoystickController>();

        Elephant.LevelStarted(PlayerPrefs.GetInt("Level"));
    }

    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {


            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }

            else
            {

                //Handle FB.Init
                FB.Init(() => {
                    FB.ActivateApp();
                });
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (heliObj.transform.position == transform.position - new Vector3(0, 0.8f, 0) && !didplaysmoke)
        {
           smokeparticle.Play();
           didplaysmoke = true;
        }
    }

    public void LandAirCraft(Transform airCraft)
    {
        heliObj = airCraft.gameObject;
        Sequence seq = DOTween.Sequence();
        seq.Append(airCraft.transform.DOMove(this.transform.position - new Vector3(0, 0.8f, 0), landTime).SetEase(easeType))
            .Join(airCraft.transform.DORotate(this.transform.eulerAngles, landTime).SetEase(easeType));
        wingRotate.slowingDown = true;
        //heliObj.transform.eulerAngles = new Vector3(0, 180, 0);
        animateHeli.enabled = false;
        wingRotate.winGame = true;
        joyCon.CloseAllAlerts();
        Elephant.LevelCompleted(PlayerPrefs.GetInt("Level"));

        Invoke(nameof(RestartScene), 7f);
    }

    void RestartScene()
    {
        winUI.SetActive(true);
        joystick.SetActive(false);
    }
}
