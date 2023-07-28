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

        // toggle vector visibility
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            ToggleVectors<VelocityVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            ToggleVectors<MomentumVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            ToggleVectors<ForceVectorScaler>();
        }

        // increase / decrease vector lengths
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            DeltaVectorsLengthMultiplier<VelocityVectorScaler>(-0.125f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            DeltaVectorsLengthMultiplier<VelocityVectorScaler>(0.125f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            DeltaVectorsLengthMultiplier<MomentumVectorScaler>(-0.125f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            DeltaVectorsLengthMultiplier<MomentumVectorScaler>(0.125f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            DeltaVectorsLengthMultiplier<ForceVectorScaler>(-0.125f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)) {
            DeltaVectorsLengthMultiplier<ForceVectorScaler>(0.125f);
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
    private void ToggleVectors<SomeVectorScaler>() where SomeVectorScaler: VectorScaler {
        foreach (SomeVectorScaler vectorScaler in GameObject.FindObjectsByType<SomeVectorScaler>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            vectorScaler.gameObject.SetActive(!vectorScaler.gameObject.activeSelf);
        }
    }

    private void DeltaVectorsLengthMultiplier<SomeVectorScaler>(float deltaLengthMultiplier) where SomeVectorScaler: VectorScaler {
        foreach (SomeVectorScaler vectorScaler in GameObject.FindObjectsByType<SomeVectorScaler>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            vectorScaler.lengthMultiplier += deltaLengthMultiplier;
        }
    }
    #endregion
}
