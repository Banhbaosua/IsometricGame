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
    public SkillTreePassiveSO SkillTreeSO => skillTreeSO;
    private CompositeDisposable disposables = new();

    private void OnEnable()
    {
        disposables?.Clear();
        image.OnPointerClickAsObservable()
            .Subscribe(_ =>
            {
                passiveChooseEvent.Notify(this, this);
            })
            .AddTo(disposables);
            
        yellowConnectionLine.gameObject.SetActive(skillTreeSO.CurrentTier != default);

    }

    public void EnableYellowLine()
    {
        yellowConnectionLine.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //disposables?.Dispose();
    }
}
