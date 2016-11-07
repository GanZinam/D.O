using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject Gm;

    [SerializeField]
    Text scoreText;

    public GameObject comboEff;
    public GameObject[] effect;
    public GameObject parent;
    public GameObject gameEndLayer;
    public GameObject reStartLayer;

    Rigidbody2D rigidbody2D;
    bool canMove = false;
    bool firstShow = false;

    public int detectCodeNum = 0;
    Vector3 beginWallPos;

    int combo = 0;

    public Sprite[] spr;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spr[Singleton.getInstance.themeNum];
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
            rigidbody2D.velocity += Vector2.right * Time.deltaTime / 1.5f;

        if (transform.position.y <= -5 && !Singleton.getInstance.gameEnd && firstShow)
        {
            gameEnd();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<GroundManager>().myCode > detectCodeNum)
        {
            if(Singleton.getInstance.themeNum == 1)
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(effect[i], scoreText.transform.position, Quaternion.identity) as GameObject;
                go.transform.parent = parent.transform;
            }

            detectCodeNum = coll.gameObject.GetComponent<GroundManager>().myCode;

            Singleton.getInstance.count++;
            scoreText.text = "x" + Singleton.getInstance.count;

            rigidbody2D.velocity = Vector2.one;

            // 이전에 밟았던 블록과 현재 밟은 블록의 높이가 같을때 콤보를 준다.
            if (beginWallPos.y == coll.transform.position.y)
            {
                if (combo < 5)
                    combo++;
                GameObject go = Instantiate(comboEff, transform.position, Quaternion.identity) as GameObject;
                go.transform.parent = parent.transform;
                go.GetComponentInChildren<Text>().text = "X" + Mathf.Pow(2, combo);
            }
            else
            {
                combo = 0;
            }

            firstShow = true;
        }
        else if (coll.gameObject.GetComponent<GroundManager>().myCode < detectCodeNum)
        {
            Singleton.getInstance.count--;
            scoreText.text = "x" + Singleton.getInstance.count;

            gameEnd();
        }
        else
        {
            beginWallPos = coll.transform.position;
        }

        coll.gameObject.SendMessage("checkTheGround", transform.position);
    }

    public void pausePlayerMove()
    {
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
        canMove = false;
    }

    public void moveAgainPlayer()
    {
        rigidbody2D.gravityScale = 1;
        rigidbody2D.velocity = Vector2.zero;
        canMove = true;
    }

    void gameEnd()
    {
        gameEndLayer.SetActive(true);
        gameEndLayer.transform.SendMessage("reSet");
        Singleton.getInstance.gameEnd = true;

        parent.SetActive(false);
        reStartLayer.SetActive(true);
    }

    public void reSetting()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(-7.8f, 2.305f, 0);
        detectCodeNum = 0;
        combo = 0;

        //if(GameObject.Find("scoreText").active)
        GameObject.Find("scoreText").GetComponent<Text>().text = "X" + combo;

        pausePlayerMove();
    }
}
