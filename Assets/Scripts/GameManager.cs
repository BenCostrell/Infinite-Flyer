using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject ship;
    public GameObject timePassedUI;
    public GameObject messageUI;
    public float realTimePerSection;
    public float pauseTime;

    private Text timePassedText;
    public Text messageText;

    private int timePassed;
    private float realTimePerTimePass;
    private float timeSinceLastTimePass;
    public bool paused;

    private TimeScale currentTimeScale;

    // Use this for initialization
	void Start () {
        InitializeServices();
        timePassedText = timePassedUI.GetComponent<Text>();
        messageText = messageUI.GetComponent<Text>();
        messageUI.SetActive(false);
        InitializeTimeScaleObjects();
        timePassed = 0;
        timeSinceLastTimePass = 0;
        realTimePerTimePass = realTimePerSection / currentTimeScale.numUnitsUntilNextTimeScale;
        paused = false;
	}

    // Update is called once per frame
    void Update()
    {
        Services.TaskManager.Update();
        if (!paused)
        {
            if (timeSinceLastTimePass >= realTimePerTimePass)
            {
                PassTime();
                timeSinceLastTimePass = 0;
            }
            else
            {
                timeSinceLastTimePass += Time.deltaTime;
            }
        }
	}

    void InitializeServices()
    {
        Services.GameManager = this;
        Services.TaskManager = new TaskManager();
    }

    void InitializeTimeScaleObjects()
    {
        TimeScale seconds = new TimeScale("seconds elapsed", 60, 
            "oh boy, explorin the universe");
        TimeScale minutes = new TimeScale("minutes elapsed", 60, 
            "can't wait to learn the secrets of reality");
        TimeScale hours = new TimeScale("hours elapsed", 24, 
            "check out the view");
        TimeScale days = new TimeScale("days elapsed", 30, 
            "got any threes?");
        TimeScale months = new TimeScale("months elapsed", 12, 
            "holy shit it's Mars");
        TimeScale years = new TimeScale("years elapsed", 10, 
            "at long last... it's pluto");
        TimeScale decades = new TimeScale("decades elapsed", 10, 
            "RIP, you died. your child will continue searching the cosmos");
        TimeScale lifetimes = new TimeScale("generations passed", 30, 
            "we honor the ancient founders of our society who began this journey");
        TimeScale civilizations = new TimeScale("civilizations fallen", 10, 
            "");
        TimeScale recordedHumanHistories = new TimeScale("human histories recorded", 20, 
            "humans on earth have evolved to the point where they are noticeably different looking");
        TimeScale anatomicalEvolutionaryDivergences = new TimeScale("anatomical evolutionary divergences", 30, 
            "there is no such species as human on earth anymore");
        TimeScale speciesDivergences = new TimeScale("new species evolved", 3,
            "difference between you and dominant earth species is like difference between mammals and reptiles");
        TimeScale animalKingdomDivergences = new TimeScale("new animal kingdoms evolved", 3, 
            "life on earth is almost unrecognizable");
        TimeScale lifeformDivergences = new TimeScale("new forms of life evolved", 8, 
            "life on earth no longer exists, the sun exploded, also the milky way collided with the andromeda galaxy");
        TimeScale starFormations = new TimeScale("suns formed from nothing", 3, 
            "who knows? maybe the universe has collapsed in on itself or something");
        TimeScale universeLifetimes = new TimeScale("lifetimes of the universe", 1, 
            "");

        seconds
            .Then(minutes)
            .Then(hours)
            .Then(days)
            .Then(months)
            .Then(years)
            .Then(decades)
            .Then(lifetimes)
            .Then(civilizations)
            .Then(recordedHumanHistories)
            .Then(anatomicalEvolutionaryDivergences)
            .Then(speciesDivergences)
            .Then(animalKingdomDivergences)
            .Then(lifeformDivergences)
            .Then(starFormations)
            .Then(universeLifetimes);

        currentTimeScale = seconds;
    }

    public void SetShipVelocity(float vel)
    {
        ship.GetComponent<Rigidbody2D>().velocity = vel * Vector2.right;       
    }

    void PassTime()
    {
        timePassed += 1;
        CheckAndUpdateTimeScale();
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        timePassedText.text = currentTimeScale.unit + ": " + timePassed;
    }

    void CheckAndUpdateTimeScale()
    {
        if (timePassed == currentTimeScale.numUnitsUntilNextTimeScale)
        {
            PauseForMessage();
            currentTimeScale = currentTimeScale.nextTimeScale;
            realTimePerTimePass = realTimePerSection / currentTimeScale.numUnitsUntilNextTimeScale;
            timePassed = 1;
        }
    }

    void PauseForMessage()
    {
        PauseForMessage pause = new PauseForMessage(currentTimeScale);
        Services.TaskManager.AddTask(pause);
    }
}
