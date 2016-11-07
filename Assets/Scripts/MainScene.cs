using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public GameObject gameStartLayer;
    public Animator title;
    public Animator playBT;
    public Animator optionBT;
    public Animator themeBT;
    public GameObject character;

    bool canTouch = false;

    public Texture[] texture;
    public Sprite[] titleTexture;
    public Sprite[] playTexture;
    public Sprite[] optionTexture;
    public Sprite[] themeTexture;
    public GameObject backgroundMaterial;
    public GameObject creditOBJ;
    public AudioSource audio;
    bool isCreditOn = false;

    public AudioClip[] audioCl;

    void Start()
    {
        backgroundMaterial.GetComponent<Renderer>().material.mainTexture = texture[Singleton.getInstance.themeNum];
        title.GetComponent<Image>().sprite = titleTexture[Singleton.getInstance.themeNum];
        playBT.GetComponent<Image>().sprite = playTexture[Singleton.getInstance.themeNum];
        optionBT.GetComponent<Image>().sprite = optionTexture[Singleton.getInstance.themeNum];
        themeBT.GetComponent<Image>().sprite = themeTexture[Singleton.getInstance.themeNum];

        audio.Stop();
        audio.clip = audioCl[Singleton.getInstance.themeNum];
        audio.Play();

        StartCoroutine(delay());
    }

    void Update()
    {
        if (isCreditOn && Input.GetKeyDown(KeyCode.Escape))
        {
            isCreditOn = false;
            creditOBJ.SetActive(false);
        }
    }
    public void gameStartButton()
    {
        if (!playBT.GetBool("Hide") && canTouch)
        {
            gameStartLayer.SetActive(true);

            title.SetBool("Hide", true);
            playBT.SetBool("Hide", true);
            optionBT.SetBool("Hide", true);
            themeBT.SetBool("Hide", true);

            StartCoroutine(delayOne());
        }
    }

    public void gameThemeButten()
    {
        if (!themeBT.GetBool("Hide") && canTouch)
        {
            DontDestroyOnLoad(Singleton.getInstance.gameObject);
            Application.LoadLevel("ThemeScene");
        }
    }
    public void gameOptionButten()
    {
        if (!optionBT.GetBool("Hide") && canTouch)
        {
            creditOBJ.SetActive(true);
            isCreditOn = true;
        }
    }

    IEnumerator delayOne()
    {
        yield return new WaitForSeconds(2f);

        character.gameObject.SendMessage("moveAgainPlayer");
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);

        canTouch = true;
    }
}
