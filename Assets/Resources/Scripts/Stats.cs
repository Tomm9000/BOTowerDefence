using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class Stats : MonoBehaviour
{
    [SerializeField] GameObject textmeshpro_Health;
    [SerializeField] GameObject textmeshpro_Energy;

    private float _lives = 100;
    public float _energy = 0;

    TextMeshProUGUI textmeshpro_healthText;
    TextMeshProUGUI textmeshpro_energyText;

    //Deck of cards
    public List<TowerPlacement> deck = new List<TowerPlacement>();
    public List<TowerPlacement> discarded = new List<TowerPlacement>();
    [SerializeField] Transform[] _cardSlots;
    public bool[] availableCardSlots;


    public GameObject amountInDeck;
    TextMeshProUGUI amountInDeckText;

    public GameObject amountInDiscard;
    TextMeshProUGUI amountInDiscardText;
    private void Start()
    {
        for (int i = 0; i < _cardSlots.Length; i++)
        {
            DrawCard();
        }
        textmeshpro_healthText = textmeshpro_Health.GetComponent<TextMeshProUGUI>();
        textmeshpro_healthText.text = "Health: " + _lives.ToString();

        amountInDeckText = amountInDeck.GetComponent<TextMeshProUGUI>();
        amountInDeckText.text = deck.Count.ToString();

        amountInDiscardText = amountInDiscard.GetComponent<TextMeshProUGUI>();
        amountInDiscardText.text = discarded.Count.ToString();

        textmeshpro_energyText = textmeshpro_Energy.GetComponent<TextMeshProUGUI>();
        textmeshpro_energyText.text = "Energy: " + _energy.ToString();
    }
    private void Update()
    {
        amountInDeckText.text = deck.Count.ToString();
        amountInDiscardText.text = discarded.Count.ToString();
        textmeshpro_energyText.text = "Energy: " + _energy.ToString();
        if (_energy > 100)
        {
            _energy = 100;
        }
    }

    public void EnemyThrough(float TypeDamage)
    {
        _lives -= TypeDamage;
        textmeshpro_healthText.text = "Health: " + _lives.ToString();
        Debug.Log(_lives);
    }
    
    public void EnergyGenerator(float EnergyAmount)
    {

        _energy += EnergyAmount;
        Debug.Log(_energy);
    }
    public void EnergyDeplete(float Cost)
    {
        _energy -= Cost;
    }

    public void EnemyKill(float energyGain)
    {
        if (_energy < 100)
        {
            _energy += energyGain;
        }
        
    }
    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            TowerPlacement randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < deck.Count; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = _cardSlots[i].position;

                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void Shuffle()
    {
        if(discarded.Count >= 1)
        {
            foreach (TowerPlacement card in discarded)
            {
                deck.Add(card);
            }
            discarded.Clear();
        }
    }
}
