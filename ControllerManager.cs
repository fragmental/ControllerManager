using UnityEngine;
using System.Collections;

public class ControllerManager{

	public enum OS
    {
        OSX,
        WIN,
        LIN
    };

	public bool Player1Keyboard {
		get{ return i.player1Keyboard;}
		set{ 
			i.player1Keyboard = value; 
            //if (i.player1Keyboard) {
            //    i.player2Keyboard = false;
            //}
		}
	}
	public bool Player2Keyboard {
		get{ return i.player2Keyboard;}
		set{
			i.player2Keyboard = value;
            //if (i.player2Keyboard) {
            //    i.player1Keyboard = false;
            //}
		}
	}
	public static ControllerManager Instance {
		get {
			if (i == null) {
				i = new ControllerManager ();
				if (Application.platform == RuntimePlatform.OSXEditor
					|| Application.platform == RuntimePlatform.OSXPlayer) {
					i.OS = OSX;
				} else (Application.platform == RuntimePlatform.OSXEditor
					|| Application.platform == RuntimePlatform.OSXPlayer){
					i.osx = false;
				}
			}
			return i;
		}
	}

	private static ControllerManager i;
	private bool player1Keyboard = true;
	private bool player2Keyboard;
	private bool osx;
    private bool isAxisInUse  = false;


	/// <summary>
	/// Gets the player movement on the x axis.
	/// </summary>
	/// <returns>The raw axis value for the x axis. -1, 0, or 1</returns>
	/// <param name="player">Player number ( 1 or 2 )</param>
	public float GetMovementX(int player)
	{
		if (player == 1) {

			if (i.Player1Keyboard) {
				//get player 1 keyboard movement
				//Debug.Log ("keyboard");
				return Input.GetAxisRaw ("Horizontal");
			} else {
				//get player 1 controller movement
				return Input.GetAxisRaw("x-axis1");
			}
		} else {
			if (i.Player2Keyboard) {
				//get player 2 keyboard movement
				//Debug.Log ("keyboard player2");
				return Input.GetAxisRaw ("Horizontal");
			} else {
				//get player 2 controller movement
                Debug.Log("controller player2");
                
				return Input.GetAxisRaw("x-axis2");
			}
		}
	}
	/// <summary>
	/// Gets the player movement on the y axis.
	/// </summary>
	/// <returns>The raw axis value for the y axis. -1, 0, or 1</returns>
	/// <param name="player">Player number ( 1 or 2 )</param>
	public float GetMovementY(int player)
	{
		if (player == 1) {
			if (i.Player1Keyboard) {
				//get player 1 keyboard movement
				return Input.GetAxisRaw ("Vertical");
			} else {
				//get player 1 controller movement
				return -Input.GetAxisRaw("y-axis1");
			}
		} else {
			if (i.Player2Keyboard) {
				//get player 2 keyboard movement
				return Input.GetAxisRaw ("Vertical");
			} else {
				//get player 2 controller movement
				return -Input.GetAxisRaw("y-axis2");
			}
		}
	}
	/// <summary>
	/// Gets the angle of direction from mouse or right joystick.
	/// </summary>
	/// <returns>The direction angle in radians.</returns>
	/// <param name="player">Player number.</param>
	/// <param name="screenLocation">Screen location of object.</param>
	public float GetDirectionAngle(int player, Vector3 screenLocation = new Vector3())
	{
		float angle;
		if (player == 1) {
			if (i.Player1Keyboard) {
				//get player 1 mouse direction angle
				Vector3 mousePos = Input.mousePosition;
				mousePos.x = mousePos.x - screenLocation.x;
				mousePos.y = mousePos.y - screenLocation.y;
				angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
				return angle;
			} else {
				//get player 1 controler direction angle
				angle = Mathf.Atan2(-i.GetRightJoystickY (player), 
					i.GetRightJoystickX (player)) * Mathf.Rad2Deg;
				return angle;
			}
		} else {
			if (i.Player2Keyboard) {
				//get player 2 mouse direction angle
				Vector3 mousePos = Input.mousePosition;
				mousePos.x = mousePos.x - screenLocation.x;
				mousePos.y = mousePos.y - screenLocation.y;
				angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
				return angle;
			} else {
				//get player 2 controller direction angle
				angle = Mathf.Atan2(-i.GetRightJoystickY (player), 
					i.GetRightJoystickX (player)) * Mathf.Rad2Deg;
				return angle;
			}
		}
	}
	/// <summary>
	/// Returns which, if any, button the player is selecting.
	/// left trigger == 1, left bumper == 2, right bumper == 3. If none of these buttons are selected, return 0
	/// </summary>
	/// <param name="player">Player number</param>
	public int Switch(int player){
		int value = 0;
		if ((player == 1 && i.Player1Keyboard)
			|| (player == 2 && i.Player2Keyboard)) {
			if (Input.GetKeyDown (KeyCode.Alpha1))
				value = 1;
			else if (Input.GetKeyDown (KeyCode.Alpha2))
				value = 2;
			else if (Input.GetKeyDown (KeyCode.Alpha3))
				value = 3;
			else
				value = 0;
		} else {
			if (i.GetLeftTrigger (player) > 0)
				value = 1;
			else if (i.GetLeftBumper (player))
				value = 2;
			else if (i.GetRightBumper (player))
				value = 3;
			else
				value = 0;
		}
		if (value != 0) {
			i.WeaponChangedCount++;
		}
		return value;
	}
    public bool Pause(int player)
    {
        if ((player == 1 && i.Player1Keyboard)
            || (player == 2 && i.Player2Keyboard))
            return Input.GetKeyDown(KeyCode.Escape);
        else
            return i.GetStartButton(player);
    }


