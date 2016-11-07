using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MoneyText : MonoBehaviour
{
    void Start()
    {
        int money = System.Convert.ToInt32(readStringFromFile(pathForDocuments("assets/Data/money.txt")));
        GetComponent<UnityEngine.UI.Text>().text = "" + money;
    }

    void writeStringToFile(string str, string fileName)
    {
        string path = pathForDocuments(fileName);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);

        sw.Close();
        file.Close();
    }

    string readStringFromFile(string fileName)
    {
        string path = pathForDocuments(fileName);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            //str = sr.ReadLine();
            str = sr.ReadToEnd();

            char sp = ',';
            string[] spString = str.Split(sp);

            for (int i = 0; i < spString.Length - 1; i++)
            {
                float numFloat = System.Convert.ToSingle(spString[i]);
                Debug.Log(numFloat);
            }

            sr.Close();
            file.Close();

            return str;
        }
        else
        {
            return null;
        }
    }

    string pathForDocuments(string fileName)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), fileName);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            //path = "jar:file://" + Application.dataPath + "!/";
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
        else
        {
            string path = Application.dataPath;
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
    }
}
