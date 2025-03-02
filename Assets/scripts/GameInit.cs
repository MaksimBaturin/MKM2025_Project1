using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Unity.VisualScripting;

public class GameInit : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject startPad;
    [SerializeField] private GameObject endPad;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject pointer;

    private RaycastHit2D hit;

    void Start()
    {
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        //terrain
        Instantiate(terrain, new Vector3(-4099f, 0, 0), Quaternion.identity);

        //startPad
        yield return PerformRaycast(new Vector2(0, 500f));
        Instantiate(startPad, hit.point.ConvertTo<Vector3>() + new Vector3(0, 150, 0), Quaternion.identity);

        //endPad
        System.Random rand = new System.Random();
        Vector3 endPadPos = new Vector3(rand.Next(2000, 3000), 500f);
        int direction = rand.Next(-1, 1);
        if (direction == -1) endPadPos.x *= -1;

        yield return PerformRaycast(endPadPos);
        Instantiate(endPad, hit.point.ConvertTo<Vector3>() + new Vector3(0, 150, 0), Quaternion.identity);

        //rocket
        Vector3 rocketSpawnPosition = GameObject.Find("RocketSpawn").transform.position;
        Instantiate(rocket, rocketSpawnPosition, Quaternion.identity);

        //camera
        Vector3 cameraPosition = rocketSpawnPosition - new Vector3(0, 0, 10);
        Instantiate(_camera, cameraPosition, Quaternion.identity);

        //UI
        Instantiate(ui);

        //pointer
        Instantiate(pointer, rocketSpawnPosition, Quaternion.identity);
    }

    private IEnumerator PerformRaycast(Vector2 rayPos)
    {
        yield return null;
        hit = Physics2D.Raycast(rayPos, -Vector2.up, 2000f, Physics.DefaultRaycastLayers, -1f);
    }

}