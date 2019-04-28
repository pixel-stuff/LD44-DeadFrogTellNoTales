using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Card", order = 1)]
public class CardData : ScriptableObject {
  public AnimationCurve weightCurve = AnimationCurve.Linear(0, 1, 10, 1);

    public Vector2 epicness;
    public Vector2 romance;


  public Sprite cardSprite;
  public bool isPlayer;

  public int GetEpicness() => (int)Random.Range(epicness.x, epicness.y);
  public int GetRomance() => (int)Random.Range(romance.x, romance.y);
}
