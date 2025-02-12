﻿using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Handles starting the game, detecting the game being won, and ending the game
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject EndingScreen = default;

    private FoundationAnchor[] FoundationAnchors;
    private CardDeck Deck;

    private void Start()
    {
        FoundationAnchors = FindObjectsOfType<FoundationAnchor>();
        Assert.AreEqual(FoundationAnchors.Length, 4, "GameManager requires 4 FoundationAnchors in the scene");

        Deck = FindObjectOfType<CardDeck>();
        Assert.IsNotNull(Deck, "GameManager needs CardDeck to be in the scene");

        Assert.IsNotNull(EndingScreen, "GameManager needs reference to EndingScreen");
    }

    public void StartGame()
    {
        Deck.StartGame();
    }

    private void Update()
    {
        //作弊指令
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)&& Input.GetKeyDown(KeyCode.H) && Input.GetKeyDown(KeyCode.K))
        {
            OnGameCompleted();
        }
    }

    public void OnFoundationChanged()
    {
        if (IsGameCompleted())
        {
            OnGameCompleted();
        }
    }

    private bool IsGameCompleted()
    {
        foreach (FoundationAnchor foundation in FoundationAnchors)
        {
            if (foundation.IsComplete == false)
            {
                return false;
            }
        }
        return true;
    }

    private void OnGameCompleted()
    {
        StartCoroutine(EndOfGameSequence());
    }

    private IEnumerator EndOfGameSequence()
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.StopTimer();
        ResultsExporter.WriteResultsToFile(timer.timeAccumulator, GameConfiguration.Instance.CheatsEnabled);

        foreach (var card in FindObjectsOfType<PlayingCard>())
        {
            var rb = card.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 1f;
            rb.AddForce(Random.onUnitSphere * 10000f);
            rb.AddTorque(Random.Range(-5000f, 5000f));
            rb.gravityScale = 50f;
        }
        yield return new WaitForSeconds(2f);
        print("执行延时函数"); 
        EndingScreen.SetActive(true);
    }
}
