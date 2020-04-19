using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSocket : MonoBehaviour
{
    public enum SocketType
    {
        Turret = 0,
        Armor = 1,
    }
    [SerializeField]
    private SocketType _socketType = SocketType.Turret;
    public SocketType GetSocketType() { return _socketType; }

    private Attachment _attachment = null;
    public Attachment GetAttachment() { return _attachment; }
    public bool HasAttachment() { return _attachment; }
    public void SetAttachment(Attachment attachment)
    {
        _attachment = attachment;
        _attachment.transform.SetParent(this.transform, false);
    }
}
