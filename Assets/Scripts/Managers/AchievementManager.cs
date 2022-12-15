using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    [SerializeField]
    private int _currentKillsCount = 0;
    [SerializeField] private string _achievementInfoPath = "AchievementInfo";

    [SerializeField]
    private List<Achievement> _currentAchievements = new List<Achievement>();

    protected override void Awake()
    {
        base.Awake();
        var infos = Resources.LoadAll<AchievementInfo>(_achievementInfoPath);
        foreach (var a in infos)
        {
            // _achievements[a.Kind] = a;
            var achievement = new Achievement(a);
            _currentAchievements.Add(achievement);
        }
        Entity.OnEntityDied += OnEntityDied;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Entity.OnEntityDied -= OnEntityDied;
    }

    private void OnEntityDied(Entity entity)
    {
        if (entity.IsPlayer)
        {
            //TODO die achievement
            foreach (var a in GetAchievements(AchievementType.Die))
            {
                a.UpdateAchievement(_currentKillsCount);
            }
            return;
        }
        _currentKillsCount++;
        foreach (var a in GetAchievements(AchievementType.Kill))
        {
            a.UpdateAchievement(_currentKillsCount);
        }
    }

    private IEnumerable<Achievement> GetAchievements(AchievementType type)
    {
        var achievements = _currentAchievements.Where(a => a.Type == type);
        return achievements;
    }

    //TODO calling on each achievement types
    private void Start()
    {
        //TODO the loading of settings
    }
}
