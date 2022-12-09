using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _skillCooldownBar;

    private void Start()
    {

    }

    private void OnEnable()
    {
        Entity.OnUpdatePlayerHUD += OnOnUpdatePlayerHUD;
        Entity.OnCastSkill += OnOnCastSkill;
    }

    private void OnOnCastSkill(float time)
    {
        StartCoroutine(FillAmount(time));
    }

    private void OnOnUpdatePlayerHUD(Sprite icon, Entity entity)
    {
        if (!entity.IsPlayer)
            return;
        _iconImage.sprite = icon;
        _healthBar.fillAmount = (float)entity.CurrentHealth / entity.MaxHealth;
    }

    private void OnDisable()
    {
        Entity.OnUpdatePlayerHUD -= OnOnUpdatePlayerHUD;
        Entity.OnCastSkill -= OnOnCastSkill;
    }

    private IEnumerator FillAmount(float secondsToFill)
    {
        float fillAmt = 1;
        _skillCooldownBar.fillAmount = fillAmt;
        while (fillAmt > 0)
        {
            fillAmt -= (1.0f / (float)secondsToFill * Time.deltaTime);
            // Debug.Log(fillAmt);
            _skillCooldownBar.fillAmount = fillAmt;
            yield return null;
        }
        
    }
}
