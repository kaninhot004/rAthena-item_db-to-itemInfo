using System;
using System.Collections.Generic;

[Serializable]
public class SkillDatabase
{
    public int id;
    public int maxLv;
    public string name;
    public string nameWithQuote;
    public string description;
    public bool isCritical;
    public bool isAttackSkill;
    public bool isGroundSkill;
    public bool isSupportSkill;
    public bool isSelfSkill;
    public List<string> requiredEquipments = new List<string>();
    public List<string> requiredItems = new List<string>();
}
