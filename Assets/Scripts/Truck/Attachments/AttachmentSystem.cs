using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

public class AttachmentSystem : MonoBehaviour
{
    private List<AttachmentSocket> _armorSockets = new List<AttachmentSocket>();
    //private List<Armor> _armors = new List<Armor>();
    [SerializeField] private GameObject _armorPrefab = null;

    private List<AttachmentSocket> _turretSockets = new List<AttachmentSocket>();
    //private List<Turret> _turrets = new List<Turret>();
    [SerializeField] private GameObject _turretPrefab = null;

    private List<AttachmentSocket> _trailerSockets = new List<AttachmentSocket>();
    //private List<Trailer> _trailer = new List<Trailer>();
    [SerializeField] private GameObject _trailerPrefab = null;

    private void Awake()
    {
        AttachmentSocket[] sockets = this.GetComponentsInChildren<AttachmentSocket>();
        foreach(var socket in sockets)
        {
            switch(socket.GetSocketType())
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
                case AttachmentSocket.SocketType.Trailer:
                    {
                        _trailerSockets.Add(socket);
                    }
                    break;
                default:
                    Debug.LogWarning("Unrecognized Socket Type");
                    break;
            }
        }
    }

    public float TakeDamage(float damage)
    {
        //@TODO: Figure out how to take daamge around the system with more going to armor, less to turrets, etc
        //  ... currently thinking like 70% going to armor first (if they exist), then 30% to turrets, etc
        //  ... if no armor exists, 100% goes to turrets
        //  ... any damage not taken by the system gets returned and would be applied to the tank

        //return damage;

        return DamageAttachments_Random(damage);
    }

    //@TEMP: Random until positional upgrade and UI works
    public void UpgradeTurret_Random()
    {
        UpgradeSockets_Random<Turret>(_turretSockets, _turretPrefab);
    }

    public void UpgradeArmor_Random()
    {
        UpgradeSockets_Random<Armor>(_armorSockets, _armorPrefab);
    }

    private void UpgradeSockets_Random<T>(List<AttachmentSocket> sockets, GameObject attachmentPrefab) where T : Attachment
    {
        List<AttachmentSocket> vacantSockets = GetVacantSockets(sockets);
        if (vacantSockets != null && vacantSockets.Count > 0)
        {
            T attachment = Instantiate(attachmentPrefab).GetComponent<T>();
            int index = UnityEngine.Random.Range(0, vacantSockets.Count);
            SetAttachmentToSocket(vacantSockets[index], attachment);
        }
    }

    private void SetAttachmentToSocket(AttachmentSocket socket, Attachment attachment)
    {
        socket.SetAttachment(attachment);
        //@TODO: Might need to subscribe to death events etc?

        //Play build audio
        if (AudioController.build.isPlaying == false)
        {
            AudioController.build.Play();
        }
    }

    private List<AttachmentSocket> GetVacantSockets(List<AttachmentSocket> sockets)
    {
        return sockets.Where((t) => !t.HasAttachment()).ToList();
    }

    public float DamageAttachments_Random(float damage)
    {
        float armorScale = 0.7f;
        float turretScale = 0.3f;
        bool hasArmor = HasFunctioningArmorSockets();
        bool hasTurrets = HasFunctioningTurretSockets();
        if (hasArmor && !hasTurrets)
        {
            armorScale = 1f;
            turretScale = 0f;
        }
        else if (!hasArmor && hasTurrets)
        {
            armorScale = 0f;
            turretScale = 1f;
        }

        float remainingDamage = DamageArmor_Random(damage * armorScale);

        remainingDamage = DamageTurret_Random(damage * turretScale + remainingDamage);

        return remainingDamage;
    }

    public float DamageTurret_Random(float damage)
    {
        return DamageSockets_Random<Turret>(_turretSockets, damage);
    }

    public float DamageArmor_Random(float damage)
    {
        return DamageSockets_Random<Armor>(_armorSockets, damage);
    }

    private float DamageSockets_Random<T>(List<AttachmentSocket> sockets, float damage) where T : Attachment
    {
        List<AttachmentSocket> aliveSockets = GetFunctioningSockets(sockets);
        if (aliveSockets != null && aliveSockets.Count > 0)
        {
            var res = from item in aliveSockets orderby Guid.NewGuid() select item;
            foreach (AttachmentSocket socket in res)
            {
                //@TODO: Repair damage and track how much has been repaired.
                damage = socket.GetAttachment().TakeDamage(damage);

                //Play truck and attachment damage audio
                AudioController.truckDamage.Play();
            }
        }

        return damage;
    }

    private List<AttachmentSocket> GetFunctioningSockets(List<AttachmentSocket> sockets)
    {
        return sockets.Where((t) => (t.HasAttachment() && t.GetAttachment().IsAlive())).ToList();
    }

    private bool HasFunctioningTurretSockets()
    {
        return _turretSockets.Where((t) => (t.HasAttachment() && t.GetAttachment().IsAlive())).ToList().Count > 0;
    }

    private bool HasFunctioningArmorSockets()
    {
        return _armorSockets.Where((t) => (t.HasAttachment() && t.GetAttachment().IsAlive())).ToList().Count > 0;
    }

    public float RepairAttachments_Random(float damage)
    {
        //@TODO: Maybe attempt to repair turrets first, then armor?
        float adjustedDamage = RepairTurret_Random(damage);

        if(adjustedDamage > 0f)
        {
            adjustedDamage = RepairArmor_Random(adjustedDamage);
        }

        return adjustedDamage;
    }

    public float RepairTurret_Random(float damage)
    {
        return RepairSockets_Random<Turret>(_turretSockets, damage);
    }

    public float RepairArmor_Random(float damage)
    {
        return RepairSockets_Random<Armor>(_armorSockets, damage);
    }

    private float RepairSockets_Random<T>(List<AttachmentSocket> sockets, float repairAmount) where T : Attachment
    {
        List<AttachmentSocket> damagedTurretSockets = GetDamagedSockets(sockets);
        if (damagedTurretSockets != null && damagedTurretSockets.Count > 0)
        {
            var res = from item in damagedTurretSockets orderby Guid.NewGuid() select item;
            foreach(AttachmentSocket socket in res)
            {
                //@TODO: Repair damage and track how much has been repaired.
                repairAmount = socket.GetAttachment().RepairDamage(repairAmount);

                //Play repair audio
                if (AudioController.repair.isPlaying == false)
                {
                    AudioController.repair.Play();
                }
            }
        }

        return repairAmount;
    }

    private List<AttachmentSocket> GetDamagedSockets(List<AttachmentSocket> sockets)
    {
        return sockets.Where((t) => (t.HasAttachment() && t.GetAttachment().HasDamage())).ToList();
    }

    public void UpgradeTrailer()
    {
        AttachmentSocket trailerSocket = GetNextVacantTrailerSocket();
        if (trailerSocket)
        {
            Trailer trailer = Instantiate(_trailerPrefab).GetComponent<Trailer>();
            SetAttachmentToSocket(trailerSocket, trailer);

            //@TODO: Add all the trailer's new sockets
            foreach(AttachmentSocket turretSocket in trailer.GetTurretSockets())
            {
                _turretSockets.Add(turretSocket);
            }
            foreach(AttachmentSocket armorSocket in trailer.GetArmorSockets())
            {
                _armorSockets.Add(armorSocket);
            }
        }
    }

    private AttachmentSocket GetNextVacantTrailerSocket()
    {
        foreach(AttachmentSocket socket in _trailerSockets)
        {
            if(!socket.HasAttachment())
            {
                return socket;
            }
        }
        return null;
    }
}
