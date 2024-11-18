using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public static void printLines(params string[] mylist)
    {
        foreach (string line in mylist)
        {
            print(line);
        }
    }

    public void Start()
    {
        printLines("HI", "My name is", "Dong Huck Sa");
    }
}
