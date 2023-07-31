using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class SimulationControls : MonoBehaviour
{
    private Predictor _predictor;
    private SceneLoader _sceneLoader;

    public float rememberedSimulationSpeed { get; private set; } = 1.0f;
    public float velocityVectorsLength
    {
        get
        {
            return _vectorTypeToFieldDict[typeof(VelocityVectorScaler)];
        }
        private set
        {
            _vectorTypeToFieldDict[typeof(VelocityVectorScaler)] = value;
            PropagateVectorsLengthMultiplier<VelocityVectorScaler>();
        }
    }
    public float momentumVectorsLength
    {
        get
        {
            return _vectorTypeToFieldDict[typeof(MomentumVectorScaler)];
        }
        private set
        {
            _vectorTypeToFieldDict[typeof(MomentumVectorScaler)] = value;
            PropagateVectorsLengthMultiplier<MomentumVectorScaler>();
        }
    }
    public float forceVectorsLength
    {
        get
        {
            return _vectorTypeToFieldDict[typeof(ForceVectorScaler)];
        }
        private set
        {
            _vectorTypeToFieldDict[typeof(ForceVectorScaler)] = value;
            PropagateVectorsLengthMultiplier<ForceVectorScaler>();
        }
    }
    private Dictionary<Type, float> _vectorTypeToFieldDict = new Dictionary<Type, float>();

    private const float _deltaSimulationSpeed = 0.125f;
    private const float _deltaVectorLength = 0.125f;

    void Start()
    {
        _predictor = GameObject.FindObjectOfType<Predictor>();
        _sceneLoader = GameObject.FindObjectOfType<SceneLoader>();

        _vectorTypeToFieldDict[typeof(VelocityVectorScaler)] = 1.0f;
        _vectorTypeToFieldDict[typeof(MomentumVectorScaler)] = 1.0f;
        _vectorTypeToFieldDict[typeof(ForceVectorScaler)] = 1.0f;
    }

    void Update()
    {
        // stop simulation
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            PauseUnpauseSimulation();
        }

        // change simulation speed
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                rememberedSimulationSpeed = i;
                SetTimeScale(i);
                break;
            }
        }

        // increase simulation speed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DeltaSimulationSpeed(_deltaSimulationSpeed);
        }
        // decrease simulation speed
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DeltaSimulationSpeed(-_deltaSimulationSpeed);
        }

        // draw prediction lines
        if (Input.GetKeyDown(KeyCode.P))
        {
            Predict();
        }

        // toggle vector visibility
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ToggleVectors<VelocityVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ToggleVectors<MomentumVectorScaler>();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ToggleVectors<ForceVectorScaler>();
        }

        // increase / decrease vector lengths
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            velocityVectorsLength -= _deltaVectorLength;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            velocityVectorsLength += _deltaVectorLength;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            momentumVectorsLength -= _deltaVectorLength;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            momentumVectorsLength += _deltaVectorLength;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            forceVectorsLength -= _deltaVectorLength;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            forceVectorsLength += _deltaVectorLength;
        }

        // load previous/next scene
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            _sceneLoader.LoadPreviousScene();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            _sceneLoader.LoadNextScene();
        }
    }

    #region Simulation Speed
    public void PauseUnpauseSimulation()
    {
        if (Time.timeScale == 0f)
        {
            SetTimeScale(rememberedSimulationSpeed);
        }
        else
        {
            rememberedSimulationSpeed = Time.timeScale;
            SetTimeScale(0f);
        }
    }
    private void SetTimeScale(float speed)
    {
        Time.timeScale = speed;
    }

    public void IncreaseSimulationSpeed()
    {
        DeltaSimulationSpeed(_deltaSimulationSpeed);
    }

    public void DecreaseSimulationSpeed()
    {
        DeltaSimulationSpeed(-_deltaSimulationSpeed);
    }

    private void DeltaSimulationSpeed(float deltaSpeed)
    {
        rememberedSimulationSpeed += deltaSpeed;

        if (Time.timeScale != 0f) Time.timeScale += deltaSpeed;
    }
    #endregion

    #region Prediction
    public void Predict()
    {
        _predictor.Predict();
    }
    #endregion

    #region Vector Displaying
    public void ToggleVectors<SomeVectorScaler>() where SomeVectorScaler : VectorScaler
    {
        foreach (SomeVectorScaler vectorScaler in GameObject.FindObjectsByType<SomeVectorScaler>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            vectorScaler.gameObject.SetActive(!vectorScaler.gameObject.activeSelf);
        }
    }

    private void PropagateVectorsLengthMultiplier<SomeVectorScaler>() where SomeVectorScaler : VectorScaler
    {
        foreach (SomeVectorScaler vectorScaler in GameObject.FindObjectsByType<SomeVectorScaler>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            vectorScaler.lengthMultiplier = _vectorTypeToFieldDict[typeof(SomeVectorScaler)];
        }
    }

    public void IncreaseVectorsLengthMultiplier<SomeVectorScaler>() where SomeVectorScaler : VectorScaler
    {
        _vectorTypeToFieldDict[typeof(SomeVectorScaler)] += _deltaVectorLength;
        PropagateVectorsLengthMultiplier<SomeVectorScaler>();
    }

    public void DecreaseVectorsLengthMultiplier<SomeVectorScaler>() where SomeVectorScaler : VectorScaler
    {
        _vectorTypeToFieldDict[typeof(SomeVectorScaler)] -= _deltaVectorLength;
        PropagateVectorsLengthMultiplier<SomeVectorScaler>();
    }
    #endregion
}
