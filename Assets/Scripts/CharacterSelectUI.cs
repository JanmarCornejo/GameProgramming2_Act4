using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private EntityType Type;
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _parent;
    [SerializeField] private AudioClip Sound;

    public static event Action playerSpawnEvent;

    private void OnEnable()
    {
        _btn.onClick.AddListener(() => SelectCharacter(Type));
    }

    private void OnDisable()
    {
        _btn.onClick.RemoveListener(() => SelectCharacter(Type));
    }

    private void SelectCharacter(EntityType type)
    {
        EntityManager.Instance.CreateEntity(type);
        SoundManager.instance.PlaySound(Sound);
        playerSpawnEvent?.Invoke();
        Destroy(_parent.gameObject);
    }


}
