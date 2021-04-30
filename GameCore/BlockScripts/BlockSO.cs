using UnityEngine;
//using UnityEngine.UI;
[CreateAssetMenu (fileName = "New Block", menuName = "Block")]
public class BlockSO : ScriptableObject
{
    [Header("Název*")]
    [Tooltip("Pouze stringové označení bloku, (pro search nepodstatné)")]
    public string block_name;

    [Header("Detaily*")]
    [Tooltip("Důležité - zda se dá rozšiřovat, zda jde i např. samotná budoucnu budova editovat...")]
    [Range(1, 2)]
    public int levelOfDetails;

    [Header("Stavba")]
    [Tooltip("Čas, který zabere pro stavbu bloku.")]
    public float buildTime = 5f;

    [Header("Přípojky*")]
    [Tooltip("Důležité - počet možných přípojek")]
    public int mainNumberOfSlots;

    [Header ("Hlavní grafika*")]
    public GameObject mainGraphics;

    [Header("Rozřazení bloku Podle:")]
    [Tooltip("Typu bloku")]
    public Settings.Blocks_types blockType;

    [Tooltip("Funkce bloku, resp. i na jaký typ checkeru jde blok umístit")]
    public Settings.Checkers_types blockFunctionality;

    [Tooltip("Jaký tear daného typu bloku")]
    public int blockTear;

    [Header ("Ostatní atributy")]
    [Tooltip("-1 <= int - bere, 0 - nic, int > 0 - dodává")]
    public int energy = 0;
    [Tooltip("-1 <= int - bere, 0 - nic, int > 0 - dodává")]
    public int water = 0;
    [Tooltip("-1 <= int - bere, 0 - nic, int > 0 - dodává")]
    public int air = 0;

    [Tooltip("Jsou checkery rozmístěny symetricky?")]
    public bool isSymetric = true;
    [Tooltip("Jedná se o uzel path findingu (pro lidi).")]
    public bool isNode = false;

    [Header("Může být objekt otáčen")]
    [Tooltip("Jsou checkery rozmístěny symetricky?")] // Splňuje pouze pokud má více portů jak 1
    public bool canRotate;

    [Header("Rozměry bloku.")]
    [Tooltip("Jaké skutečné rozměry má blok, protože např. u pipe se symetric konstanta nedá použít jako rozměr...")]
    public Vector3 blockDimensions;

    [Header("Vzdálenost od středu k checkeru")]
    [Tooltip("Konstantní vzdálenost - konstanta symetričnosti ")]
    public float checkersDistance;

    [Header("Kontrolery*")]
    [Tooltip("Pozice checkerů, které definují jédnotlivé bloky")]
  //  public Vector3[] checkers;

    public Vector3[] checkersPositions;

    [Tooltip("Typy checkerů, v pořadí jejich Vector3 pozic")]
    public Settings.Checkers_types[] checkersTypes;

    [Header("UI data*")]
    public Sprite UIprew;


    /* TODO:
     * 
     * BUDEME MUSET UDĚLAT GUI PRO TVORBU!!! DAVIDE??? :)
     * 
     */
}
