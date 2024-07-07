using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField] List<SkillTreePassiveSO> list;

    private void OnEnable()
    {
        foreach (SkillTreePassiveSO s in list) 
        {
            s.Initiate();
        }
    }
}
