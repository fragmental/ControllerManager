using UnityEngine;
using System.Collections;

public class ControllerManager{

    public enum OS
    {
        OSX,
        WIN,
        LIN
    };

    public static ControllerManager Instance 
    {
        get 
        {
            if (i == null) 
            {
                i = new ControllerManager ();
                if (Application.platform == RuntimePlatform.OSXEditor
                    || Application.platform == RuntimePlatform.OSXPlayer) 
                {
                    i.os = OS.OSX;
                } 
                else  if (Application.platform == RuntimePlatform.WindowsEditor
                    || Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    i.os = OS.WIN;
                } 
                else  if (Application.platform == RuntimePlatform.LinuxPlayer)
                {
                    i.os = OS.LIN;
                } 
                else 
                {
                    throw new Exception(); // Unsupported platform
                }
            }
            return i;
        }
    }
    private OS os;
    private static ControllerManager i;


    /// <summary>
    /// Gets the left joystick x axis.
    /// </summary>
    /// <returns>The axis value for the x axis. -1 to 1</returns>
    /// <param name="player">Player number ( 1 or 2 )</param>
    public float GetLeftJoystickX(int player)
    {
        return Input.GetAxis("x-axis" + player);
    }
    /// <summary>
    /// Gets the left joystick y axis.
    /// </summary>
    /// <returns>The axis value for the y axis. -1 to 1</returns>
    /// <param name="player">Player number ( 1 or 2 )</param>
    public float GetLeftJoystickY(int player)
    {
        return Input.GetAxis("y-axis" + player);
    }

    public float GetRightJoystickX(int player)
    {
        if (os == OS.OSX) 
        {
            return Input.GetAxis("3rd-axis" + player);
        } 
        else 
        {
            return Input.GetAxis("4th-axis" + player);
        }
    }
    public float GetRightJoystickY(int player)
    {
        if (os == OS.OSX) 
        {
            return Input.GetAxis("4th-axis" + player);
        } 
        else 
        {
            return Input.GetAxis("5th-axis" + player);
        }
    }

    /// <summary>
    /// Gets the right trigger axis as a value from 0 (not engaged) to 1 (fully engaged).
    /// </summary>
    /// <returns>The axis value for the right trigger axis. 0 ro 1</returns>
    /// <param name="player">Player number ( 1 or 2 )</param>
    public float GetRightTrigger(int player)
    {
        if (os == OS.OSX) 
        {
            // Convert -1 - 1 range to 0 - 1 range
            float value = Input.GetAxis ("6th-axis" + player);
            return convertRange(-1f, 1f, 0f, 1f, value);
        } 
        else 
        {
            return Input.GetAxis ("10th-axis" + player);
        }

    }

    public float GetLeftTrigger(int player)
    {
        if (os == OS.OSX) 
        {
            return Input.GetAxis("5th-axis" + player);
        } 
        else 
        {
            return Input.GetAxis("9th-axis" + player);
        }
    }

    public bool GetRightBumper(int player)
    {
        if (os == OS.OSX)
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button14);
            else
                return Input.GetKeyDown(KeyCode.Joystick2Button14);
        } 
        else 
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button5);
            else 
                return Input.GetKeyDown(KeyCode.Joystick2Button5);
        }
    }

    public bool GetLeftBumper(int player)
    {
        if (os == OS.OSX)
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button13);
            else
                return Input.GetKeyDown(KeyCode.Joystick2Button13);
        }
        else
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button4);
            else
                return Input.GetKeyDown(KeyCode.Joystick2Button4);
        }
    }

    public bool GetXButton(int player)
    {
        if (os == OS.OSX) 
        {
            if (player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button18);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button18);
        } 
        else 
        {
            if(player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button2);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button2);
        }
    }

    public bool GetAButton(int player)
    {
        if (os == OS.OSX) 
        {
            if (player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button16);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button16);
        } 
        else 
        {
            if(player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button0);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button0);
        }
    }

    public bool GetStartButton(int player)
    {
        if (os == OS.OSX)
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button9);
            else
                return Input.GetKeyDown(KeyCode.Joystick2Button9);
        }
        else
        {
            if (player == 1)
                return Input.GetKeyDown(KeyCode.Joystick1Button7);
            else
                return Input.GetKeyDown(KeyCode.Joystick2Button7);
        }

    }

    public bool GetBButton(int player){
        if (os == OS.OSX) 
        {
            if (player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button17);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button17);
        } else 
        {
            if(player == 1)
                return Input.GetKeyDown (KeyCode.Joystick1Button1);
            else
                return Input.GetKeyDown (KeyCode.Joystick2Button1);
        }
    }
    private float convertRange(float originalStart, float originalEnd,
                               float newStart, float newEnd,
                               float value)
    {

        float scale = (newEnd - newStart) / (originalEnd - originalStart);
        return (newStart + ((value - originalStart) * scale));
    }
}