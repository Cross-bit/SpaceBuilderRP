using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "In Game Highlight")]
public class InGameHighlightsSO : ScriptableObject
{

    [Header ("Barva zvýrazněného objektu")]
    public Color color;


    [Header("Typ zvýraznění")]
    [Tooltip ("Jednoznačně definuje typ highlight (např. checker => zvýrazní se kontrolér)")]
    public Settings.GameHighlights highlightType;
}
