using UnityEngine;
using System.Collections;

public class SelectTheme : MonoBehaviour
{
    public GameObject selectLight;

    public GameObject[] theme;

    void Start()
    {
        Vector3 pos = theme[Singleton.getInstance.themeNum].transform.position;
        pos.x += 4.8f;
        selectLight.transform.position = pos;
    }

    public void clickTheme(int themeNum)
    {
        Vector3 pos = theme[themeNum].transform.position;
        pos.x += 2.5f;
        selectLight.transform.position = pos;

        Singleton.getInstance.themeNum = themeNum;
    }

    public void goToMainScene()
    {
        Application.LoadLevel("InGameScene");
    }
}
