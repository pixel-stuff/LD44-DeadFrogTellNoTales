using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class Resource {
  public Vector2 values;
  public ResourceType type;
  public ResourceData data;
  public int GetEpicness() => (int)UnityEngine.Random.Range(values.x, values.y);
}

[CreateAssetMenu(fileName = "Card", menuName = "Data/Card", order = 1)]
public class CardData : ScriptableObject {
  public AnimationCurve weightCurve = AnimationCurve.Linear(0, 1, 10, 1);

  [Header("Resources")]
  public Resource[] ressources;
  public int GetEpicness() {
    var resource = ressources.FirstOrDefault(r => r.type == ResourceType.Epicness);
    if(resource == null) {
      return 0;
    } else {
      return (int)UnityEngine.Random.Range(resource.values.x, resource.values.y);
    }
  }
  public int GetRomance() {
    var resource = ressources.FirstOrDefault(r => r.type == ResourceType.Romance);
    if(resource == null) {
      return 0;
    } else {
      return (int)UnityEngine.Random.Range(resource.values.x, resource.values.y);
    }
  }

  [Header("Card")]
  public Sprite cardSprite;
  public bool isPlayer;
  public Sprite[] animationsSprite;

  int randomImageInjected;

  public Sprite GetRandomSprite() {
    randomImageInjected++;
    return animationsSprite[randomImageInjected % animationsSprite.Count()];
  }

}
