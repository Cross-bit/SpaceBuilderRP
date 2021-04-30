using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosInGame
{
    // Hlavní grid scény
    public static GameObject grid;


    // -- Funkce volané přes listenery buttns --

    
    /// <summary> Funkce voláná togglem </summary>
    public static void SetGridGame(){
       // GridState(ScreenUIHolder.Instance.leftPanel.enableGrid.isOn);
    }


    // -- vykonávací funkce gizmů --

    // Určuje zda grid bude on/off
    public static void GridState(bool setGridTo)
    {
        if (setGridTo)
            grid.SetActive(true);
        else
            grid.SetActive(false);
    }

    // 
    /*public static void ArrowPointer()
    { 
        if (ScreenUIHolder.Instance.leftPanel.enableGrid.isOn)
            grid.SetActive(true);
        else
            grid.SetActive(false);
    }*/

}


