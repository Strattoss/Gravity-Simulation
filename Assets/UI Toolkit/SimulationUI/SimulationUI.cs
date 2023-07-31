using UnityEngine;
using UnityEngine.UIElements;

struct PropertyControls
    {
        public Label value;
        public Button up;
        public Button down;
        public Button toggle;
    }

public class SimulationUI : MonoBehaviour
{
    private SimulationControls _simulationControls;
    private Statistics _statistics;
    private Predictor _predictor;
    private SceneLoader _sceneLoader;

    private VisualElement _simulationUI;
    private Button _toggleSimulationUI;

    // scene buttons
    private Button _previousScene, _nextScene;

    // statistics
    private Label _energyKinetic, _energyPotential, _energyTotal, _energyTotalMaximal, _energyTotalMinimal, _totalMomentum;

    // simulation properties
    private PropertyControls _simulationSpeed;
    private Button _predict;

    // vectors
    

    private PropertyControls _velocityVectors, _momentumVectors, _forceVectors;

    private void OnEnable()
    {
        _simulationControls = GameObject.FindGameObjectWithTag("SimulationController").GetComponent<SimulationControls>();
        _statistics = GameObject.FindGameObjectWithTag("SimulationManager").GetComponent<Statistics>();
        _predictor = GameObject.FindObjectOfType<Predictor>();
        _sceneLoader = GameObject.FindObjectOfType<SceneLoader>();


        var uiDocument = GetComponent<UIDocument>();

        // UI panel
        _simulationUI = uiDocument.rootVisualElement.Q<VisualElement>("simulationUI");
        _toggleSimulationUI = uiDocument.rootVisualElement.Q<Button>("toggle-simulationUI");
        _toggleSimulationUI.clicked += ToggleSimulationUI;

        // scene loaders
        var sceneLoader = uiDocument.rootVisualElement.Q("scene-loader");
        BindSceneLoader(sceneLoader);

        var statistics = uiDocument.rootVisualElement.Q("statistics");
        BindStatistics(statistics);

        var vectors = uiDocument.rootVisualElement.Q("vectors");
        BindVectors(vectors);

        var simulationProperties = uiDocument.rootVisualElement.Q("simulation-properties");
        BindSimulationProperties(simulationProperties);
    }

    private void BindSceneLoader(VisualElement sceneLoader)
    {
        _previousScene = sceneLoader.Q<Button>("previous-scene");
        _previousScene.clicked += LoadPreviousScene;
        _nextScene = sceneLoader.Q<Button>("next-scene");
        _nextScene.clicked += LoadNextScene;
    }

    private void BindStatistics(VisualElement statistics)
    {
        _energyKinetic = statistics.Q<Label>("energy-kinetic");
        _energyPotential = statistics.Q<Label>("energy-potential");
        _energyTotal = statistics.Q<Label>("energy-total");

        _energyTotalMaximal = statistics.Q<Label>("energy-total-maximal");
        _energyTotalMinimal = statistics.Q<Label>("energy-total-minimal");

        _totalMomentum = statistics.Q<Label>("momentum");
    }

    private void BindSimulationProperties(VisualElement simulationProperties)
    {
        _simulationSpeed.value = simulationProperties.Q<Label>("simulation-speed");

        _simulationSpeed.down = simulationProperties.Q<Button>("simulation-speed-down");
        _simulationSpeed.down.clicked += DecreaseSimulationSpeed;

        _simulationSpeed.toggle = simulationProperties.Q<Button>("simulation-speed-toggle");
        _simulationSpeed.toggle.clicked += ToggleSimulationSpeed;

        _simulationSpeed.up = simulationProperties.Q<Button>("simulation-speed-up");
        _simulationSpeed.up.clicked += IncreaseSimulationSpeed;

        _predict = simulationProperties.Q<Button>("simulation-predict");
        _predict.clicked += SimulationPredict;
    }

