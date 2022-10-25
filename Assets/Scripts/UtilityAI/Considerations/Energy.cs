using System.Collections;
using System.Collections.Generic;
using AiMovement;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "UtilityAI/Considerations/Energy")]
public class Energy : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    private Stats stats;
    
    public override float ScoreConsideration(AnimalController animal)
    {
        float score = responseCurve.Evaluate(Mathf.Clamp01(animal.Stats.energy / 100f));
        return score;
    }
}
