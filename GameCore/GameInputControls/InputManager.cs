using System.Collections.Generic;
using Assets.Scripts.GameCore.GameControls;
using Assets.Scripts.GameCore.GameControls.Controllers;

public class InputManager : SingletonPersistant<InputManager>
{
    private InputMaster inputMaster;
    
    public ICameraInputControls CamInputs { get; private set; }
    public IGeneralInputControls GeneralInputs { get; private set; }

    public WorldInteractionsControls WBMInputs { get; set; }

    private List<IControlsInput> inputControls = new List<IControlsInput>();

    private void OnEnable()
    {
        if (inputMaster == null)
            inputMaster = new InputMaster();

        // Inicializace controllérů & (přídání do listu controllers)
        InitalizeControllers();
        
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        if (this.inputMaster == null)
            return;

        this.CamInputs?.UnRegisterInputs(this.inputMaster);
        this.GeneralInputs?.UnRegisterInputs(this.inputMaster);
        this.WBMInputs?.UnRegisterInputs(this.inputMaster);
        inputMaster.Disable();
    }

    private void InitalizeControllers() {
        this.CamInputs = new CameraInputControls();
        inputControls.Add(this.CamInputs);
        this.GeneralInputs = new GeneralInputControls();
        inputControls.Add(this.GeneralInputs);
        this.WBMInputs = new WorldInteractionsControls();
        inputControls.Add(this.WBMInputs);

        foreach (var controls in this.inputControls)
            controls.RegisterInputs(this.inputMaster);
    }
}
