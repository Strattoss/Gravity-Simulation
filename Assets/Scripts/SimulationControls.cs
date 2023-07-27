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
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                SetSimulationSpeed(i);
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DeltaSimulationSpeed(0.09f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DeltaSimulationSpeed(-0.09f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Predict();
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
}
