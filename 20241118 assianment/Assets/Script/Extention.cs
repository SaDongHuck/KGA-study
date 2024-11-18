using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions
{
    public static string[] Split(this string str, char separator)
    {
        if(string.IsNullOrEmpty(str))
        {
            return new string[0];
        }

        return str.Split(new char[] { separator }, System.StringSplitOptions.None);
    }
}

public class Extention : MonoBehaviour
{
    public void Start()
    {
        string input = "¾È³ç ÇÏ¼¼¿ä";
        char separator = ' ';

        string[] result = input.Split(separator);  

        foreach(string sr in result)
        {
            print(sr);
        }
    }
}
