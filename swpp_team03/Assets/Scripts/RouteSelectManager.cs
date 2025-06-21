using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class RouteSelectManager : MonoBehaviour
{
    public GameObject[] maps;                  
    public string[] routeDescriptions;
    public TextMeshProUGUI routeDescription;
    public TextMeshProUGUI recordText;
    public Button startButton;                 

    private int selectedRouteIndex = -1;       

    public void OnHoverRoute(int index)
    {
        for (int i = 0; i < maps.Length; i++)
            maps[i].SetActive(i == index);

        routeDescription.text = routeDescriptions[index];
        selectedRouteIndex = index;

        float time = LoadBestTime($"route{index+1}");
        if (time == -1)
        {
            recordText.text = "Best Record : None";
        }
        else
        {
            recordText.text = $"Best Record : {time}";
        }

        startButton.gameObject.SetActive(true);
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => StartRoute(index));
    }

    float GetRouteTime(RouteTimeData data, string routeName)
    {
        return routeName switch
        {
            "route1" => data.route1,
            "route2" => data.route2,
            "route3" => data.route3,
            "route4" => data.route4,
            "route5" => data.route5,
            "route6" => data.route6,
            _ => -1
        };
    }

    float LoadBestTime(string routeName)
    {
        string path = Path.Combine(Application.persistentDataPath, "times.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            RouteTimeData data = JsonUtility.FromJson<RouteTimeData>(json);
            return GetRouteTime(data, routeName);
        }

        return -1f;
    }

    void StartRoute(int index)
    {
        // Store selected route in RouteManager
        if (RouteManager.Instance != null)
        {
            RouteManager.Instance.selectedRoute = index;
            RouteManager.Instance.route = index;
        }

        // Load shared gameplay scene
        SceneManager.LoadScene("Scene1"); // Replace with your actual scene name
    }
}
