using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action LevelStart;
    public static event Action LevelRestart;

    public static void TriggerLevelStart() => LevelStart?.Invoke();
    public static void TriggerLevelRestart() => LevelRestart?.Invoke();
}
