using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language
{
    public string language;

    public Menu Menu;
    public InGame InGame;

    public Language(Menu Menu, InGame InGame, string language)
    {
        this.Menu = Menu;
        this.InGame = InGame;
        this.language = language;
    }
}

[System.Serializable]
public class InGame
{
    public string NewWaveButtonName;

    public string MagicTowerButtonName;

    public string PauseText;
    public string MenuText;
    public string ExitText;

    public string MessageDeath;
}

[System.Serializable]
public class Menu
{
    public string PlayButtonName;
    public string SettingButtonName;
    public string CreditsButtonName;
    public string ExitButtonName;

    public string ExitInfoText;

    public string LevelText;

    public string SettingText;
    public string SettingVolumeMusicName;
    public string SettingLanguageName;

    public string CreditsName;
    public string ProgrammerText;
    public string DesignerText;
}
