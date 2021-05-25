using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    [SerializeField] Text displayText;
    private readonly string[] handStrings = new string[3] { "left", "right", "N/A" };
    public void GenerateRandomLog()
    {
        LoggingManager.LogObjectData(RandomString(), RandomString(), (LoggingManager.Type)UnityEngine.Random.Range(0, 8), handStrings[UnityEngine.Random.Range(0, handStrings.Length)]);
    }

    private string RandomString()
    {
        TextAsset txt = (TextAsset)Resources.Load("English");
        string[] dict = txt.text.Split("\n"[0]);
        return dict[UnityEngine.Random.Range(0, dict.Length)];
    }

    private void Update()
    {
        displayText.text = LoggingManager.GetDisplayText();
    }
}
