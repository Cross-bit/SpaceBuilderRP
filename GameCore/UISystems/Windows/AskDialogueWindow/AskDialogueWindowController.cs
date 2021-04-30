using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class AskDialogueWindowController : MonoBehaviour, IAskDialogueWindowController
{
    // Pozice na které musí
    [Header("Obecné vlastnosti")]
    [Tooltip("3D pozice v   e světě, na které má být dilaog vygenerován.")]
    [SerializeField] private Vector3 _positionToDrawOn;

    [Header("Komponenty")]
    [SerializeField] private RectTransform _askDialogueContainer;
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private Button _acceptBtn;
    [SerializeField] private Button _rejectBtn;

    public Vector3 PositionToDrawOn { get => _positionToDrawOn; set => _positionToDrawOn = value; }
    public Button AcceptBtn { get => _acceptBtn; set => _acceptBtn = value; }
    public Button RejectBtn { get => _rejectBtn; set => _rejectBtn = value; }
    public TextMeshProUGUI Message { get => _message; set => _message = value; }
    public RectTransform AskDialogueContainer { get => _askDialogueContainer; set => _askDialogueContainer = value; }

    public IAskDialogueWindowModul AskDialogueCurrentModul { get; set; }

    private void Start() {
        this.gameObject.SetActive(true);

        if (AskDialogueContainer == null)
            this.AskDialogueContainer = GetComponent<RectTransform>();

        this.gameObject.SetActive(false);
    }

    void FixedUpdate() {
        this.AskDialogueContainer.position = Helpers.UIWorldSpaceToScreenSpace(this.PositionToDrawOn);
    }

    public void SetWindowModul(IAskDialogueWindowModul dialogueType) { // Data binding kind of...
        if (dialogueType != this.AskDialogueCurrentModul)
            this.AskDialogueCurrentModul = dialogueType;

        this.WindowInit();
    }

    private void WindowInit() {
        // Odstraníme veškeré exitující listenery
        UI.PrepareButton(_acceptBtn);
        UI.PrepareButton(_rejectBtn);

        _acceptBtn.onClick.AddListener(() => this.OnTrue());
        _rejectBtn.onClick.AddListener(() => this.OnFalse<IAskDialogueWindowModul>());
    }

    public void OnTrue() => this.AskDialogueCurrentModul?.OnTrue();

    public void OnFalse<T>() {
           if (this.AskDialogueCurrentModul == null)
               return;

           if(!(this.AskDialogueCurrentModul is T)){
               Debug.LogError("Ask dialogue window ... Poskytnutý typ dialogového okna se neshoduje s typem momentálně definovaným v IAskDialogueWindowType.");
           }
        
            this.AskDialogueCurrentModul?.OnFalse();
    }

    public void OnWindowOn() => 
        AskDialogueCurrentModul?.OnWindowOn();

    public void OnWindowOff() {
        if (AskDialogueCurrentModul == null)
            Debug.LogError("Ask dialog window je null!!", this);
        AskDialogueCurrentModul?.OnWindowOff();

    }
}