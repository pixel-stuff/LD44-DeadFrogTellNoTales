using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Data/Resource", order = 1)]
public class ResourceData : ScriptableObject {
  [Header("Positive datas")]
  public Sprite goodSprite;
  public Color goodColor;

  [Header("Negative datas")]
  public Sprite badSprite;
  public Color badColor;
}
