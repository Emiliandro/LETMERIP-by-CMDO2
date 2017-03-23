using UnityEngine;
using System.Collections;

public static class ControllerMaps {

	
	public static Hashtable Joystick = new Hashtable () {
		{ "Start", "3" },
		{ "Select", "0" },
		{ "Crouch", "10" },
		{ "UseItem", "12" },
		{ "Punch", "13" },
		//{ "Fire", "14" },
		{ "Fire", "9" },
		{ "Run", "15" },
		{ "Lock", "10" }, //FIXME: Select another keycode
		{ "MoveLeft", "8"},
		{ "MoveRight", "9"}
	};
}

public class InputController : MonoBehaviour {

	static public InputController instance;

	const int MAX_SLOTS = 4;
	
    [SerializeField]
	public PlayerInput[] playerInputSlots = new PlayerInput[MAX_SLOTS];

	private string[] currentDevicesConnected;

	void Awake () {

        foreach(string i in Input.GetJoystickNames())
        {
            print(i);
        }


		Debug.Log("InputController Init() " + Input.GetJoystickNames().Length);

		if (instance == null) {
			instance = this;
		}

		currentDevicesConnected = Input.GetJoystickNames();


		//Initialize Input slots
		for (int i = 0; i < MAX_SLOTS; i++) {
			playerInputSlots[i] = new PlayerInput();
			playerInputSlots[i].Init(i+1, ControllerMaps.Joystick); // idx must to be greater than zero;
		}

	}

	// Use this for initialization
	void Start () {
		ShowJoysticksConnected ();
	}
	
	// Update is called once per frame
	void Update () {
        if (UnpluggedJoystick()){
            print("Unplugged");
        }
		currentDevicesConnected = Input.GetJoystickNames();
        ShowJoysticksConnected();
	}

    bool UnpluggedJoystick(){
        string[] oldJoysticks = currentDevicesConnected;
        string[] newJoysticks = Input.GetJoystickNames();

        return oldJoysticks.Length != newJoysticks.Length;

    }


    void ShowJoysticksConnected () {

		for(int i = 0; i < currentDevicesConnected.Length; i++) {
			Debug.Log("Joystick" + (i + 1) + " = " + currentDevicesConnected[i]);
		}
	}

	public int getAmountOfControllersConnected () {
	
		return currentDevicesConnected.Length;
	}

	public PlayerInput GetPlayerInput (int slot) {
		return playerInputSlots[slot];
	}

	public PlayerInput[] GetAllInputs () {
		return playerInputSlots;
	}
}
