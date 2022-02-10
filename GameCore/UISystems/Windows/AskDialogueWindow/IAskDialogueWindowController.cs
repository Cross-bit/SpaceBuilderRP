﻿using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IAskDialogueWindowController
{
    Button AcceptBtn { get; set; }
    RectTransform AskDialogueContainer { get; set; }
    IAskDialogueWindowModul AskDialogueCurrentModul { get; set; }
    TextMeshProUGUI Message { get; set; }
    Vector3 PositionToDrawOn { get; set; }
    Button RejectBtn { get; set; }

    void Rejected<T>(); // what client chooses
    void Accepted();
    void OnWindowOff();
    void OnWindowOn();
    void SetWindowModul(IAskDialogueWindowModul dialogueType);
}