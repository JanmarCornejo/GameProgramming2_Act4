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

    public int GetCurrentKillCount() => _currentKillsCount;

    protected override void Awake()
    {
        base.Awake();
        var infos = Resources.LoadAll<AchievementInfo>(_achievementInfoPath);
        foreach (var a in  infos)
        {
            var achievement = new Achievement(a);
            _currentAchievements.Add(achievement);
        }
        Entity.OnEntityDied += OnEntityDied;
        DataManager.OnLoadData += OnLoadData;
    }

    private void OnLoadData(SaveData data, bool loadPlayer)
    {
        _currentKillsCount = data.KillCount;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Entity.OnEntityDied -= OnEntityDied;
        DataManager.OnLoadData -= OnLoadData;
    }

    private void OnEntityDied(Entity entity)
    {
        if (entity.IsPlayer)
        {
            foreach (var a in GetAchievements(AchievementType.Die))
            {
                a.CheckAchievement(_currentKillsCount);
            }
            //TODO save
            return;
        }
        _currentKillsCount++;
        foreach (var a in GetAchievements(AchievementType.Kill))
        {
            a.CheckAchievement(_currentKillsCount);
        }
    }

    private IEnumerable<Achievement> GetAchievements(AchievementType type)
    {
        var achievements = _currentAchievements.Where(a => a.Type == type);
        return achievements;
    }

    private void Start()
    {
        //TODO the loading of settings
    }
}
