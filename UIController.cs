using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private Text healthLabel;
    [SerializeField] private Text level;
    [SerializeField] private Image audio;
    [SerializeField] private Text greeting;

    string lastLevel;
    Save myData;

    private static bool win;
    private int _score;
    private int health;

	void Awake() {
		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.HEALTHNEG, OnHealthNeg);
        Messenger.AddListener(GameEvent.HEALTHPOS, OnHealthPos);
        Messenger.AddListener(GameEvent.LEVEL, OnLevelUp);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.OPEN_MENU, OnOpenSettings);
        //       Messenger.AddListener(GameEvent.AUDIO, OnAudioChange);
    }
	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.HEALTHNEG, OnHealthNeg);
        Messenger.RemoveListener(GameEvent.HEALTHPOS, OnHealthPos);
        Messenger.RemoveListener(GameEvent.LEVEL, OnLevelUp);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.OPEN_MENU, OnOpenSettings);
        //     Messenger.RemoveListener(GameEvent.AUDIO, OnAudioChange);
    }

	void Start() {
		_score = 0;
        win = false;
		scoreLabel.text = _score.ToString();
        health = 25;
        healthLabel.text = health.ToString();
        level.text = "LEVEL 1";
        lastLevel = "LEVEL 1";
		settingsPopup.Close();
	}

    void Update() {
        SaveLoad.SaveData(_score, health, lastLevel, Time.time);
    }

	private void OnEnemyHit() {
		_score += 1;
		scoreLabel.text = _score.ToString();
	}

    private void OnHealthNeg() {
        health -= 1;
        if (health >= 0)
        {
            healthLabel.text = health.ToString();
        }
        else {
        }
    }

    private void OnHealthPos() {
        health += 5;
        if (health >= 0)
        {
            healthLabel.text = health.ToString();
        }
        else { }
    }

    private void OnLevelUp() {
        level.text = "BOSS LEVEL";
        lastLevel = "BOSS LEVEL";
        win = true;//just for now
    }

    private void OnAudioChange() {
        //audio.image = "no-audio";
    }

    private void OnGameOver() {
        level.text = "GAME OVER";
        greeting.text = "You Lost. Play Again?";
        OnOpenSettings();
    }

	public void OnOpenSettings() {
		settingsPopup.Open();
	}

	public void OnPointerDown() {
		Debug.Log("pointer down");
	}
    public static bool Win() {
        return win;
    }

}
