using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    private bool _right() { return (Input.GetKeyDown(KeyCode.RightArrow)); }
    private bool _left() { return (Input.GetKeyDown(KeyCode.LeftArrow)); }

    private Vector3 cardPos;
    private Vector3 cardHidePos;
    private GameObject card1;
    private GameObject card2a;
    private GameObject card2b;
    private GameObject card3;
    private GameObject card4;
    private GameObject card5;
    private GameObject card6;
    private GameObject card7;
    private GameObject card8;
    private GameObject card9;
    private GameObject card10;
    private GameObject card11;
    private GameObject card12;
    private GameObject card13;
    private GameObject card14;
    private GameObject card15;
    private GameObject card16;
    private GameObject card17;
    private GameObject card18;
    private GameObject card19;
    private GameObject card20;


    // Start is called before the first frame update
    void Awake()
    {
        cardPos = new Vector3(0, 0, 0);
        cardHidePos = new Vector3(0, 50, 0);

        card1 = GameObject.Find("1");
        card2a = GameObject.Find("2a");
        card2b = GameObject.Find("2b");
        card3 = GameObject.Find("3");
        card4 = GameObject.Find("4");
        card5 = GameObject.Find("5");
        card6 = GameObject.Find("6");
        card7 = GameObject.Find("7");
        card8 = GameObject.Find("8");
        card9 = GameObject.Find("9");
        card10 = GameObject.Find("10");
        card11 = GameObject.Find("11");
        card12 = GameObject.Find("12");
        card13 = GameObject.Find("13");
        card14 = GameObject.Find("14");
        card15 = GameObject.Find("15");
        card16 = GameObject.Find("16");
        card17 = GameObject.Find("17");
        card18 = GameObject.Find("18");
        card19 = GameObject.Find("19");
        card20 = GameObject.Find("20");

        card1.transform.position = cardPos;


    }

    // Update is called once per frame
    void Update()
    {
        CardSelect();
    }

    private void CardSelect()
    {
        if (_right())
        {
            if (card2a.transform.position.x != cardPos.x)
            {
                card2a.transform.position = cardPos;
                card1.transform.position = cardHidePos;
            }
            else if (card2b.transform.position.x != cardPos.x)
            {
                card2b.transform.position = cardPos;
                card2a.transform.position = cardHidePos;
            }
            else if (card3.transform.position.x != cardPos.x)
            {
                card3.transform.position = cardPos;
                card2b.transform.position = cardHidePos;
            }
            else if (card4.transform.position.x != cardPos.x)
            {
                card4.transform.position = cardPos;
                card3.transform.position = cardHidePos;
            }
            else if (card5.transform.position.x != cardPos.x)
            {
                card5.transform.position = cardPos;
                card4.transform.position = cardHidePos;
            }
            else if (card6.transform.position.x != cardPos.x)
            {
                card6.transform.position = cardPos;
                card5.transform.position = cardHidePos;
            }
            else if (card7.transform.position.x != cardPos.x)
            {
                card7.transform.position = cardPos;
                card6.transform.position = cardHidePos;
            }
            else if (card8.transform.position.x != cardPos.x)
            {
                card8.transform.position = cardPos;
                card7.transform.position = cardHidePos;
            }
            else if (card9.transform.position.x != cardPos.x)
            {
                card9.transform.position = cardPos;
                card8.transform.position = cardHidePos;
            }
            else if (card10.transform.position.x != cardPos.x)
            {
                card10.transform.position = cardPos;
                card9.transform.position = cardHidePos;
            }
            else if (card11.transform.position.x != cardPos.x)
            {
                card11.transform.position = cardPos;
                card10.transform.position = cardHidePos;
            }
            else if (card12.transform.position.x != cardPos.x)
            {
                card12.transform.position = cardPos;
                card11.transform.position = cardHidePos;
            }
            else if (card13.transform.position.x != cardPos.x)
            {
                card13.transform.position = cardPos;
                card12.transform.position = cardHidePos;
            }
            else if (card14.transform.position.x != cardPos.x)
            {
                card14.transform.position = cardPos;
                card13.transform.position = cardHidePos;
            }
            else if (card15.transform.position.x != cardPos.x)
            {
                card15.transform.position = cardPos;
                card14.transform.position = cardHidePos;
            }
            else if (card16.transform.position.x != cardPos.x)
            {
                card16.transform.position = cardPos;
                card15.transform.position = cardHidePos;
            }
            else if (card17.transform.position.x != cardPos.x)
            {
                card17.transform.position = cardPos;
                card16.transform.position = cardHidePos;
            }
            else if (card18.transform.position.x != cardPos.x)
            {
                card18.transform.position = cardPos;
                card17.transform.position = cardHidePos;
            }
            else if (card19.transform.position.x != cardPos.x)
            {
                card19.transform.position = cardPos;
                card18.transform.position = cardHidePos;
            }
            else if (card20.transform.position.x != cardPos.x)
            {
                card20.transform.position = cardPos;
                card19.transform.position = cardHidePos;
            }
        }
    }

}
