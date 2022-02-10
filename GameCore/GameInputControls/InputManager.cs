using System.Collections.Generic;
using Assets.Scripts.GameCore.GameControls;
using Assets.Scripts.GameCore.GameControls.Controllers;
using System;

public class InputManager : SingletonPersistant<InputManager>
{
    private InputMaster inputMaster;
    
    public ICameraInputControls CamInputs { get; private set; }
    public IGeneralInputControls GeneralInputs { get; private set; }

    public PlayerInteractionsControls PlayerActionInputs { get; set; }

    public Dictionary<Type, IControlsInput> InputControls { get; private set; } = new Dictionary<Type, IControlsInput>();

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

        this.CamInputs?.UnregisterInputs(this.inputMaster);
        this.GeneralInputs?.UnregisterInputs(this.inputMaster);
        this.PlayerActionInputs?.UnregisterInputs(this.inputMaster);
        inputMaster.Disable();
    }

    private void InitalizeControllers() {

        this.CamInputs = new CameraInputControls();
        InputControls.Add(typeof(CameraInputControls), this.CamInputs);

        this.GeneralInputs = new GeneralInputControls();
        InputControls.Add(typeof(GeneralInputControls), this.GeneralInputs);

        this.PlayerActionInputs = new PlayerInteractionsControls();
        InputControls.Add(typeof(PlayerInteractionsControls), this.PlayerActionInputs);

        foreach (var controls in this.InputControls.Values)
            controls.RegisterInputs(this.inputMaster);
    }
}
