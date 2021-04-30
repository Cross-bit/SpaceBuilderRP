using UnityEngine;
//using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Block", menuName = "Pool Object")]
public class PoolObjectSO : ScriptableObject
{
    [Header ("Prefab")]
    [Tooltip("Hlavní prefab objektu.")]
    public GameObject objectToPool;
    [Header("Atributy")]
    [Tooltip("Počet objektů, který se vygeneruje na začátku.")]
    public int amount;
    [Tooltip("Zda se může navýšit počet objektů. Resp. zda se může spawnout další objekt při výtahu z poolu, pokud je empty.")]
    public bool canExpand = true;
    [Tooltip("Jasně definuje pool objekt.")]
    public Settings.PoolTypes poolType;
    [Header("Pokud UI")]
    [Tooltip("Pokud se jedná o UI")]// Mohlo být z názvu(vysoce proměnné ==> bad) enum,ale tohle je jistější/příjemnější (asi) + pokud v budoucnu udělám nějaký nástroje pro tvorbu, bude takto lepší
    public bool isUI;
    [Tooltip("Pod jakým typem canvasu se daný UI element bude nacházet.")]
    public Settings.CanvasTypes canvasDefParent;
}