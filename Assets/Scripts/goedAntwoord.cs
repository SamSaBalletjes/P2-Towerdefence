using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class goedAntwoord : MonoBehaviour
{
    public UnityEngine.UI.Button correctButton;
    public UnityEngine.UI.Button FoutAntwoord;
    public UnityEngine.UI.Button FoutAntwoord2;
    public UnityEngine.UI.Button NextSceneButton;
    public static int coinsGathered;
    private static int maxAddCoins;
    private static int addCoins;
    public void Start()
    {
        maxAddCoins = 3;
    }
    public void IsClickedGoed()
    {
        correctButton.GetComponent<Image>().color = Color.green;
        correctButton.interactable = false;
        NextSceneButton.interactable = true;
        coinsGathered = coinsGathered + maxAddCoins;
        print(coinsGathered);
    }

    public void IsClickedFout()
    {
        maxAddCoins = maxAddCoins -= 1;
        if (maxAddCoins < 0)
        {
            maxAddCoins = 0;
        }
        print("Dit is het foute antwoord!");
    }
}
