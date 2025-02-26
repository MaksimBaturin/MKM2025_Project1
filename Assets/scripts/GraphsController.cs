using UnityEngine;

public class GraphsController : MonoBehaviour
{
    [SerializeField] private GraphHandler graphHandlerVelocity;
    [SerializeField] private GraphHandler graphHandlerPosition;
    [SerializeField] private Rocket rocket;

    private float lastPointTime1 = 0f;
    private float lastPointTime2 = 0f;
    public float deltaTimeForGraphs = 0.2f;

    public Vector2 VelocityGraphPosition = new Vector2(750, 100);
    public Vector2 PositionGraphPosition = new Vector2(750, 225);

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();

        graphHandlerVelocity = GameObject.Find("GraphHandlerVelocity").GetComponent<GraphHandler>();

        


        if (graphHandlerVelocity != null)
        {
            graphHandlerVelocity.SetCornerValues(new Vector2(-15, -15), new Vector2(128, 128));
            RectTransform graphRectTransform = graphHandlerVelocity.GetComponent<RectTransform>();
            graphRectTransform.anchoredPosition = VelocityGraphPosition;
        }

        graphHandlerPosition = GameObject.Find("GraphHandlerPosition").GetComponent<GraphHandler>();
        if (graphHandlerPosition != null)
        {
            //graphHandlerPosition.SetCornerValues(new Vector2(-15, -15), new Vector2(128, 128));
        }
        
    }
  
    void FixedUpdate()
    {
        RectTransform graphRectTransform = graphHandlerPosition.GetComponent<RectTransform>();
        graphRectTransform.anchoredPosition = PositionGraphPosition;

        if (graphHandlerVelocity != null && Time.time - lastPointTime1 >= deltaTimeForGraphs)
        {
            graphHandlerVelocity.CreatePoint(new Vector2(Time.time, rocket.Velocity.magnitude));
            lastPointTime1 = Time.time;
        }

        if (graphHandlerPosition != null && Time.time - lastPointTime2 >= deltaTimeForGraphs)
        {
            graphHandlerPosition.CreatePoint(rocket.Position);
            lastPointTime2 = Time.time;
        }
    }
}