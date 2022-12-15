using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private Coroutine _mainCo;

    private void OnEnable()
    {
        Achievement.OnDoneAchievement += OnDoneAchievement;
    }

    private void OnDisable()
    {
        Achievement.OnDoneAchievement -= OnDoneAchievement;
    }

    private void Start()
    {
         //ShowMessage("testing", 1.5f);
    }
    
    private void OnDoneAchievement(IAchievementHandler achievement)
    {
        var msg = $"{achievement.Description} Unlocked!";
        Popup(msg, 4f);
    }

    public void Popup(string msg, float showTime = 2f)
    {
        if (_mainCo != null)
            StopCoroutine(_mainCo);
        _mainCo = StartCoroutine(Fade(msg, showTime));
    }

    private IEnumerator Fade(string msg, float showTime = 2f, float fadeTime = 0.4f, bool close = false)
    {
        _textMesh.text = msg;
        float targetValue = 1;
        float t = 0;
        if (close)
        {
            t = 1 - _cg.alpha;
            targetValue = 0;
        }
        float alpha = _cg.alpha;
        for (t = 0; t < 1.0f ; t+= Time.unscaledDeltaTime / fadeTime)
        {
            _cg.alpha = Mathf.Lerp(alpha, targetValue, t);
            yield return new WaitForEndOfFrame();
        }
        _cg.alpha = targetValue;
        if (close) yield break;

        yield return new WaitForSeconds(showTime);
        
        StopCoroutine(_mainCo);
        _mainCo = StartCoroutine(Fade(msg, 0,0.4f, true));
    }
}