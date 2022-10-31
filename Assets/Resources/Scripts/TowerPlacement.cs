using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TowerPlacement : MonoBehaviour
{
    private bool canBePlayed = true;
    private bool wasPlaced;
    public int handIndex;

    private float CardCost;

    public enum CardType {High, Medium, Low};
    public CardType Cardtype;

    private Stats statsScript;

    [SerializeField] GameObject Outline;
    [SerializeField] GameObject Tower;
    [SerializeField] GameObject TowerModel;
    private Vector3 originalPos;
    public Vector2 originalCardLoc;


    private void Start()
    {
        if (Cardtype == CardType.High) CardTypeHigh();
        else if (Cardtype == CardType.Medium) CardTypeMedium();
        else CardTypeLow();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        statsScript = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

    }
    private void OnMouseDrag()
    {
        if (statsScript._energy > CardCost)
        {
            Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(MousePosition);
            Outline.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
        }
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);

    }
    private void OnMouseEnter()
    {
        if (statsScript._energy > CardCost)
        {
            Outline.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
        else Outline.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
    }
    private void OnMouseExit()
    {
        Outline.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
    private void OnMouseUp()
    {
        if(statsScript._energy > CardCost)
        {
            if(canBePlayed == true)
            {
                GameObject pp = Instantiate(Tower);
                pp.transform.position = transform.position;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                gameObject.transform.position = originalPos;
                statsScript.availableCardSlots[handIndex] = true;
                statsScript.DrawCard();
                statsScript.EnergyDeplete(CardCost);
                Invoke("moveToDiscard", 1f);
            }
            else
            {
                transform.position = statsScript._cardSlots[handIndex].transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBePlayed = false;
        Debug.Log(canBePlayed);
        TowerModel.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canBePlayed = true;
        Debug.Log(canBePlayed);
        TowerModel.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

    }

    private void moveToDiscard()
    {
        statsScript.discarded.Add(this);
        gameObject.SetActive(false);
    }
    private void CardTypeHigh()
    {
        CardCost = 50;
    }
    private void CardTypeMedium()
    {
        CardCost = 20;
    }
    private void CardTypeLow()
    {
        CardCost = 10;
    }
}
