using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    static private Truck _instance = null;
    static public Truck Get() { return _instance; }

    private AttachmentSystem _attachmentSystem = null;

    private bool truckExploded = false;

    //@TODO: This is gonna eventually be some separate data structure with all the attachments, their health, etc.
    private float _health = 100f;
    public bool IsAlive() { return _health > 0f; }
    public bool IsDead() { return _health == 0f; }

    [SerializeField] private Text _healthText = null;

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogWarning("Should only be one Tank instance.");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        _attachmentSystem = this.GetComponentInChildren<AttachmentSystem>();
    }

    private void Start()
    {
        UpdateHealthText();
    }

    public void TakeDamage(float damage)
    {
        float adjustedDamage = _attachmentSystem.TakeDamage(damage);

        _health -= adjustedDamage;
        UpdateHealthText();

        //Play truck damage audio
        AudioController.truckDamage.Play();

        if(_health <= 0f)
        {
            _health = 0f;

            Die();
        }
    }

    private void Die()
    {
        //@TODO: Fire off some more major death FX / anim and trigger the game ending...

        //Play explosion audio
        if (AudioController.truckExplode.isPlaying == false && truckExploded == false)
        {
            AudioController.truckDamage.volume = 0f;
            AudioController.truckExplode.Play();
            truckExploded = true;
        }
    }

    private void Update()
    {
        //@TODO: Update how the thing will move around within an area of motion in the middle of the playfield to make it feel a bit more lively...

        DEBUG_Input();
    }

    //@TEMP/@NOTE: This is temp until we figure out position indicating what to upgrade and UI, etc
    public void DoRangomUpgrade()
    {
        //@TEMP: Randomly pick armor or turret to upgrade...
        AttachmentSocket.SocketType socketType = (AttachmentSocket.SocketType)Random.Range(0, (int)AttachmentSocket.SocketType.COUNT);
        switch(socketType)
        {
            case AttachmentSocket.SocketType.Armor:
                {
                    _attachmentSystem.UpgradeArmor_Random();
                }
                break;
            case AttachmentSocket.SocketType.Turret:
                {
                    _attachmentSystem.UpgradeTurret_Random();
                }
                break;
            case AttachmentSocket.SocketType.Trailer:
                {
                    _attachmentSystem.UpgradeTrailer();
                }
                break;
            default:
                Debug.LogWarning("Unrecognized Attachment type.");
                break;
        }
    }

    //@TEMP/@NOTE: This is temp until we figure out position indicating what to repair, etc
    public void DoRandomRepair(float damage)
    {
        //@TODO: Do repair.
        _attachmentSystem.RepairAttachments_Random(damage);
    }

    private void DEBUG_Input()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            DEBUG_RandomDamage();
        }
    }

    private void DEBUG_RandomDamage()
    {
        float damage = Random.Range(5f, 25f);
        damage = _attachmentSystem.TakeDamage(damage);

        TakeDamage(damage);

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        _healthText.text = _health.ToString("0");

        Color healthColor = HealthColorUtility.GetHealthColor(_health);
        //_healthText.CrossFadeColor(healthColor, 0.5f, false, false);
        _healthText.color = healthColor;
    }

    public List<AttachmentSocket> GetFunctioningTrailerSockets()
    {
        return _attachmentSystem.GetFunctioningTrailerSockets();
    }
}
