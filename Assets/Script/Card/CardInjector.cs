using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class SpriteEvent : UnityEvent<Sprite> { }
[Serializable] public class StringEvent : UnityEvent<string> { }


public class CardInjector : MonoBehaviour {

  [SerializeField] SpriteEvent backgroundSprite;
  [SerializeField] StringEvent epicness;
  [SerializeField] StringEvent romance;

  [Header("Flow")]
  [SerializeField] CardDataEvent cardData;

  public void Inject(CardData cardData) {
    backgroundSprite.Invoke(cardData.cardSprite);
    this.cardData.Invoke(cardData);
  }

  public void InjectEpicness(float epicness) => this.epicness.Invoke(epicness.ToString());
  public void InjectRomance(float romance) => this.romance.Invoke(romance.ToString());
}
