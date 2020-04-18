using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    public float bgSpeed;
    public Renderer bgRend;

    private void FixedUpdate()
    {
        bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.fixedDeltaTime, 0f);
    }
}
