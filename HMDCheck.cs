using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class HMDCheck : UdonSharpBehaviour
{
    private VRCPlayerApi _player = Networking.LocalPlayer;
    
    //Set object references in scene Inspector window to drag and drop teleport locations for users
    public Transform tp_vr;
    public Transform tp_desktop;

    //Allow list of usernames which can bypass the HMDCheck
    public string[] _adminUsers;

    //Bypass check for HMDToggle
    public bool bypass = false;

    void Start() {
        //Only apply to local player -- not global or synced
        _player = Networking.LocalPlayer;
    }


    private bool CheckAllowListStatus()
    {
        bool allowlist = false;
        foreach (string _adminPlayers in _adminUsers)
        {
            if (Networking.LocalPlayer.displayName == _adminPlayers)
                allowlist = true;
        }

        return allowlist;
    }

//HMDCheck when player spawns into the collider field that this script is attached to
//Comment out this section and use the below "Interact()" section to use an interact object instead
   /* 
    public override void OnPlayerTriggerStay(VRCPlayerApi _player)
    {
        bool allowlist = CheckAllowListStatus();
        //Check if player is in VR or allow list bypass and then teleport to the corresponding location
        if (_player.IsUserInVR() || allowlist)
        {
            _player.TeleportTo(tp_vr.transform.position, tp_vr.transform.localRotation);
        }
        else
        {
            _player.TeleportTo(tp_desktop.transform.position, tp_desktop.transform.localRotation);
            _player.Immobilize(true);
        }
    }
    */

//HMDCheck when player interacts with an object that this script is attached to
//Comment out this section and use the above "OnPlayerTriggerStay()" section to use a collider field instead

public override void Interact()
    {
        if (!bypass)
        {
            bool allowlist = CheckAllowListStatus();

            //Check if local player an admin or staff, and if local player is in VR and teleport to vrYes if so, vrNo if not
            if (_player.IsUserInVR() || allowlist)
            {
                _player.TeleportTo(tp_vr.transform.position, tp_vr.transform.localRotation);
            }
            else
            {
                _player.TeleportTo(tp_desktop.transform.position, tp_desktop.transform.localRotation);
                _player.Immobilize(true);
            }
        }
        else
        {
            _player.TeleportTo(tp_vr.transform.position, tp_vr.transform.localRotation);
            _player.Immobilize(false);
        }
    }

    //HMDCheck players on respawn.

public override void OnPlayerRespawn(VRCPlayerApi _player)
    {
        if (!bypass)
        {
            bool allowlist = CheckAllowListStatus();
            //Check if local player an admin or staff, and if local player is in VR and teleport to vrYes if so, vrNo if not
            if (_player.IsUserInVR() || allowlist)
            {
                _player.TeleportTo(tp_vr.transform.position, tp_vr.transform.localRotation);
            }
            else
            {
                _player.TeleportTo(tp_desktop.transform.position, tp_desktop.transform.localRotation);
                _player.Immobilize(true);
            }
        }
        else
        {
            _player.TeleportTo(tp_vr.transform.position, tp_vr.transform.localRotation);
            _player.Immobilize(false);
        }
    }
    
}