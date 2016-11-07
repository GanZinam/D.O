using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool isBallMoving = false;
    bool finishLoad = false;
    public int blockNum = 0;
    
    [SerializeField]
    GameObject ground;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject character;

    [SerializeField]
    GameObject pauseLayer;

    GameObject nowMovingGroundObj = null;

    bool gameEnd = false;


    void Start()
    {

        StartCoroutine(delayOne());
    }

    void Update()
    {
        if (Singleton.getInstance.gameEnd)
        {
            if (mainCamera.transform.position.x != 0)
            {
                mainCamera.transform.position -= new Vector3(4 * Time.deltaTime, 0, 0);
                if (mainCamera.transform.position.x <= 0)
                {
                    mainCamera.transform.position = new Vector3(0, 0, -10);
                }
            }
        }
        else
        {
            mainCamera.transform.position = new Vector3(character.transform.position.x + 7.8f, mainCamera.transform.position.y, -10);
            if (isBallMoving && finishLoad)
            {
                if (mainCamera.transform.position.x >= 2f * (1 + blockNum))
                {
                    isBallMoving = false;
                    mainCamera.transform.position = new Vector3(2f * (1 + blockNum), 0, -10);
                    character.SendMessage("moveAgainPlayer");
                }
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (blockNum - Singleton.getInstance.count <= 4)
                    actBall();
            }
#elif UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(blockNum - Singleton.getInstance.count <= 4)
            actBall();
        }
#endif
        }
    }

    void actBall()
    {
        // 움직이던 벽이 이전 벽과 높이차이가 몇 안난다면 맞춰주기
        if (Mathf.Abs(Singleton.getInstance.beginGroundPos.y - nowMovingGroundObj.transform.position.y) <= 0.1)
        {
            nowMovingGroundObj.transform.position = new Vector3(nowMovingGroundObj.transform.position.x, Singleton.getInstance.beginGroundPos.y, 0);
        }

        // 움직이던 벽을 현재 기준 벽으로 변경한느 과정
        Singleton.getInstance.beginGroundPos = nowMovingGroundObj.transform.position;
        nowMovingGroundObj.SendMessage("stopMoving");
        nowMovingGroundObj = Instantiate(ground, new Vector3(1.98f * (blockNum + 1) - 7.81f, Singleton.getInstance.beginGroundPos.y - 1, 0), Quaternion.identity) as GameObject;
        nowMovingGroundObj.SendMessage("setCodeNum", ++blockNum);

        isBallMoving = true;
        //character.SendMessage("pausePlayerMove");
    }

    public void pauseButton()
    {
        Time.timeScale = 0;
        pauseLayer.SetActive(true);
    }

    public void resumeButton()
    {
        Time.timeScale = 1;
        pauseLayer.SetActive(false);
    }

    public void mainButton()
    {
        Application.LoadLevel("InGameScene");
    }

    IEnumerator delayOne()
    {
        yield return new WaitForSeconds(2f);

        deleyTwo();

        finishLoad = true;
    }

    public void deleyTwo()
    {
        nowMovingGroundObj = Instantiate(ground, new Vector3(1.98f * (blockNum + 1) - 7.81f, -3, 0), Quaternion.identity) as GameObject;
        nowMovingGroundObj.SendMessage("setCodeNum", ++blockNum);
        Singleton.getInstance.beginGroundPos = nowMovingGroundObj.transform.position;

        mainCamera.transform.position = new Vector3(character.transform.position.x + 7.8f, mainCamera.transform.position.y, -10);
    }

    public void reSetting()
    {
        if (GameObject.FindGameObjectsWithTag("Ground").Length  > 0)
        {
            StartCoroutine(delay());
        }
        else
        {
            isBallMoving = false;
            blockNum = 0;
            gameEnd = false;

            deleyTwo();

            StartCoroutine(delayPlayer());
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);

        reSetting();
    }

    IEnumerator delayPlayer()
    {
        yield return new WaitForSeconds(1);

        character.SendMessage("moveAgainPlayer");
    }

}