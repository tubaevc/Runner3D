using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class MainMenuManager : EventListenerMono
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsMenuPanel;

    private void Start()
    {
        SetPanelActive(_mainMenuPanel);
    }

    private void SetPanelActive(GameObject panel)
    {
        _mainMenuPanel.SetActive(_mainMenuPanel == panel);
        _settingsMenuPanel.SetActive(_settingsMenuPanel == panel);
    }

    protected override void RegisterEvents()
    {
        MainMenuEvents.SettingsBTN += OnSettingsBTN;
        MainMenuEvents.SettingsExitBTN += OnSettingsExitBTN;
    }

    private void OnSettingsBTN()
    {
        SetPanelActive(_settingsMenuPanel);
    }

    private void OnSettingsExitBTN()
    {
        SetPanelActive(_mainMenuPanel);
    }

    protected override void UnRegisterEvents()
    {
        MainMenuEvents.SettingsBTN -= OnSettingsBTN;
        MainMenuEvents.SettingsExitBTN -= OnSettingsExitBTN;
    }
}