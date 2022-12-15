using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    private int _currentKillsCount = 0;
    [SerializeField] private string _achievementInfoPath = "AchievementInfo";
    // private Dictionary<AchievementKind, AchievementInfo> _achievements =
    //     new Dictionary<AchievementKind, AchievementInfo>();

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
            
            return;
        }
        _currentKillsCount++;
        foreach (var achievement in _currentAchievements.
                     Where(a => a.Type == AchievementType.Kill))
        {
            achievement.UpdateAchievement(_currentKillsCount);
        }
    }

    //TODO calling on each achievement types
    private void Start()
    {
        //TODO the loading of settings
    }
}
