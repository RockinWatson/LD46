using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private Vector2 _scrapAmountRange = new Vector2(5f, 15f);

    private bool _isEnemyScrap = false;
    public void SetEnemyScrap() { _isEnemyScrap = true; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float scrapAmount = UnityEngine.Random.Range(_scrapAmountRange.x, _scrapAmountRange.y);
            collision.GetComponent<PlayerBehavior>().AddScrap(scrapAmount);
            
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        if(_isEnemyScrap)
        {
            Destroy(this.gameObject);
        }
    }
}
