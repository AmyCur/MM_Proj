using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Collections.Generic;

public class TimerController : MonoBehaviour
{
    float targetTime = 0f;
    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    List<char> StringToChars(string s)
    {
        List<char> chars = new();
        foreach (char c in s)
        {
            chars.Add(c);
        }
        return chars;

    }

    string GetTimerText()
    {
        string tt = Math.Round(targetTime, 2).ToString();
        List<char> ntt = StringToChars(tt);

        int i = 0;
        if (tt.Split(".").Length == 2)
        {

            while (tt.Split(".")[1].Length < 2)
            {
                ntt.Add('0');
                if (i > 1000)
                {
                    break;
                }
                i++;
            }

        }

        float f = float.Parse(new string(ntt.ToArray()));
        return (Math.Round(Math.Round(f, 2), 2)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime += Time.deltaTime;
        text.text = GetTimerText();
    }
}
