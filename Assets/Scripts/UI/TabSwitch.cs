using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum TAB_NAMES
{
    INVENTORY,
    FACTION_RELATIONS,
    CRAFTING,
    ABILITY_TREE,
    WORLD_MAP,
    FLOWERPEDIA,
    MONSTERPEDIA,
    SAVE,
    NONE
}
public class TabSwitch : MonoBehaviour
{
    [field: SerializeField] public GameObject inventoryTab { get; private set; }
    [field: SerializeField] public GameObject factionRelationsTab { get; private set; }
    [field: SerializeField] public GameObject craftingTab { get; private set; }
    [field: SerializeField] public GameObject abilityTreeTab { get; private set; }
    [field: SerializeField] public GameObject worldMapTab { get; private set; }
    [field: SerializeField] public GameObject flowerpediaTab { get; private set; }
    [field: SerializeField] public GameObject monsterpediaTab { get; private set; }
    [field: SerializeField] public GameObject saveTab { get; private set; }

    private TAB_NAMES currentTab;
    private TAB_NAMES previousTab;

    private Dictionary<TAB_NAMES, GameObject> tabs;

    private void Awake()
    {
        currentTab = TAB_NAMES.INVENTORY;
        previousTab = TAB_NAMES.NONE;

        tabs = new Dictionary<TAB_NAMES, GameObject>();
    }

    private void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        tabs.Add(TAB_NAMES.INVENTORY, inventoryTab);
        tabs.Add(TAB_NAMES.FACTION_RELATIONS, factionRelationsTab);
        tabs.Add(TAB_NAMES.CRAFTING, craftingTab);
        tabs.Add(TAB_NAMES.ABILITY_TREE, abilityTreeTab);
        tabs.Add(TAB_NAMES.WORLD_MAP, worldMapTab);
        tabs.Add(TAB_NAMES.FLOWERPEDIA, flowerpediaTab);
        tabs.Add(TAB_NAMES.MONSTERPEDIA, monsterpediaTab);
        tabs.Add(TAB_NAMES.SAVE, saveTab);
    }

    private void ClosePreviousTab()
    {
        if (currentTab == TAB_NAMES.NONE) { return; }

        tabs[currentTab].SetActive(false);
    }

    private void OpenNewTab(TAB_NAMES tabToOpen)
    {
        if(tabToOpen == TAB_NAMES.NONE) { return; }

        tabs[tabToOpen].SetActive(true);
        previousTab = currentTab;
        currentTab = tabToOpen;

        transform.gameObject.SetActive(true);
    }

    public void CloseCurrentTab()
    {
        if (currentTab == TAB_NAMES.NONE)
        {
            return;
        }

        tabs[currentTab].SetActive(false);
        previousTab = currentTab;
        currentTab = TAB_NAMES.NONE;

        transform.gameObject.SetActive(false);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public void SwitchToInventory()
    {
        if (currentTab == TAB_NAMES.INVENTORY)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.INVENTORY);
    }

    public void SwitchToFactionRelations()
    {
        if (currentTab == TAB_NAMES.FACTION_RELATIONS)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.FACTION_RELATIONS);
    }

    public void SwitchToCrafting()
    {
        if (currentTab == TAB_NAMES.CRAFTING)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.CRAFTING);
    }

    public void SwitchToAbilityTree()
    {
        if (currentTab == TAB_NAMES.ABILITY_TREE)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.ABILITY_TREE);
    }

    public void SwitchToWorldMap()
    {
        if (currentTab == TAB_NAMES.WORLD_MAP)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.WORLD_MAP);
    }

    public void SwitchToFlowerpedia()
    {
        if (currentTab == TAB_NAMES.FLOWERPEDIA)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.FLOWERPEDIA);
    }

    public void SwitchToMonsterpedia()
    {
        if (currentTab == TAB_NAMES.MONSTERPEDIA)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.MONSTERPEDIA);
    }

    public void SwitchToSave()
    {
        if (currentTab == TAB_NAMES.SAVE)
        {
            return;
        }

        ClosePreviousTab();
        OpenNewTab(TAB_NAMES.SAVE);
    }

    
}