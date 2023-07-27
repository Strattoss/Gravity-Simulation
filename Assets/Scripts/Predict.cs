// Script written with help from: https://youtu.be/p8e4Kpl9b28 //


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Predictor : MonoBehaviour
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private GameObject _realCelestialBodiesParent;
    private GameObject _ghostCelestialBodiesParent;
    private GameObject _realSimulationManagerGameObject;
    private GameObject _ghostSimulationManagerGameObject;
    private readonly Dictionary<Gravitybody, Gravitybody> ghostToReal = new Dictionary<Gravitybody, Gravitybody>();

    public int predictSteps = 1000;

    void Start() {
        _realCelestialBodiesParent = GameObject.FindGameObjectWithTag("CelestialBodiesParent");
        _realSimulationManagerGameObject = GameObject.FindGameObjectWithTag("SimulationManager");
    }

    public void Predict()
    {
        // var startTime = Time.realtimeSinceStartup;
        CreateGhostScene();
        PredictPaths();
        DeleteGhostScene();
        // var endTime = Time.realtimeSinceStartup;
        // Debug.Log("Measured time: " + (endTime - startTime));
    }

    void CreateGhostScene()
    {
        _simulationScene = SceneManager.CreateScene("PredictingSimulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
        Physics.simulationMode = SimulationMode.Script;

        _ghostCelestialBodiesParent = Instantiate(_realCelestialBodiesParent);
        SceneManager.MoveGameObjectToScene(_ghostCelestialBodiesParent, _simulationScene);

        Gravitybody[] realGravitybodies = _realCelestialBodiesParent.GetComponentsInChildren<Gravitybody>();
        Gravitybody[] ghostGravitybodies = _ghostCelestialBodiesParent.GetComponentsInChildren<Gravitybody>();
        for (int i = 0; i < realGravitybodies.Length; i++)
        {
            ghostToReal[ghostGravitybodies[i]] = realGravitybodies[i];
        }

        _ghostSimulationManagerGameObject = Instantiate(_realSimulationManagerGameObject);
        SceneManager.MoveGameObjectToScene(_ghostSimulationManagerGameObject, _simulationScene);
        _ghostSimulationManagerGameObject.GetComponent<GravityManager>().celestialBodiesParent = _ghostCelestialBodiesParent;
    }

    void PredictPaths()
    {
        // prepare line renderers
        foreach (Gravitybody gb in _realSimulationManagerGameObject.GetComponent<GravityManager>().gravitybodies)
        {
            gb.GetComponent<LineRenderer>().positionCount = 0;
            gb.GetComponent<LineRenderer>().positionCount = predictSteps;
        }

        for (int i = predictSteps - 1; i >= 0; i--)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);

            _ghostSimulationManagerGameObject.GetComponent<GravityManager>().DoScenewideVerletStep();

            foreach (Gravitybody ghostGb in _ghostSimulationManagerGameObject.GetComponent<GravityManager>().gravitybodies)
            {
                ghostToReal[ghostGb].GetComponent<LineRenderer>().SetPosition(i, ghostGb.position);
            }
        }

        // trim prediction lines (if a body was destroyed, the prediction line should end on body's last position)
        foreach (var gb in _realSimulationManagerGameObject.GetComponent<GravityManager>().gravitybodies)
        {
            gb.GetComponent<Gravitybody>().TrimPredictionLine();
        }
    }

    void DeleteGhostScene()
    {
        Destroy(_ghostSimulationManagerGameObject);
        Destroy(_ghostCelestialBodiesParent);
        Physics.simulationMode = SimulationMode.FixedUpdate;
        SceneManager.UnloadSceneAsync(_simulationScene);
    }
}
