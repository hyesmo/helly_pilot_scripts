using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneManagerScript : MonoBehaviour
{
    private int levelCount;
    public int coinCount;
    
    public GameObject[] startPoints;
    public GameObject heliObj;
    public int startCount;
    
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        startCount = PlayerPrefs.GetInt("startCount");
        heliObj.transform.position = startPoints[startCount].transform.position;
        heliObj.transform.rotation = startPoints[startCount].transform.rotation;
        startCount++;
        
        coinCount = PlayerPrefs.GetInt("coinCount");
        levelCount = PlayerPrefs.GetInt("levelCount");
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount.ToString();
    }
    
    public void NextScene()
    {
        if (startCount > 5)
        {
            startCount = 0;
        }
        PlayerPrefs.SetInt("startCount", startCount);
        levelCount++;
        PlayerPrefs.SetInt("levelCount", levelCount);
        PlayerPrefs.SetInt("coinCount", coinCount);
        RestartScene();
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
