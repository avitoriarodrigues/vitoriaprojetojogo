using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DiamanteUI : MonoBehaviour
{
    [SerializeField] private TMP_Text diamanteText;

    private void OnEnable()
    {
        PlayerObserverManager.OnDiamanteChanged += UpdateDiamanteText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnDiamanteChanged -= UpdateDiamanteText;
    }

    private void UpdateDiamanteText(int newDiamanteValue)
    {
        diamanteText.text = newDiamanteValue.ToString();
    }
}
