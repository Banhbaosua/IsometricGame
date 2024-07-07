using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Triggers;
using System;

public class SkillTreePassiveComponent : MonoBehaviour
{
    [SerializeField] SkillTreePassiveSO skillTreeSO;
    [SerializeField] GameEvent passiveChooseEvent;
    [SerializeField] Image image;
    [SerializeField] Transform yellowConnectionLine;
    private CompositeDisposable disposables = new();

    private void OnEnable()
    {
        disposables?.Clear();
        image.OnPointerClickAsObservable()
            .Subscribe(_ =>
            {
                passiveChooseEvent.Notify(this, skillTreeSO);
            })
            .AddTo(disposables);
            
        yellowConnectionLine.gameObject.SetActive(skillTreeSO.CurrentTier != default);

    }

    private void OnDisable()
    {
        //disposables?.Dispose();
    }
}
