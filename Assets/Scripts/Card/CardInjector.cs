using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class SpriteEvent : UnityEvent<Sprite> { }
[Serializable] public class StringEvent : UnityEvent<string> { }


public class CardInjector : MonoBehaviour {

  [SerializeField] SpriteEvent backgroundSprite;
  [Header("Epicness")]
  [SerializeField] StringEvent epicnessValue;
  [SerializeField] SpriteEvent epicnessSprite;
  [SerializeField] ColorEvent epicnessColor;
  [SerializeField] UnityEvent noEpicness;
  [Header("Romance")]
  [SerializeField] StringEvent romanceValue;
  [SerializeField] SpriteEvent romanceSprite;
  [SerializeField] ColorEvent romanceColor;
  [SerializeField] UnityEvent noRomance;

  [Header("Flow")]
  [SerializeField] CardDataEvent cardData;

  CardData lastInjectedData;

  public void Inject(CardData cardData) {
    backgroundSprite.Invoke(cardData.cardSprite);
    this.cardData.Invoke(cardData);
  }

  public void InjectEpicness(int epicnessValue) {
    if(epicnessValue == 0) {
      this.noEpicness.Invoke();
    } else {
      if(epicnessValue > 0) {
        epicnessSprite.Invoke(lastInjectedData.epicnessData.goodSprite);
        epicnessColor.Invoke(lastInjectedData.epicnessData.goodColor);
      } else {
        epicnessSprite.Invoke(lastInjectedData.epicnessData.badSprite);
        epicnessColor.Invoke(lastInjectedData.epicnessData.badColor);
      }
      this.epicnessValue.Invoke(epicnessValue.ToString());
    }
  }

  public void InjectRomance(int romanceValue) {
    if(romanceValue == 0) {
      this.noRomance.Invoke();
    } else {
      if(romanceValue > 0) {
        romanceSprite.Invoke(lastInjectedData.romanceData.goodSprite);
        romanceColor.Invoke(lastInjectedData.romanceData.goodColor);
      } else {
        romanceSprite.Invoke(lastInjectedData.romanceData.badSprite);
        romanceColor.Invoke(lastInjectedData.romanceData.badColor);
      }
      this.romanceValue.Invoke(romanceValue.ToString());
    }
  }
}
