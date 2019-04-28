using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable] public class CardEvent : UnityEvent<Card> { }
public enum Position {
  TopLeft,
  TopMiddle,
  TopRight,
  Left,
  Middle,
  Right,
  BottomLeft,
  BottomMiddle,
  BottomRight
}

public class Card : MonoBehaviour {

  [Header("Required")]
  [SerializeField] CardInjector injectorCard;
  [SerializeField] CardInjector injectorPlayer;
  public Position position;

  [Header("Flow")]
  public CardEvent Appeared;
  public CardEvent Disappeared;
  public CardEvent Selected;
  public CardEvent Unselected;
  public CardEvent Activated;

  public CardData refData { get; set; }
  public int epicness { get; set; }
  public int romance { get; set; }
  public bool isLinked { get; set; }
  public bool isPlayer { get; set; }
  public bool needInject { get; set; }

  public bool DebugDisappear = true;

  public void Inject(CardData data) {
    refData = data;
    epicness = data.GetEpicness();
    romance = data.GetRomance();
    DebugDisappear = false;
    isLinked = false;
    isPlayer = data.isPlayer;
    needInject = false;
    if(isPlayer) {
      injectorCard.InjectEpicness(epicness);
      injectorCard.InjectRomance(epicness);
      injectorCard.Inject(data);
    } else {
      injectorPlayer.Inject(data);
    }
  }

  public void Select() => Selected.Invoke(this);
  public void Unselect() => Unselected.Invoke(this);
  public void Activate() => Activated.Invoke(this);
  public void Appear() => Appeared.Invoke(this);
  public void Disappear() => Disappeared.Invoke(this);
}
