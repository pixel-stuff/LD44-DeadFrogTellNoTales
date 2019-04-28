using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerHandler : MonoBehaviour {

  public CardManager cardManager {
    get {
      if(cardManager == null) { cardManager = FindObjectOfType<CardManager>(); }

      return cardManager;
    }
    set { cardManager = value; }
  }

  public void SelectedCard(Card card) => cardManager.SelectedCard(card);
  public void EndDisappear(Card card) => cardManager.EndDisappear(card);
  public void EndAppear(Card card) => cardManager.EndAppear(card);
  public void EndActivated(Card card) => cardManager.EndActivated(card);
}
