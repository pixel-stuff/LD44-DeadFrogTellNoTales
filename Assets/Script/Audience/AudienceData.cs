using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Audience", order = 1)]
public class AudienceData : ScriptableObject
{
     public AnimationCurve weightCurve = AnimationCurve.Linear(0, 1, 10, 1);

    public Vector2 romanceNeeds;
    public Vector2 epicnessNeeds;

    public Sprite calmSprite;
    public Sprite impacientSprite;
    public Sprite angrySprite;
}