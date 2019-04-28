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

  public void Inject(CardData cardData) {
    backgroundSprite.Invoke(cardData.cardSprite);
  }

  public void InjectEpicness(float epicness) => this.epicness.Invoke(epicness.ToString());
  public void InjectRomance(float romance) => this.romance.Invoke(romance.ToString());
}
