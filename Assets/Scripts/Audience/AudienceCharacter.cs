using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable] public class MyAudianceCharacterEvent : UnityEvent<AudienceCharacter> {}
[System.Serializable] public class OverrideControllerEvent : UnityEvent<AnimatorOverrideController> { }

public class AudienceCharacter : MonoBehaviour {
  public MyAudianceCharacterEvent isComplete;
  public MyAudianceCharacterEvent isFilled;
  public OverrideControllerEvent isOverrideControllerFilled;
  public SpriteEvent isBackgroundImageFilled;
  public MyAudianceCharacterEvent isSatisfied;
  public MyAudianceCharacterEvent isDisappointed;
  public MyAudianceCharacterEvent isAnnoyed;
  public MyAudianceCharacterEvent isDisappeared;

  public StringEvent epicnessStringUpdated;
  public StringEvent romanceStringUpdated;
  public SpriteEvent cardSpriteUpdated;

  public IntEvent pointCount;
  public AudienceData refData;
  public int neededEpicness = 0;
  public int neededRomance = 0;
  public int currentEpicness = 0;
  public int currentRomance = 0;
  public bool isInit = false;

  public bool DebugOver = true;

  public void Fill(AudienceData data) {
    isInit = true;
    refData = data;
    neededEpicness = (int)Random.Range(data.epicnessNeeds.x, data.epicnessNeeds.y);
    neededRomance = (int)Random.Range(data.romanceNeeds.x, data.romanceNeeds.y);
    currentEpicness = neededEpicness;
    currentRomance = neededRomance;
    if(currentEpicness > 0)
      epicnessStringUpdated.Invoke(currentEpicness.ToString());
    if(currentRomance > 0)
      romanceStringUpdated.Invoke(currentRomance.ToString());
    cardSpriteUpdated.Invoke(data.mainSprite);//this.GetComponent<Image>().sprite = data.calmSprite;
    DebugOver = false;
    isFilled.Invoke(this);
    isOverrideControllerFilled.Invoke(data.overrideController);
    isBackgroundImageFilled.Invoke(data.backgroundSprite);
  }

  // Update is called once per frame
  void Update() {
    if(DebugOver) {
      isComplete.Invoke(this);
    }
  }

  public void ApplyModifier(int epicness, int romance) {
    if(neededEpicness > 0)
      currentEpicness -= epicness;
    if(neededRomance > 0)
      currentRomance -= romance;

    bool epicnessSatisfaction = (currentEpicness <= 0) ? false : epicness > 0;
    bool romanceSatisfaction = (currentRomance <= 0) ? false : romance > 0;
    if(epicnessSatisfaction || romanceSatisfaction) {
      isSatisfied.Invoke(this);
    } else if((!epicnessSatisfaction && epicness < 0) || (!romanceSatisfaction && romance < 0)) {
      isDisappointed.Invoke(this);
    }

    if(epicness != 0 && neededEpicness > 0) {
      epicnessStringUpdated.Invoke((currentEpicness >= 0) ? currentEpicness.ToString() : "0");
    }

    if(romance != 0 && neededRomance > 0) {
      romanceStringUpdated.Invoke((currentRomance >= 0) ? currentRomance.ToString() : "0");
    }
  }

  public void Resolve() {
    if(isInit) {
      if(currentEpicness <= 0 && currentRomance <= 0) {
        isComplete.Invoke(this);
      } else {
        isAnnoyed.Invoke(this);
      }
    }
  }

  public void IsDisappeared() {
    isDisappeared.Invoke(this);
  }
}
