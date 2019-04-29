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
  [SerializeField] Color colorNear;
  [SerializeField] Color colorNotNear;

  [Header("Flow")]
  [SerializeField] CardEvent Appeared;
  [SerializeField] CardEvent EndAppeared;
  [SerializeField] CardEvent Disappeared;
  [SerializeField] CardEvent EndDisappeared;
  [SerializeField] CardEvent Selected;
  [SerializeField] ColorEvent NearSelected;
  [SerializeField] ColorEvent NotNearSelected;
  [SerializeField] CardEvent Unselected;
  [SerializeField] CardEvent Activated;
  [SerializeField] CardEvent EndActivated;
  [SerializeField] IntEvent EpicnessActivated;
  [SerializeField] IntEvent RomanceActivated;

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
      injectorCard.Inject(data); // -> always call this one before epicness/romance -> risk of death
      injectorCard.InjectEpicness(epicness);
      injectorCard.InjectRomance(romance);
    } else {
      injectorPlayer.Inject(data);
    }
  }

  public void Select() => Selected.Invoke(this);
  public void Unselect() => Unselected.Invoke(this);
  public void Activate() {
    Activated.Invoke(this);
    EpicnessActivated.Invoke(epicness);
    RomanceActivated.Invoke(romance);
  }
  public void Appear() => Appeared.Invoke(this);
  public void Disappear() => Disappeared.Invoke(this);
  public void TriggerEndAppeared() => EndAppeared.Invoke(this);
  public void TriggerEndDisappeared() => EndDisappeared.Invoke(this);
  public void TriggerEndActivated() => EndActivated.Invoke(this);
  public void TriggerNearSelected() => NearSelected.Invoke(colorNear);
  public void TriggerNotNearSelected() => NotNearSelected.Invoke(colorNotNear);
}
