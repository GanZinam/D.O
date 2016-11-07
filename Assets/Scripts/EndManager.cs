using UnityEngine;
using System.Collections;

public class EndManager : MonoBehaviour
{
    public GameObject reStartLayer;
    public GameObject gameEndLayer;
    public GameObject gameStartLayer;
    public GameObject gm;

    public UnityEngine.UI.Text scoreText;

    void Start()
    {
        reSet();
    }

    public void reSet()
    {
        Singleton.getInstance.count++;
        scoreText.text = "" + Singleton.getInstance.count;

    }

    public void retryButton()
    {
        reStartLayer.SetActive(false);
        gameEndLayer.SetActive(false);

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in objs)
        {
            obj.SendMessage("brokeMySelf");
        }

        GameObject.Find("Player").SendMessage("reSetting");
        Singleton.getInstance.gameEnd = false;
        Singleton.getInstance.count = 0;
        gm.SendMessage("reSetting");

        gameStartLayer.SetActive(true);

        GameObject[] objs2;
        objs = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }

    public void mainButton()
    {
        Application.LoadLevel("InGameScene");
    }
}
