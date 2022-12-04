public interface ISkillHandler
{
    Skill[] Skills { get; }
    void CastSkill(SkillType type);
}