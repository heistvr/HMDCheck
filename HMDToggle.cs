using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class HMDToggle : UdonSharpBehaviour
{
    [UdonSynced]
    private bool syncedValue;
    private bool deserializing;
    public HMDCheck vrSpawn;
    private VRCPlayerApi localPlayer;

    public Color onColor = Color.green;
    public Color offColor = Color.red;
    public MeshRenderer thisMesh;
    public string[] _adminUsers;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
        deserializing = false;
        syncedValue = vrSpawn.bypass;
        thisMesh = GetComponent<MeshRenderer>();
        // init color for toggle
        thisMesh.material.color = (vrSpawn.bypass ? onColor : offColor);
        if (Networking.IsOwner(gameObject))
            RequestSerialization();
    }

    private bool CheckAllowListStatus()
    {
        bool allow = false;
        foreach (string _adminPlayers in _adminUsers)
        {
            if (localPlayer.displayName == _adminPlayers)
                allow = true;
        }

        return allow;
    }

    public override void Interact()
    {
        bool allowed = CheckAllowListStatus();
        if (allowed)
        {
            vrSpawn.bypass = !vrSpawn.bypass;
            thisMesh.material.color = (vrSpawn.bypass ? onColor : offColor);
            ToggleUpdate();
        }
    }

    public override void OnDeserialization()
    {
        deserializing = true;
        vrSpawn.bypass = syncedValue;
        thisMesh.material.color = (syncedValue ? onColor : offColor);
        deserializing = false;
    }

    public void ToggleUpdate()
    {
        if (deserializing)
            return;
        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(localPlayer, gameObject);

        syncedValue = vrSpawn.bypass;
        RequestSerialization();
    }
}