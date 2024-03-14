using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItems : MonoBehaviour
{
    //кнопки
    public TMPro.TMP_Text playButtonName;
    public TMPro.TMP_Text settingsButtonName;
    public TMPro.TMP_Text creditsButtonName;
    public TMPro.TMP_Text exitButtonName;

    //панель уровней
    public TMPro.TMP_Text levelText;
    public TMPro.TMP_Text exitInfoTextLevel;

    // панель настроек
    public TMPro.TMP_Text settingText;
    public TMPro.TMP_Text volume;
    public TMPro.TMP_Text language;
    public TMPro.TMP_Text exitInfoTextSettings;

    //панель создателей
    public TMPro.TMP_Text creditsText;
    public TMPro.TMP_Text programmerText;
    public TMPro.TMP_Text designerText;
    public TMPro.TMP_Text exit;

    //подставляем новый язык в обьект
    public void NewValueLang(Language lang)
    {
        playButtonName.text = lang.Menu.PlayButtonName;
        settingsButtonName.text = lang.Menu.SettingButtonName;
        creditsButtonName.text = lang.Menu.CreditsButtonName;
        exitButtonName.text = lang.Menu.ExitButtonName;

        levelText.text = lang.Menu.LevelText;
        exitInfoTextLevel.text = lang.Menu.ExitInfoText;

        settingText.text = lang.Menu.SettingText;
        volume.text = lang.Menu.SettingVolumeMusicName;
        language.text = lang.Menu.SettingLanguageName;
        exitInfoTextSettings.text = lang.Menu.ExitInfoText;

        creditsText.text = lang.Menu.CreditsName;
        programmerText.text = lang.Menu.ProgrammerText;
        designerText.text = lang.Menu.DesignerText;
        exit.text = lang.Menu.ExitInfoText;

    }


}
