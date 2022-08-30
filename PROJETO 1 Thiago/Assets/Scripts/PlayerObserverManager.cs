using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerObserverManager
{
   
    public static Action<int> OnCoinsChanged;

    public static void CoinsChanged(int value)
    {
        OnCoinsChanged?.Invoke(value);
    }
    public static Action<int> OnDiamanteChanged;

    public static void DiamanteChanged(int value)
    {
        OnDiamanteChanged?.Invoke(value);
    }
}
