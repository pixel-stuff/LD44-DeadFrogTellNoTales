using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManagerHandler : MonoBehaviour {

  AudienceManager audienceManager;
  public AudienceManager AudienceManager {
    get {
      if(audienceManager == null) { audienceManager = FindObjectOfType<AudienceManager>(); }

      return audienceManager;
    }
    set { audienceManager = value; }
  }


  public void CardActive(Card card) => AudienceManager.CardActive(card);
}
