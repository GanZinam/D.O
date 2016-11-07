using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThemeOpacity : MonoBehaviour {
    public Image button;

    void Start ()
    {
        Color temp = button.color;
        temp.a = 0.5f;
        button.color = temp;
    }
}