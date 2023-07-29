using UnityEngine;
using UnityEngine.UIElements;

public class SimulationUI : MonoBehaviour
{
    private SimulationControls _simulationControls;
    private Statistics _statistics;

    // statistics
    private Label _energyKineticLabel;
    private Label _energyPotentialLabel;
    private Label _energyTotalLabel;

    // simulation properties
    private Label _simulationSpeed;
    private Button _simulationSpeedUp;
    private Button _simulationSpeedDown;

    // vectors
    private Label _velocityLength;
    private Button _velocityLengthUp;
    private Button _velocityLengthDown;
    private Label _momentumLength;
    private Button _momentumLengthUp;
    private Button _momentumLengthDown;
    private Label _forceLength;
    private Button _forceLengthUp;
    private Button _forceLengthDown;



    private void OnEnable()
    {
        _simulationControls = GameObject.FindGameObjectWithTag("SimulationController").GetComponent<SimulationControls>();
        _statistics = GameObject.FindGameObjectWithTag("SimulationManager").GetComponent<Statistics>();


        var uiDocument = GetComponent<UIDocument>();

        var statistics = uiDocument.rootVisualElement.Q("statistics");
        BindStatistics(statistics);

        var vectors = uiDocument.rootVisualElement.Q("vectors");
        BindVectors(vectors);

        var simulationProperties = uiDocument.rootVisualElement.Q("simulation-properties");
        BindSimulationProperties(simulationProperties);
    }

    private void BindStatistics(VisualElement statistics)
    {
        _energyKineticLabel = statistics.Q<Label>("energy-kinetic");
        _energyPotentialLabel = statistics.Q<Label>("energy-potential");
        _energyTotalLabel = statistics.Q<Label>("energy-total");
    }

    private void BindSimulationProperties(VisualElement simulationProperties)
    {
        _simulationSpeed = simulationProperties.Q<Label>("simulation-speed");

        _simulationSpeedDown = simulationProperties.Q<Button>("simulation-speed-down");
        _simulationSpeedDown.clicked += DecreaseSimulationSpeed;

        _simulationSpeedUp = simulationProperties.Q<Button>("simulation-speed-up");
        _simulationSpeedUp.clicked += IncreaseSimulationSpeed;
    }

    private void BindVectors(VisualElement vectors)
    {
        // labels
        _velocityLength = vectors.Q<Label>("velocity-length");
        _momentumLength = vectors.Q<Label>("momentum-length");
        _forceLength = vectors.Q<Label>("force-length");

        // buttons
        _velocityLengthDown = vectors.Q<Button>("velocity-length-down");
        _velocityLengthDown.clicked += DecreaseVelocityLengthMultiplier;
        _velocityLengthUp = vectors.Q<Button>("velocity-length-up");
        _velocityLengthUp.clicked += IncreaseVelocityLengthMultiplier;

        _momentumLengthDown = vectors.Q<Button>("momentum-length-down");
        _momentumLengthDown.clicked += DecreaseMomentumLengthMultiplier;
        _momentumLengthUp = vectors.Q<Button>("momentum-length-up");
        _momentumLengthUp.clicked += IncreaseMomentumLengthMultiplier;

        _forceLengthDown = vectors.Q<Button>("force-length-down");
        _forceLengthDown.clicked += DecreaseForceLengthMultiplier;
        _forceLengthUp = vectors.Q<Button>("force-length-up");
        _forceLengthUp.clicked += IncreaseForceLengthMultiplier;
    }

    private void DecreaseVelocityLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<VelocityVectorScaler>();
    private void IncreaseVelocityLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<VelocityVectorScaler>();
    private void DecreaseMomentumLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<MomentumVectorScaler>();
    private void IncreaseMomentumLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<MomentumVectorScaler>();
    private void DecreaseForceLengthMultiplier() => _simulationControls.DecreaseVectorsLengthMultiplier<ForceVectorScaler>();
    private void IncreaseForceLengthMultiplier() => _simulationControls.IncreaseVectorsLengthMultiplier<ForceVectorScaler>();
    private void DecreaseSimulationSpeed() => _simulationControls.DecreaseSimulationSpeed();
    private void IncreaseSimulationSpeed() => _simulationControls.IncreaseSimulationSpeed();


    private void Update()
    {
        UpdateRealtimeUI();
    }

    ///<summary>Updates all UI elements which require frequent updates</summary>
    private void UpdateRealtimeUI()
    {
        // statistics
        _energyKineticLabel.text = _statistics.kineticEnergy.ToString();
        _energyPotentialLabel.text = _statistics.potentialEnergy.ToString();
        _energyTotalLabel.text = _statistics.totalEnergy.ToString();

        // simulation properties
        _simulationSpeed.text = _simulationControls.rememberedSimulationSpeed.ToString();

        // vectors
        _velocityLength.text = _simulationControls.velocityVectorsLength.ToString();
        _momentumLength.text = _simulationControls.momentumVectorsLength.ToString();
        _forceLength.text = _simulationControls.forceVectorsLength.ToString();
    }

    private void OnDisable()
    {
        // Unregister click event callbacks for vectors length buttons
        _velocityLengthDown.clicked -= DecreaseVelocityLengthMultiplier;
        _velocityLengthUp.clicked -= IncreaseVelocityLengthMultiplier;
        _momentumLengthDown.clicked -= DecreaseMomentumLengthMultiplier;
        _momentumLengthUp.clicked -= IncreaseMomentumLengthMultiplier;
        _forceLengthDown.clicked -= DecreaseForceLengthMultiplier;
        _forceLengthUp.clicked -= IncreaseForceLengthMultiplier;

        // Unregister click event callbacks for simulation speed buttons
        _simulationSpeedDown.clicked -= DecreaseSimulationSpeed;
        _simulationSpeedUp.clicked -= IncreaseSimulationSpeed;
    }
}