using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private float _scrapAmount = 9f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerBehavior>().AddScrap(_scrapAmount);

            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
