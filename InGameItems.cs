using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameItems : MonoBehaviour
{
    public TMP_Text NewWaveButtonName;


    public TMP_Text PauseText;
    public TMP_Text MenuPauseText;
    public TMP_Text ExitPausetext;

    public TMP_Text MagicTowerButtonName;

    public TMP_Text MessageDeath;
    public TMP_Text MenuPText;
    public TMP_Text ExitPtext;

    public void NewValueLang(Language language)
    {
        NewWaveButtonName.text = language.InGame.NewWaveButtonName;

        PauseText.text = language.InGame.PauseText;
        MenuPauseText.text = language.InGame.MenuText;
        ExitPausetext.text = language.InGame.ExitText;

        MagicTowerButtonName.text = language.InGame.MagicTowerButtonName;

        MessageDeath.text = language.InGame.MessageDeath;
        MenuPText.text = language.InGame.MenuText;
        ExitPtext.text = language.InGame.ExitText;
    }
}
