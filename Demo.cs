using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Right Buttons
		if (ControllerManager.Instance.GetAButton (1)) 
		{
			Debug.Log ("A Button Pressed.");
		}
		if (ControllerManager.Instance.GetBButton(1)) 
		{
			Debug.Log ("B Button Pressed.");
		}
		if (ControllerManager.Instance.GetXButton(1)) 
		{
			Debug.Log ("X Button Pressed.");
		}
		if (ControllerManager.Instance.GetYButton(1)) 
		{
			Debug.Log ("Y Button Pressed.");
		}

		// Middle Buttons
		if (ControllerManager.Instance.GetStartButton(1)) 
		{
			Debug.Log ("Start Button Pressed.");
		}
		if (ControllerManager.Instance.GetBackButton(1)) 
		{
			Debug.Log ("Back Button Pressed.");
		}

		// D-Pad Buttons
		if (ControllerManager.Instance.GetDPadUp(1)) 
		{
			Debug.Log ("Up D-Pad Button Pressed.");
		}
		if (ControllerManager.Instance.GetDPadDown(1)) 
		{
			Debug.Log ("Down D-Pad Button Pressed.");
		}
		if (ControllerManager.Instance.GetDPadRight(1)) 
		{
			Debug.Log ("Right D-Pad Button Pressed.");
		}
		if (ControllerManager.Instance.GetDPadLeft(1)) 
		{
			Debug.Log ("Left D-Pad Button Pressed.");
		}

		// Bumpers and Triggers
		if (ControllerManager.Instance.GetLeftBumper(1)) 
		{
			Debug.Log ("Left Bumper Pressed.");
		}
		if (ControllerManager.Instance.GetRightBumper(1)) 
		{
			Debug.Log ("Right Bumper Pressed.");
		}
		if (ControllerManager.Instance.GetLeftTrigger(1) > 0) 
		{
			Debug.Log ("Left Trigger Pressed.");
		}
		if (ControllerManager.Instance.GetRightTrigger(1) > 0) 
		{
			Debug.Log ("Right Trigger Pressed.");
		}

		// Josystick Clicks
		if (ControllerManager.Instance.GetRightJoystickClick(1)) 
		{
			Debug.Log ("Right Joystick Clicked.");
		}
		if (ControllerManager.Instance.GetLeftJoystickClick(1)) 
		{
			Debug.Log ("Left Joystick Clicked.");
		}
	
	}
}
