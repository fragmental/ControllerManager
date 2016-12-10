ControllerManager
=================

ControllerManager is a cross platform multiplayer controller manager for Unity. ControllerManager maps up to 4 XBox 360/XBOne controllers. Currently works in macOS and Windows. Linux coming soon. 

Currently works for two xboxs controllers.

Unity input manager must include the axes as specified in the InputManager.asset file. 

Use
===

Simply inlcude the ControllerManager in your Unity project and accesss it via its static singleton instance. i.e. ControllerManager.Instance.GetRightTrigger(player2);
