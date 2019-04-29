using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
      var resource = lastInjectedData.ressources.FirstOrDefault(r => r.type == ResourceType.Epicness);
      if(epicnessValue > 0) {
        epicnessSprite.Invoke(resource.data.goodSprite);
        epicnessColor.Invoke(resource.data.goodColor);
      } else {
        epicnessSprite.Invoke(resource.data.badSprite);
        epicnessColor.Invoke(resource.data.badColor);
      }
      this.epicnessValue.Invoke(epicnessValue.ToString());
    }
  }

  public void InjectRomance(int romanceValue) {
    if(romanceValue == 0) {
      this.noRomance.Invoke();
    } else {
      var resource = lastInjectedData.ressources.FirstOrDefault(r => r.type == ResourceType.Romance);
      if(romanceValue > 0) {
        epicnessSprite.Invoke(resource.data.goodSprite);
        epicnessColor.Invoke(resource.data.goodColor);
        this.romanceValue.Invoke(string.Concat("-", romanceValue.ToString()));
      } else {
        epicnessSprite.Invoke(resource.data.badSprite);
        epicnessColor.Invoke(resource.data.badColor);
        this.epicnessValue.Invoke(string.Concat("-", romanceValue.ToString()));
      }
      this.romanceValue.Invoke(epicnessValue.ToString());
    }
  }
}
