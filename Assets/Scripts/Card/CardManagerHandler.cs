using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManagerHandler : MonoBehaviour {

  CardManager cardManager;
  public CardManager CardManager {
    get {
      if(cardManager == null) { cardManager = FindObjectOfType<CardManager>(); }

      return cardManager;
    }
    set { cardManager = value; }
  }

  public void SelectedCard(Card card) => CardManager.SelectedCard(card);
  public void EndDisappear(Card card) => CardManager.EndDisappear(card);
  public void EndAppear(Card card) => CardManager.EndAppear(card);
  public void EndActivated(Card card) => CardManager.EndActivated(card);
  public void InjectRandomPlayerImage(Image image) => CardManager.InjectRandomPlayerImage(image);

}
