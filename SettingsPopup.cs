using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {
	[SerializeField] private Slider speedSlider;
	
	void Start() {
		speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) { //quit

            //prompt save
            Close();
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.O)) { //start
            Close();
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.RightAlt)) { //resume
            //load saved game
            Close();
            Application.Quit();
        }
    }

    public void Open() {
		gameObject.SetActive(true);
	}
	public void Close() {
		gameObject.SetActive(false);
	}

	public void OnSubmitName(string name) {
		Debug.Log(name);
	}
	
	public void OnSpeedValue(float speed) {
		Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
		PlayerPrefs.SetFloat("speed", speed);
	}
}
