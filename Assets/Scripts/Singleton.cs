using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    public Vector3 beginGroundPos = Vector3.zero;
    public int count = 0;
    public int themeNum = 0;
    public bool gameEnd;

    public static Singleton getInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(Singleton)) as Singleton;
                if (instance == null)
                {
                    Debug.Log("No Instance");
                }
            }
            return instance;
        }
    }
}
