using System.Collections;
using System.Collections.Generic;
using UtilityAi;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "UtilityAI/Considerations/Energy")]
public class Energy : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;

    public override float ScoreConsideration(RabbitController rabbit, AISensor sensor)
    {
        float score = responseCurve.Evaluate(Mathf.Clamp01(rabbit.stats.Energy / 100f));
        return score;
    }
}
    