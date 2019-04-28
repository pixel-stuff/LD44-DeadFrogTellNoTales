using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

[Serializable] public class CardEvent : UnityEvent<Card> { }
[Serializable] public class CardDataEvent : UnityEvent<CardData> { }
[Serializable] public class LinkEvent : UnityEvent<Link> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class CardsEvent : UnityEvent<IEnumerable<Card>> { }

public class CardManager : MonoBehaviour {
  [Header("Required")]
  public List<CardData> deck;
  public List<Card> cards;
  public List<Link> links;

  [Header("Flow")]

  [SerializeField] CardEvent cardInjected;
  [SerializeField] CardEvent cardFirstSelected;
  [SerializeField] CardEvent cardSelected;
  [SerializeField] CardEvent cardUnselected;
  [SerializeField] LinkEvent LinkActivated;
  [SerializeField] LinkEvent LinkDesactivated;
  [SerializeField] CardEvent cardActivated;
  [SerializeField] CardEvent cardDisappear;

  [SerializeField] StringEvent pathCountChanged;
  [SerializeField] CardsEvent cardsLinkedReached;
  [SerializeField] UnityEvent PathDone;

  [HideInInspector] public int usedCardCount = 0;

  const int NUMBER_LINKABLE_CARD = 4;
  List<Card> selectedCards;


  // Start is called before the first frame update
  void Start() {
    selectedCards = new List<Card>();
    foreach(var link in links) { link.gameObject.SetActive(false); }
    InjectCard(cards[0], true);
    for(int i = 1; i < cards.Count(); i++) { InjectCard(cards[i]); }
  }

  public void InjectCard(Card card, bool setPlayer = false) {
    usedCardCount++;
    card.Inject(PickCard(setPlayer));
    cardInjected.Invoke(card);
  }

  public CardData PickCard(bool setPlayer = false) {
    if(setPlayer) { return deck.FirstOrDefault(c => c.isPlayer); }

    int weightSum = 0;
    foreach(CardData data in deck.Where(c => !c.isPlayer)) {
      weightSum += (int)data.weightCurve.Evaluate(usedCardCount);
    }
    //random selected a Data
    int randomValue = (int)UnityEngine.Random.Range(0, weightSum);
    foreach(CardData data in deck.Where(c => !c.isPlayer)) {
      randomValue -= (int)data.weightCurve.Evaluate(usedCardCount);
      if(randomValue < 0) {
        return data;
      }
    }
    return new CardData(); // should not happend
  }

  public void SelectedCard(Card card) {
    if(card.isPlayer) { return; }
    if(selectedCards.Count() >= NUMBER_LINKABLE_CARD) { return; }

    if(card.isLinked) {
      TryRemoveCardFromPath(card);
    } else {
      if(!selectedCards.Any()) {
        TryAddCardToPath(card, cards.FirstOrDefault(c => c.isPlayer));
      } else {
        TryAddCardToPath(card, selectedCards[selectedCards.Count - 1]);
      }

      if(selectedCards.Count() >= NUMBER_LINKABLE_CARD) {
        StartExecutePath();
        //StartCoroutine(AnimationCardsSelectedReach());
      }
    }
    pathCountChanged.Invoke(selectedCards.Count().ToString());
  }

  void TryAddCardToPath(Card cardClicked, Card previousCardClicked) {
    var link = GetLink(cardClicked, previousCardClicked);
    if(link != null) {
      cardClicked.Select();
      link.gameObject.SetActive(true);
      selectedCards.Add(cardClicked);
      cardFirstSelected.Invoke(cardClicked);
      LinkActivated.Invoke(link);
    }
  }

  void TryRemoveCardFromPath(Card cardClicked) {
    if(selectedCards.LastOrDefault().Equals(cardClicked)) {
      cardClicked.Unselect();

      var link = default(Link);
      if(selectedCards.Count() == 1) {
        link = GetLink(cardClicked, cards.FirstOrDefault(c => c.isPlayer));
      } else {
        link = GetLink(cardClicked, selectedCards[selectedCards.Count - 2]);
      }
      link.gameObject.SetActive(false);
      selectedCards.Remove(cardClicked);
      cardUnselected.Invoke(cardClicked);
      LinkDesactivated.Invoke(link);
    }
  }

  Link GetLink(Card a, Card b) {
    //Debug.Log("ASK : " + a.position + " - " + b.position);

    //var linksFirst = links.Where(l => l.linkedPositions.Contains(a.position));
    //Debug.Log("FIRST ; " + linksFirst.Count());
    //foreach(var l in links) { Debug.Log("" + l.linkedPositions[0] + " - " + l.linkedPositions[1]); }

    //var linkSecond = linksFirst.FirstOrDefault(l => l.linkedPositions.Contains(b.position));
    //Debug.Log("SECOOOOOOOOOOOOOOND : " + (linkSecond == null));
    //if(linkSecond != null) {
    //  Debug.Log("SECOND ; " + linkSecond.linkedPositions[0] + " - " + linkSecond.linkedPositions[1]);
    //}

    return links.Where(l => l.linkedPositions.Contains(a.position)).FirstOrDefault(l => l.linkedPositions.Contains(b.position));
  }

  void StartExecutePath() {
    cardsLinkedReached.Invoke(selectedCards);

    // make player disappear
    cards.FirstOrDefault(c => c.isPlayer).Disappear();
  }

  void StartActivate(Card card) {
    card.Activate();
    cardActivated.Invoke(card);
  }

  public void EndActivated(Card card) {
    card.Disappear(); //make card disappear
  }

  public void EndDisappear(Card card) {
    if(card.isPlayer) {
      StartActivate(selectedCards[0]);
    } else {
      var nextCardIndex = selectedCards.IndexOf(card) + 1;
      if(nextCardIndex < selectedCards.Count()) { // there is still card in the path to activate
        StartActivate(selectedCards[nextCardIndex]);
      } else { // no more card to activate
        TriggerPathDone();
      }
    }
  }

  void TriggerPathDone() {
    PathDone.Invoke();

    InjectCard(cards.FirstOrDefault(c => c.isPlayer));
    for(int i = 0; i < selectedCards.Count(); i++) {
      if(i < selectedCards.Count() - 1) {
        InjectCard(selectedCards[i]);
      } else {
        InjectCard(selectedCards[i], true);
      }
    }
    selectedCards.Clear();
    foreach(var link in links) { link.gameObject.SetActive(false); }

  }

  public void EndAppear(Card card) {

  }
}
