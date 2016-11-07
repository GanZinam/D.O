using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour
{
    public bool isTarget = true;
    public float arrange = 1.5f;
    public int myCode = 0;

    bool moveWhere = true;     // false = 위, true = 아래
    bool brokeMe = false;

    public Sprite[] spr;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spr[Singleton.getInstance.themeNum];
    }

    void Update()
    {
        if (brokeMe)
        {
            transform.position -= new Vector3(0, 5 * Time.deltaTime, 0);

            if (transform.position.y <= -9.4)
                Destroy(gameObject);
        }
        else if (isTarget)
        {
            if (moveWhere)
                transform.position += new Vector3(0, (4 + Singleton.getInstance.count / 10) * Time.deltaTime, 0);
            else
                transform.position -= new Vector3(0, (4 + Singleton.getInstance.count / 10) * Time.deltaTime, 0);

            if (transform.position.y >= Singleton.getInstance.beginGroundPos.y + arrange)
            {
                transform.position = new Vector3(transform.position.x, Singleton.getInstance.beginGroundPos.y + arrange, 0);
                moveWhere = false;
            }
            else if (transform.position.y <= Singleton.getInstance.beginGroundPos.y - arrange)
            {
                transform.position = new Vector3(transform.position.x, Singleton.getInstance.beginGroundPos.y - arrange, 0);
                moveWhere = true;
            }
        }
    }

    public void stopMoving()
    {
        isTarget = false;
    }

    public void checkTheGround(Vector3 pos)
    {
        // 플레이어와 접촉을 했는데 이 땅이 움직이고 있다면
        if (isTarget)
        {
            isTarget = false;

            if(myCode != 0)
            GameObject.Find("_GameManager").transform.SendMessage("deleyTwo");
            //if (Singleton.getInstance.beginGroundPos.y <= transform.position.y)
            //{
            //    Debug.Log("GameEnd");
            //}
            //else
            //{
            //    // 다시 벽 이동
            //}
        }
    }

    public void setCodeNum(int code)
    {
        myCode = code;
    }

    public int getCodeNum()
    {
        return myCode;
    }

    public void brokeMySelf()
    {
        brokeMe = true;
    }
}
