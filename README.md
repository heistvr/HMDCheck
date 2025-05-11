# HMD Check

Determine if a user in VRChat is using a Head Mounted Display (HMD).

# How to

Drag the HMDCHECK_PREFAB into the scene.
The prefab contains 4 objects:
1. VRYES - a teleport location for users in VR.
2. VRNO - a teleport location for users not in VR.
3. HMDCheck - a button which checks the user to determine if they are in VR.
4. HMDToggle - a button which disables/enables HMDCheck.

Use the HMDCheck button as an entrance toggle for the world, this object can be changed from a default cube to whatever is preferred.
Select the HMDCheck button and check the inspector script, expand the Admin Users to add allow listed users who can skip the check.

If a spawn-based check is preferred over the button, open the HMDCheck script and follow the instructions to disable the section which 
uses the button and enable the section which uses a collider check. This requires an understanding of how box collider triggers work.

Use the HMDToggle button to enable or disable the check.
Select the HMDToggle button and check the inspector script, expand the Admin Users to add allow listed users who can use this button.

In the VRNO teleport location, it is critical that text is provided to the user teleported there explaining why they were teleported there,
and how they can remedy the situation by enabling their HMD.

# Testing

When using CyanEmu to test ensure that the allow listed username is entered into the Local Player Name field within CyanEmu Settings.
