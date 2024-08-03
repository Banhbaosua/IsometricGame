using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Triggers;
using UnityEditor;

public class SkillTreePassiveComponent : MonoBehaviour
{
    [SerializeField] SkillTreePassiveSO skillTreeSO;
    [SerializeField] GameEvent passiveChooseEvent;
    [SerializeField] Image image;
    [SerializeField] Transform yellowConnectionLine;
    [SerializeField] SkillTreePassiveComponent[] connectedPassives;
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
        CheckAvailability();

    }

    public void CheckAvailability()
    {
        UnlockableOff();
        foreach (var passive in connectedPassives) 
        {
            if (skillTreeSO.FirstPassive || passive.SkillTreeSO.CurrentTier > 0)
            {
                UnlockableOn();
                break;
            }
        }
    }

    public void EnableConnectedNode()
    {
        foreach (var passive in connectedPassives)
        {
            passive.CheckAvailability();
        }
    }

    void UnlockableOn()
    {
        image.raycastTarget = true;
    }

    void UnlockableOff()
    {
        image.raycastTarget = false;
    }

    public void EnableYellowLine()
    {
        yellowConnectionLine.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        disposables?.Dispose();
    }
}
