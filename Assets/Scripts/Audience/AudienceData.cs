using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audience", menuName = "Data/Audience", order = 1)]
public class AudienceData : ScriptableObject {
  public AnimationCurve weightCurve = AnimationCurve.Linear(0, 1, 10, 1);

  [Header("Required")]
  public Vector2 epicnessNeeds;
  public Vector2 romanceNeeds;

  [Header("Data")]
  public Sprite mainSprite;
  public Sprite backgroundSprite;
  public bool isBoss = false;
  public AnimatorOverrideController overrideController;

}