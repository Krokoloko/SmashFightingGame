using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputScript : MonoBehaviour
{
    [SerializeField]
    private string[] Buttons;
    [SerializeField]
    private string[] Axises;

    public enum Function_Type {Down, Up, None };

    private Dictionary<string, Action> OnButtonDown;
    private Dictionary<string, Action> OnButton;
    private Dictionary<string, Action> OnButtonUp;

    private Dictionary<string, Action<float>> OnGetAxis;
    
    // Start is called before the first frame update
    void Start()
    {
        OnButtonDown = new Dictionary<string, Action> { };
        OnButton = new Dictionary<string, Action> { };
        OnButtonUp = new Dictionary<string, Action> { };

        OnGetAxis = new Dictionary<string, Action<float>> { };

        foreach(string button in Buttons)
        {
            OnButtonDown.Add(button, delegate () { });
            OnButton.Add(button, delegate () { });
            OnButtonUp.Add(button, delegate () { });
        }
    }

    public void DeleteFunctionButton(string name, Function_Type type, Action function)
    {
        foreach (string button in Buttons)
        {
            if (button == name)
            {
                switch (type)
                {
                    case Function_Type.None:
                        OnButton[name] -= function;
                        break;
                    case Function_Type.Down:
                        OnButtonDown[name] -= function;
                        break;
                    case Function_Type.Up:
                        OnButtonUp[name] -= function;
                        break;
                }
            }
        }
    }

    public void DeleteFunctionAxis(string name, Action<float> function)
    {
        foreach (string axis in Axises)
        {
            if (name == axis)
            {
                OnGetAxis[name] -= function;
            }
        }
    }

    public void AddFunctionButton(string name, Function_Type type, Action function)
    {
        foreach (string button in Buttons)
        {
            if(button == name)
            {
                switch (type)
                {
                    case Function_Type.None:
                        OnButton[name] += function;
                        break;
                    case Function_Type.Down:
                        OnButtonDown[name] += function;
                        break;
                    case Function_Type.Up:
                        OnButtonUp[name] += function;
                        break;
                }
            }
        }
    }

    public void AddFunctionAxis(string name, Action<float> function)
    {
        foreach (string axis in Axises)
        {
            if (axis == name)
            {
                OnGetAxis[axis] += function;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string axis in Axises)
        {
            OnGetAxis[axis](Input.GetAxis(axis));
        }

        foreach (string button in Buttons)
        {
            if (Input.GetButtonDown(button))
            {
                OnButtonDown[button]();
            }
            if (Input.GetButton(button))
            {
                OnButton[button]();
            }
            if (Input.GetButtonUp(button))
            {
                OnButtonUp[button]();
            }
        }
    }
}
