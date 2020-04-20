using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    protected GameObject _target = null;

    virtual public void SetupTarget(GameObject source, GameObject target)
    {
        this.transform.position = source.transform.position;

        _target = target;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
