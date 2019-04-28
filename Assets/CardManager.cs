using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

[Serializable] public class CardsEvent : UnityEvent<IEnumerable<Card>> { }

public class CardManager : MonoBehaviour {
  [Header("Required")]
  public List<CardData> deck;
  public List<Card> cards;
  public List<Link> links;

  [Header("Flow")]
  [SerializeField] CardsEvent cardsLinkedReached;

  [HideInInspector] public int usedCardCount = 0;

  const int NUMBER_LINKABLE_CARD = 4;
  List<Card> selectedCards;
  Card playercard;
  bool firstTimeCardPicked;

  // Start is called before the first frame update
  void Start() {
    selectedCards = new List<Card>();
    firstTimeCardPicked = true;
    foreach(var link in links) { link.gameObject.SetActive(false); }
    FillCard(cards[0], true);
    for(int i = 1; i < cards.Count(); i++) { FillCard(cards[i]); }
  }

  public void FillCard(Card card, bool setPlayer = false) {
    usedCardCount++;
    card.Inject(PickCard(setPlayer));
  }

  public CardData PickCard(bool setPlayer = false) {
    if(setPlayer) { return deck.FirstOrDefault(c => c.isPlayer); }

    int weightSum = 0;
    foreach(CardData data in deck) {
      weightSum += (int)data.weightCurve.Evaluate(usedCardCount);
    }
    //random selected a Data
    int randomValue = (int)UnityEngine.Random.Range(0, weightSum);
    for(int i = 0; i < deck.Count; i++) {
      randomValue -= (int)deck[i].weightCurve.Evaluate(usedCardCount);
      if(randomValue < 0) {
        return deck[i];
      }
    }
    return new CardData(); // should not happend
  }

  public void SelectedCard(Card card) {
    if(card.isPlayer) { return; }

    if(card.isLinked) {
      if(selectedCards.LastOrDefault().Equals(card)) {
        card.Unselect();
        GetLink(card, selectedCards[selectedCards.Count - 2]).gameObject.SetActive(false);
      }
    } else {
      if(!selectedCards.Any()) {
        var link = GetLink(card, playercard);
        if(link != null) {
          card.Select();
          link.gameObject.SetActive(true);
        }
      } else {
        var link = GetLink(card, selectedCards[selectedCards.Count - 1]);
        if(link != null) {
          card.Select();
          link.gameObject.SetActive(true);
        }
      }
    }
    if(selectedCards.Count() >= NUMBER_LINKABLE_CARD) {
      cardsLinkedReached.Invoke(selectedCards);
      StartCoroutine(AnimationCardsSelectedReach());
    }
  }

  Link GetLink(Card a, Card b) => links.Where(l => l.linkedPositions.Contains(a.position)).FirstOrDefault(l => l.linkedPositions.Contains(b.position));

  IEnumerator AnimationCardsSelectedReach() {
    // start activated 
    for(int i = 0; i < selectedCards.Count(); i++) {
      selectedCards[i].Activate();
      yield return new WaitForSeconds(2.0f);
      selectedCards[i].Disappear();
    }
    yield return new WaitForSeconds(1.0f);

    //reset all card
    for(int i = 0; i < selectedCards.Count(); i++) {
      if(i <= selectedCards.Count() - 1) {
        FillCard(selectedCards[i]);
      } else {
        FillCard(selectedCards[i], true);
      }
    }
    selectedCards.Clear();
  }
}
