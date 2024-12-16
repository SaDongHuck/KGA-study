using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScreenPointMessage
{
    public int id;
    public float x;
    public float y;
    public string type;
    public ScreenPointMessage(int id, float x, float y, string type = "SCREEN_POINT")
    {
        this.id = id;
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
