using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delegate : MonoBehaviour
{
    public InputField InputField1;
    public InputField InputField2;

    public Button plusbutton, minusbutton, multiplebutton, divebutton, equalsbutton;

    public Action<float, float> currentoperation;

    public void Start()
    {
        plusbutton.onClick.AddListener(() => Setoperation((a, b) => Display(a + b)));
        minusbutton.onClick.AddListener(() => Setoperation((a, b) => Display(a - b)));
        multiplebutton.onClick.AddListener(() => Setoperation((a, b) => Display(a * b)));
        divebutton.onClick.AddListener(() => Setoperation((a, b) => Display(a / b)));
        equalsbutton.onClick.AddListener(Calc);

    }

    public void Setoperation(Action<float, float> operation)
    {
        currentoperation = operation;
        print("Ready");
    }

    public void Calc()
    {
        if(currentoperation == null)
        {
            print("Select op");
        }

        if (float.TryParse(InputField1.text, out float number1) && float.TryParse(InputField2.text, out float number2))
        {
            currentoperation.Invoke(number1, number2); // delegate ½ÇÇà
        }
        else
        {
            print("Error");
        }
    }

    public void Display(float Result)
    {
        print(Result);
    }

}
