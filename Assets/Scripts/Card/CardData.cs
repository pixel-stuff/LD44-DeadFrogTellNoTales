using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Data/Card", order = 1)]
public class CardData : ScriptableObject {
  public AnimationCurve weightCurve = AnimationCurve.Linear(0, 1, 10, 1);

  [Header("Resources - Epicness")]
  public Vector2 epicness;
  public ResourceData epicnessData;
  public int GetEpicness() => (int)Random.Range(epicness.x, epicness.y);

  [Header("Resources - Epicness")]
  public Vector2 romance;
  public ResourceData romanceData;
  public int GetRomance() => (int)Random.Range(romance.x, romance.y);

  [Header("Card")]
  public Sprite cardSprite;
  public bool isPlayer;

}
