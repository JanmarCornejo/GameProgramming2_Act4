using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "AchievementInfo", menuName = "Survival Game/Add Achievement Info")]
public class AchievementInfo : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public AchievementKind Kind;
    public AchievementType Type;
    public int ConditionQuantity = 0;
}
