using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject ship;

    public GameObject timePassedUI;
    private Text timePassedText;

    private int timePassed;
    public float realTimePerSection;
    private float realTimePerTimePass;
    private float timeSinceLastTimePass;

    private TimeScale currentTimeScale;

    // Use this for initialization
	void Start () {
        timePassedText = timePassedUI.GetComponent<Text>();
        InitializeTimeScaleObjects();
        timePassed = 0;
        timeSinceLastTimePass = 0;
        realTimePerTimePass = realTimePerSection / currentTimeScale.numUnitsUntilNextTimeScale;
	}

    // Update is called once per frame
    void Update()
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

    void InitializeTimeScaleObjects()
    {
        TimeScale seconds = new TimeScale("seconds elapsed", 60, "");
        TimeScale minutes = new TimeScale("minutes elapsed", 60, "");
        TimeScale hours = new TimeScale("hours elapsed", 24, "");
        TimeScale days = new TimeScale("days elapsed", 30, "");
        TimeScale months = new TimeScale("months elapsed", 12, "");
        TimeScale years = new TimeScale("years elapsed", 10, "");
        TimeScale decades = new TimeScale("decades elapsed", 10, "");
        TimeScale lifetimes = new TimeScale("lifetimes passed", 10, "");
        TimeScale civilizations = new TimeScale("civilizations fallen", 10, "");
        TimeScale recordedHumanHistories = new TimeScale("human histories recorded", 20, "");
        TimeScale anatomicalEvolutionaryDivergences = new TimeScale("anatomical evolutionary divergences", 30, "");
        TimeScale speciesDivergences = new TimeScale("new species evolved", 3, "");
        TimeScale animalKingdomDivergences = new TimeScale("new animal kingdoms evolved", 3, "");
        TimeScale lifeformDivergences = new TimeScale("new forms of life evolved", 8, "");
        TimeScale starFormations = new TimeScale("suns formed from nothing", 3, "");
        TimeScale universeLifetimes = new TimeScale("lifetimes of the universe", 1, "");

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
        Debug.Log(currentTimeScale);
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
            currentTimeScale = currentTimeScale.nextTimeScale;
            realTimePerTimePass = realTimePerSection / currentTimeScale.numUnitsUntilNextTimeScale;
            timePassed = 1;
        }
    }
}
