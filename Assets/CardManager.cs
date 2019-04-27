using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<CardData> deck;

    public Card card;
    public Card card1;
    public Card card2;
    public Card card3;
    public Card card4;
    public Card card5;
    public Card card6;
    public Card card7;
    public Card card8;

    public int usedCardCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        card.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card.Fill(PickCard());
        });
        card1.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card1.Fill(PickCard());
        });
        card2.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card2.Fill(PickCard());
        });
        card3.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card3.Fill(PickCard());
        });
        card4.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card4.Fill(PickCard());
        });
        card5.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card5.Fill(PickCard());
        });
        card6.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card6.Fill(PickCard());
        });
        card7.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card7.Fill(PickCard());
        });
        card8.isDisappeared.AddListener(() =>
        {
            usedCardCount++;
            card8.Fill(PickCard());
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    CardData PickCard()
    {
        int weightSum = 0;
        foreach (CardData data in deck)
        {
            weightSum += (int)data.weightCurve.Evaluate(usedCardCount);
        }
        //random selected a Data
        int randomValue = (int)Random.Range(0, weightSum);
        for (int i = 0; i < deck.Count; i++)
        {
            randomValue -= (int)deck[i].weightCurve.Evaluate(usedCardCount);
            if (randomValue < 0)
            {
                return deck[i];
            }
        }
        return new CardData(); // should not happend
    }
}