    private void BindVectors(VisualElement vectors)
    {
        _velocityVectors.value = vectors.Q<Label>("velocity-length");
        _velocityVectors.down = vectors.Q<Button>("velocity-length-down");
        _velocityVectors.down.clicked += DecreaseVelocityLengthMultiplier;
        _velocityVectors.toggle = vectors.Q<Button>("velocity-length-toggle");
        _velocityVectors.toggle.clicked += ToggleVelocityVectors;
        _velocityVectors.up = vectors.Q<Button>("velocity-length-up");
        _velocityVectors.up.clicked += IncreaseVelocityLengthMultiplier;

        _momentumVectors.value = vectors.Q<Label>("momentum-length");
        _momentumVectors.down = vectors.Q<Button>("momentum-length-down");
        _momentumVectors.down.clicked += DecreaseMomentumLengthMultiplier;
        _momentumVectors.toggle = vectors.Q<Button>("momentum-length-toggle");
        _momentumVectors.toggle.clicked += ToggleMomentumVectors;
        _momentumVectors.up = vectors.Q<Button>("momentum-length-up");
        _momentumVectors.up.clicked += IncreaseMomentumLengthMultiplier;

        _forceVectors.value = vectors.Q<Label>("force-length");
        _forceVectors.down = vectors.Q<Button>("force-length-down");
        _forceVectors.down.clicked += DecreaseForceLengthMultiplier;
        _forceVectors.toggle = vectors.Q<Button>("force-length-toggle");
        _forceVectors.toggle.clicked += ToggleForceVectors;
        _forceVectors.up = vectors.Q<Button>("force-length-up");
        _forceVectors.up.clicked += IncreaseForceLengthMultiplier;
    }

    private void ToggleSimulationUI() {
        _simulationUI.ToggleInClassList("simulationUI--hide");
    }

    private void LoadNextScene() => _sceneLoader.LoadNextScene();
    private void LoadPreviousScene() => _sceneLoader.LoadPreviousScene();
    private void DecreaseVelocityLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<VelocityVectorScaler>();
    private void ToggleVelocityVectors() => _simulationControls.ToggleVectors<VelocityVectorScaler>();
    private void IncreaseVelocityLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<VelocityVectorScaler>();
    private void DecreaseMomentumLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<MomentumVectorScaler>();
    private void ToggleMomentumVectors() => _simulationControls.ToggleVectors<MomentumVectorScaler>();
    private void IncreaseMomentumLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<MomentumVectorScaler>();
    private void DecreaseForceLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<ForceVectorScaler>();
    private void ToggleForceVectors() => _simulationControls.ToggleVectors<ForceVectorScaler>();
    private void IncreaseForceLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<ForceVectorScaler>();

    private void DecreaseSimulationSpeed() => _simulationControls.DecreaseSimulationSpeed();
    private void ToggleSimulationSpeed() => _simulationControls.PauseUnpauseSimulation();
    private void IncreaseSimulationSpeed() => _simulationControls.IncreaseSimulationSpeed();
    private void SimulationPredict() => _predictor.Predict();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ToggleSimulationUI();
        }

        UpdateRealtimeUI();
    }

    ///<summary>Updates all UI elements which require frequent updates</summary>
    private void UpdateRealtimeUI()
    {
        // statistics
        _energyKinetic.text = _statistics.kineticEnergy.ToString();
        _energyPotential.text = _statistics.potentialEnergy.ToString();
        _energyTotal.text = _statistics.totalEnergy.ToString();

        _energyTotalMaximal.text = _statistics.maxTotalenergy.ToString();
        _energyTotalMinimal.text = _statistics.minTotalEnergy.ToString();

        _totalMomentum.text = _statistics.momentum.ToString();

        // simulation properties
        _simulationSpeed.value.text = _simulationControls.rememberedSimulationSpeed.ToString();

        // vectors
        _velocityVectors.value.text = _simulationControls.velocityVectorsLength.ToString();
        _momentumVectors.value.text = _simulationControls.momentumVectorsLength.ToString();
        _forceVectors.value.text = _simulationControls.forceVectorsLength.ToString();
    }

    private void OnDisable()
    {
        _toggleSimulationUI.clicked -= ToggleSimulationUI;

        _previousScene.clicked -= LoadPreviousScene;
        _nextScene.clicked -= LoadNextScene;

        // Unregister click event callbacks for vectors length buttons
        _velocityVectors.down.clicked -= DecreaseVelocityLengthMultiplier;
        _velocityVectors.toggle.clicked -= ToggleVelocityVectors;
        _velocityVectors.up.clicked -= IncreaseVelocityLengthMultiplier;
        _momentumVectors.down.clicked -= DecreaseMomentumLengthMultiplier;
        _momentumVectors.toggle.clicked -= ToggleMomentumVectors;
        _momentumVectors.up.clicked -= IncreaseMomentumLengthMultiplier;
        _forceVectors.down.clicked -= DecreaseForceLengthMultiplier;
        _forceVectors.toggle.clicked -= ToggleForceVectors;
        _forceVectors.up.clicked -= IncreaseForceLengthMultiplier;

        // Unregister click event callbacks for simulation speed buttons
        _simulationSpeed.down.clicked -= DecreaseSimulationSpeed;
        _simulationSpeed.toggle.clicked -= ToggleSimulationSpeed;
        _simulationSpeed.up.clicked -= IncreaseSimulationSpeed;
        _predict.clicked -= SimulationPredict;
    }
}