using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    public int lives;
    public TextMeshProUGUI livesUi;

    private void Update()
    {
        TrackLives lifeTracker = FindObjectOfType<TrackLives>();
        lives = lifeTracker.numLives;
        livesUi.text = "Lives Left: " + lives.ToString();
    }
}
