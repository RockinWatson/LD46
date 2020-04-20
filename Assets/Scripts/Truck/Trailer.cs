using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : Attachment
{
    private List<AttachmentSocket> _armorSockets = new List<AttachmentSocket>();
    public List<AttachmentSocket> GetArmorSockets() { return _armorSockets; }

    private List<AttachmentSocket> _turretSockets = new List<AttachmentSocket>();
    public List<AttachmentSocket> GetTurretSockets() { return _turretSockets; }

    private void Awake()
    {
        AttachmentSocket[] sockets = this.GetComponentsInChildren<AttachmentSocket>();
        foreach (var socket in sockets)
        {
            switch (socket.GetSocketType())
            {
                case AttachmentSocket.SocketType.Armor:
                    {
                        _armorSockets.Add(socket);
                    }
                    break;
                case AttachmentSocket.SocketType.Turret:
                    {
                        _turretSockets.Add(socket);
                    }
                    break;
                default:
                    Debug.LogWarning("Unrecognized Socket Type");
                    break;
            }
        }
    }
}
