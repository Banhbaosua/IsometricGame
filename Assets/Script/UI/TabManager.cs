using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TabType
{
    Fight,
    Character,
    Blacksmith,
    Runes,
    SkillTree,
    Achivements,
    Profile,
}
public class TabManager : MonoBehaviour
{
    public static TabManager Instance;
    public delegate void TabBtnClick(TabButton tab_Btn);
    public TabBtnClick OnTabBtnClick;
    private void Awake()
    {
        Instance = this;
    }

    private List<TabButton> tabs;

    public void AddTabBtnToList(TabButton tab_Btn)
    {
        if(tabs == null)
            tabs = new List<TabButton>();

        tabs.Add(tab_Btn);
    }
}
