using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControls : MonoBehaviour
{
    private Predictor _predictor;
    private float _rememberedTimeScale = 1.0f;

    void Start()
    {
        _predictor = GameObject.FindObjectOfType<Predictor>();
    }

    void Update()
    {
        // stop simulation
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (Time.timeScale == 0f)
            {
                SetSimulationSpeed(_rememberedTimeScale);
            }
            else
            {
                _rememberedTimeScale = Time.timeScale;
                SetSimulationSpeed(0f);
            }
        }

        // change simulation speed
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SetSimulationSpeed(i);
                break;
            }
        }

        // increase simulation speed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DeltaSimulationSpeed(0.125f);
        }
        // decrease simulation speed
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DeltaSimulationSpeed(-0.125f);
        }

        // draw prediction lines
        if (Input.GetKeyDown(KeyCode.P))
        {
            Predict();
        }

        // toggle vectors' visibility
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            Debug.Log(KeyCode.Keypad1);
            ToggleVectors<VelocityVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            Debug.Log(KeyCode.Keypad2);
            ToggleVectors<MomentumVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            Debug.Log(KeyCode.Keypad3);
            ToggleVectors<ForceVectorScaler>();
        }
    }

    #region Simulation Speed
    private void SetSimulationSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    private void DeltaSimulationSpeed(float deltaSpeed)
    {
        Time.timeScale += deltaSpeed;
    }
    #endregion

    #region Prediction
    private void Predict() {
        _predictor.Predict();
    }
    #endregion

    #region Vector Displaying
    private void ToggleVectors<GenericVectorScaler>() where GenericVectorScaler: VectorScaler {
        foreach (GenericVectorScaler vectorScaler in GameObject.FindObjectsByType<GenericVectorScaler>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            vectorScaler.gameObject.SetActive(!vectorScaler.gameObject.activeSelf);
        }
    }
    #endregion
}
