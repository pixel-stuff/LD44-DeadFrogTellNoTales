using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DebuggerCardManager : MonoBehaviour {

  [SerializeField] Text text;

  int numberOfLogAdded;

  void Start() {
    numberOfLogAdded = 0;
  }

  // Update is called once per frame
  public void Log(string str) {
    text.text += string.Concat(str, "\n");
    numberOfLogAdded++;

    if(text.text.Count(t => t.Equals('\n')) > 10) {
      //Debug.Log("Too many log remove :" + text.text.Substring(0, text.text.IndexOf('\n')));
      text.text.Remove(0, text.text.IndexOf('\n'));
    }
  }

  public void LogCardInjected(Card card) => Log(string.Concat("Inject : ", card.position));
  public void LogCardFirstSelected(Card card) => Log(string.Concat("First Selected : ", card.position));
  public void LogCardSelected(Card card) => Log(string.Concat("Selected : ", card.position));
  public void LogCardUnselected(Card card) => Log(string.Concat("Unselect : ", card.position));
  public void LogCardActivated(Card card) => Log(string.Concat("Activated : ", card.position));
  public void LogCardDisappear(Card card) => Log(string.Concat("Disappear : ", card.position));

  public void LogLinkActivated(Link link) => Log(string.Concat("Link activated : ", link.linkedPositions[0], "/", link.linkedPositions[1]));
  public void LogLinkDesactivated(Link link) => Log(string.Concat("Link desactivated : ", link.linkedPositions[0], "/", link.linkedPositions[1]));
}
