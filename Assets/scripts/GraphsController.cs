using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GraphsController : MonoBehaviour
{
    [SerializeField] private GraphHandler graphHandlerVelocity;
    [SerializeField] private GraphHandler graphHandlerTsiolkovsky;
    [SerializeField] private GraphHandler graphHandlerFuelMass;
    [SerializeField] private Rocket rocket;

    [SerializeField] private float delayBeforeShowingLoadData = 0.2f;
    public Vector2 VelocityGraphPosition = new Vector2(750, 100);
    public Vector2 PositionGraphPosition = new Vector2(750, 225);
    public Vector2 FuelMassGraphPosition = new Vector2(750, 225);

    void Start()
    {
        StartCoroutine(LoadGraphData());
    }

    private System.Collections.IEnumerator LoadGraphData()
    {
        yield return new WaitForSeconds(delayBeforeShowingLoadData);

        graphHandlerVelocity = GameObject.Find("GraphHandlerVelocity").GetComponent<GraphHandler>();
        graphHandlerTsiolkovsky = GameObject.Find("GraphHandlerTsiolkovsky").GetComponent<GraphHandler>();
        graphHandlerFuelMass = GameObject.Find("GraphHandlerFuelMass").GetComponent<GraphHandler>();

        if (graphHandlerVelocity != null)
        {
            graphHandlerVelocity.SetCornerValues(new Vector2(-15, -15), new Vector2(30, 128));
            RectTransform graphRectTransform = graphHandlerVelocity.GetComponent<RectTransform>();
            graphRectTransform.anchoredPosition = VelocityGraphPosition;
        }
        if (graphHandlerTsiolkovsky != null)
        {
            graphHandlerTsiolkovsky.SetCornerValues(new Vector2(-15, -15), new Vector2(30, 128));
            RectTransform graphRectTransform = graphHandlerTsiolkovsky.GetComponent<RectTransform>();
            graphRectTransform.anchoredPosition = PositionGraphPosition;
        }
        if (graphHandlerFuelMass != null)
        {
            graphHandlerFuelMass.SetCornerValues(new Vector2(-15, -15), new Vector2(30, 35000));
            RectTransform graphRectTransform = graphHandlerFuelMass.GetComponent<RectTransform>();
            graphRectTransform.anchoredPosition = FuelMassGraphPosition;
        }

        string path = Path.Combine(Application.persistentDataPath, "rocket_data.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            RocketData rocketData = JsonUtility.FromJson<RocketData>(json);

            List<Vector2> velocityPoints = new List<Vector2>();
            List<Vector2> tsiolkovskyPoints = new List<Vector2>();
            List<Vector2> positionPoints = new List<Vector2>();
            List<Vector2> fuelMassPoints = new List<Vector2>();

            foreach (var dataPoint in rocketData.data)
            {
                velocityPoints.Add(new Vector2(dataPoint.time, dataPoint.velocity));
                tsiolkovskyPoints.Add(new Vector2(dataPoint.time, dataPoint.tsiolkovsky));
                fuelMassPoints.Add(new Vector2(dataPoint.time, dataPoint.fuelmass));
                //positionPoints.Add(new Vector2(dataPoint.position.x, dataPoint.position.y));
            }

            foreach (var point in velocityPoints)
            {
                graphHandlerVelocity.CreatePoint(point);
            }

            foreach (var point in tsiolkovskyPoints)
            {
                graphHandlerTsiolkovsky.CreatePoint(point);
            }
            foreach (var point in fuelMassPoints)
            {
                graphHandlerFuelMass.CreatePoint(point);
            }

            Debug.Log("Graph data loaded from " + path);
        }
        else
        {
            Debug.LogError("No saved graph data found.");
        }
    }

    [System.Serializable]
    public class RocketData
    {
        public List<RocketDataPoint> data;
    }

    [System.Serializable]
    public class RocketDataPoint
    {
        public float time;
        public float velocity;
        public float tsiolkovsky;
        public float fuelmass;
        public Vector2 position;
    }

    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
    }
} 