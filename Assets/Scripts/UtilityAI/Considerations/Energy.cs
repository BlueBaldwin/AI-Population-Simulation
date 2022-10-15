using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "UtilityAI/Considerations/Energy")]
public class Energy : Consideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
