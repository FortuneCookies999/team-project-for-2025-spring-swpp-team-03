using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouteManageInPlaying : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject routeManager;
    public int routeInt;
    private int leftCount;
    public Transform[] lightTransforms;
    public int[] routes;
    public GameObject lightObject;
    public TextMeshProUGUI leftBaseText;
	private bool isGameCleared = false;
    public GameObject gameClear;
    public TextMeshProUGUI gameClearText;
    public GameObject timeCountDown;
    private TimeCountdown timeCountdownScript;
    public string routeName;

    void Start()
    {
        timeCountdownScript = timeCountDown.GetComponent<TimeCountdown>();
        if (GameObject.Find("RouteManager_1"))
        {
            routeManager = GameObject.Find("RouteManager_1");
            int routeIndex = routeManager.GetComponent<RouteManager>().route;
            routeInt = routes[routeIndex];
            routeName = "route" + (routeIndex + 1);
            Debug.Log(routeName);
            Debug.Log(routeInt);
        }
        else
        {
            routeInt = 32;
        }
        lightObject.transform.position = lightTransforms[routeInt % 10].position;

		MiniMapNext marker = FindObjectOfType<MiniMapNext>();
		marker.target = lightObject.transform;

        leftCount = routeInt.ToString().Length;
        leftBaseText.text = $"Left Base : {leftCount}";
        routeInt = routeInt / 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        timeCountdownScript.AddTimeUsed();
        if (leftCount == 1)
        {
            Debug.Log("Game Clear!");
            gameClear.SetActive(true);
            if (timeCountdownScript.SetTimeUsed())
            {
                gameClearText.text = "Very Good! Best Driver!";
            }
            else
            {
                gameClearText.text = "Good Job, Marco!";
            }
            isGameCleared = true;
            Time.timeScale = 0f;
        }
        else
        {
            int nextindex = routeInt % 10;
            routeInt = routeInt / 10;
            leftCount--;
            leftBaseText.text = $"Left Base : {leftCount}";
            lightObject.transform.position = lightTransforms[nextindex].position;
            MiniMapNext marker = FindObjectOfType<MiniMapNext>();
            if (marker != null)
            {
                marker.target = lightObject.transform;
            }
        }
    }

	public bool IsGameCleared()
	{
		return isGameCleared;
	}
}
