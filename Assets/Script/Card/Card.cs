using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


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
  [SerializeField] CardEvent Appeared;
  [SerializeField] CardEvent EndAppeared;
  [SerializeField] CardEvent Disappeared;
  [SerializeField] CardEvent EndDisappeared;
  [SerializeField] CardEvent Selected;
  [SerializeField] CardEvent Unselected;
  [SerializeField] CardEvent Activated;
  [SerializeField] CardEvent EndActivated;

  public CardData refData { get; set; }
  public int epicness { get; set; }
  public int romance { get; set; }
  public bool isLinked { get; set; }
  public bool isPlayer { get { return refData.isPlayer; } }
  public bool needInject { get; set; }

  public bool DebugDisappear = true;

  public void Inject(CardData data) {
    refData = data;
    epicness = data.GetEpicness();
    romance = data.GetRomance();
    DebugDisappear = false;
    isLinked = false;
    needInject = false;
    if(!isPlayer) {
      injectorCard.InjectEpicness(epicness);
      injectorCard.InjectRomance(romance);
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
  public void TriggerEndAppeared() => EndAppeared.Invoke(this);
  public void TriggerEndDisappeared() => EndDisappeared.Invoke(this);
  public void TriggerEndActivated() => EndActivated.Invoke(this);
}