	public bool Fire(int player){
		if ((player == 1 && i.Player1Keyboard)
		    || (player == 2 && i.Player2Keyboard))
			return Input.GetMouseButtonDown (0);
		else
			return GetRightTrigger (player) > 0;
	}

	public float GetRightJoystickX(int player)
	{
		if (osx) {
			return Input.GetAxis("3rd-axis" + player);
		} else {
			return Input.GetAxis("4th-axis" + player);
		}
	}
	public float GetRightJoystickY(int player)
	{
		if (osx) {
			return Input.GetAxis("4th-axis" + player);
		} else {
			return Input.GetAxis("5th-axis" + player);
		}
	}
	public float GetRightTrigger(int player)
	{
		if (osx) {
			return Input.GetAxis ("6th-axis" + player);
		} else {
			//return Input.GetAxis ("10th-axis" + player);
            if (Input.GetAxis("10th-axis" + player) != 0)
            {
                if (isAxisInUse == false)
                {
                    isAxisInUse = true;
                    return Input.GetAxis("10th-axis" + player);
                    
                }
            }
            if (Input.GetAxis("10th-axis" + player) == 0)
            {
                isAxisInUse = false;
            }
            return 0;
        }

	}

    public float GetLeftTrigger(int player)
    {
        if (osx) {
            return Input.GetAxis("5th-axis" + player);
        } else {
            return Input.GetAxis("9th-axis" + player);
        }
    }

	public bool GetRightBumper(int player)
	{
		if (osx)
		{
			if (player == 1)
				return Input.GetKeyDown(KeyCode.Joystick1Button14);
			else
				return Input.GetKeyDown(KeyCode.Joystick2Button14);
		} else {
			if (player == 1)
				return Input.GetKeyDown(KeyCode.Joystick1Button5);
			else 
				return Input.GetKeyDown(KeyCode.Joystick2Button5);
		}
	}

    public bool GetLeftBumper(int player)
    {
        if (osx)
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

	public bool GetXButton(int player){
		if (osx) {
			if (player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button18);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button18);
		} else {
			if(player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button2);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button2);
		}
	}

	public bool GetAButton(int player){
		if (osx) {
			if (player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button16);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button16);
		} else {
			if(player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button0);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button0);
		}
	}

    public bool GetStartButton(int player)
    {
        if (osx)
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
		if (osx) {
			if (player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button17);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button17);
		} else {
			if(player == 1)
				return Input.GetKeyDown (KeyCode.Joystick1Button1);
			else
				return Input.GetKeyDown (KeyCode.Joystick2Button1);
		}
	}
}