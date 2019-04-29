using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DebuggerCard : MonoBehaviour {

  [SerializeField] StringEvent LogText;

  public void Log(string str) {
    LogText.Invoke(str);
  }

  public void LogCardDataInjected(CardData cardData) => Log(string.Concat("Inject : ", cardData.name));
}
