using UnityEngine;
using TMPro;
using System.IO;

public class TimeCountdown : MonoBehaviour
{
    public int startMinutes = 0;
    public int startSeconds = 30;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeUsedText;

    private float _timeRemaining;
    private bool _isRunning = false;
    private float timeSpent = 0f;
    private StatusBar statusBar;
    public GameObject GameManager;

    void Start()
    {
        statusBar = FindObjectOfType<StatusBar>();

        _timeRemaining = startMinutes * 60f + startSeconds;
        _isRunning = true;
        UpdateUI();
    }

    void Update()
    {
        if (!_isRunning) return;

        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0f)
            {
                _timeRemaining = 0f;
                _isRunning = false;
                statusBar.GameOver();
                // TODO: trigger any “timer finished” event here
            }
            UpdateUI();
        }
    }

    private void UpdateUI()
    {

        float t = Mathf.Max(_timeRemaining, 0f);
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int msec = Mathf.FloorToInt((t * 1000f) % 1000f);

        // Format: "MM:SS.mmm" (e.g. "01:05.042")
        timeText.text = $"{minutes:00}:{seconds:00}.{msec:000}";
        float initialTime = startMinutes * 60f + startSeconds;
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        if (_timeRemaining > 0f)
            _isRunning = true;
    }

    public void ResetTimer()
    {
        _timeRemaining = startMinutes * 60f + startSeconds;
        _isRunning = true;
        UpdateUI();
    }

    public bool IsFinished()
    {
        return !_isRunning && _timeRemaining <= 0f;
    }

    public void SetTime(int minutes, int seconds)
    {
        // Clamp seconds to [0..59] and push overflow into minutes if needed
        int totalSeconds = minutes * 60 + Mathf.Clamp(seconds, 0, 59);

        _timeRemaining = Mathf.Max(totalSeconds, 0f);
        _isRunning = true;

        // Update inspector defaults so ResetTimer() will use these values if needed
        startMinutes = minutes + (seconds / 60);
        startSeconds = seconds % 60;

        UpdateUI();
    }

    public void AddTimeUsed()
    {
        timeSpent = timeSpent + 60f - _timeRemaining;
        _timeRemaining = 60f;
        UpdateUI();
    }

    public bool SetTimeUsed()
    {
        int mUsed = Mathf.FloorToInt(timeSpent / 60f);
        int sUsed = Mathf.FloorToInt(timeSpent % 60f);
        int msUsed = Mathf.FloorToInt((timeSpent * 1000f) % 1000f);

        timeUsedText.text = $"{mUsed:00}:{sUsed:00}.{msUsed:000}";
        return SaveTime();
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

    void SetRouteTime(RouteTimeData data, string routeName, float time)
    {
        switch (routeName)
        {
            case "route1": data.route1 = time; break;
            case "route2": data.route2 = time; break;
            case "route3": data.route3 = time; break;
            case "route4": data.route4 = time; break;
            case "route5": data.route5 = time; break;
            case "route6": data.route6 = time; break;
        }
    }

    public bool SaveTime()
    {
        string path = Path.Combine(Application.persistentDataPath, "times.json");
        RouteTimeData data;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<RouteTimeData>(json);
        }
        else
        {
            data = new RouteTimeData();
        }

        string routeName = GameManager.GetComponent<RouteManageInPlaying>().routeName;
        float existingTime = GetRouteTime(data, routeName);

        Debug.Log($"{timeSpent}, {existingTime}");
        if (existingTime < 0 || timeSpent < existingTime)
        {
            SetRouteTime(data, routeName, timeSpent);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log($"{routeName} 기록 갱신");
            return true;
        }
        return false;
    }
}
