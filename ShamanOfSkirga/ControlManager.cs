using UnityEngine;
using System.Collections;
/*
* author: Benjamin Jugl
* Script to handle the controlls in the Main Menu of Shaman of Skirga
*/
public class ControlManager : MonoBehaviour
{

		public enum InputType
		{
				idle,
				down,
				up,
				a,
				b,
				scrolldown,
				scrollup,
				left,
				right
		}
		;

		public MainMenu mainMenu;
		public MenuManager menuManager;
		public SubMenuStart subMenuStart;
		public SubMenuQuit subMenuQuit;
		public SubMenuOptions subMenuOptions;
		public SubMenuReso	subMenuReso;
		public SubMenuSound subMenuSound;
		public bool mouseSelection;
		private float delta;
		private Vector3 mousePosition = new Vector3 ();
		public InputType thisInput = new InputType ();



		// Use this for initialization
		void Start ()
		{
				mouseSelection = true;
				delta = 0f;
				thisInput = InputType.idle;
		}
	
		// Update is called once per frame
		void Update ()
		{

				//Destinction betweenMouse OR /Pad usage
				Screen.showCursor = mouseSelection;
				if (!mouseSelection)
						mouseSelection = IsMouseUsed ();

				mousePosition = Input.mousePosition;
				if (mouseSelection) {
						if (IsPadUsed ()) {
								mouseSelection = false;
						}
				}
		
				if (!mouseSelection)
						getInput ();
				else
						getMouseInput ();
		
				if (delta > 0.4f) {
						HandleInput ();
						delta = 0f;
				}
	
				delta += Time.deltaTime;
		}

		public void getInput ()
		{
				 
				//Input down
				if (Input.GetKeyDown (KeyCode.DownArrow)
						|| Input.GetKeyDown (KeyCode.S)
						|| Input.GetAxis ("Vertical") > 0.85) {
						thisInput = InputType.down;
								
				}
				//Input up
				if (Input.GetKeyDown (KeyCode.UpArrow)
						|| Input.GetKeyDown (KeyCode.W)
						|| Input.GetAxis ("Vertical") < - 0.85) {
						thisInput = InputType.up;
								
				}
				//Input left
				if (Input.GetKeyDown (KeyCode.LeftArrow)
						|| Input.GetKeyDown (KeyCode.D)
						|| Input.GetAxis ("Horizontal") < -  0.85) {
						thisInput = InputType.left;
						//Debug.Log("Left");

				}
				//input right
				if (Input.GetKeyDown (KeyCode.RightArrow)
						|| Input.GetKeyDown (KeyCode.D)
						|| Input.GetAxis ("Horizontal") > 0.85) {
						thisInput = InputType.right;
						//Debug.Log("Right");
			
				}
				//Input a
				if (Input.GetKeyDown (KeyCode.Joystick1Button0)
						|| Input.GetKeyDown (KeyCode.Return)) {
						thisInput = InputType.a;
								
				}
				//Input b
				if (Input.GetKeyDown (KeyCode.Escape)
						|| Input.GetKeyDown (KeyCode.Joystick1Button1)) { 
						thisInput = InputType.b;
								
				}
				
		}

		//For the Mousebehaviours that are not handled in the Menu- / Button- Scripts
		public void getMouseInput ()
		{
				//Abfrage Scrollwheeldown
				if (Input.GetAxis ("Mouse ScrollWheel") < 0)
						thisInput = InputType.scrolldown;
				//Abfrage Scrollwheelup
				if (Input.GetAxis ("Mouse ScrollWheel") > 0)
						thisInput = InputType.scrollup;
		}

		void HandleInput ()
		{
				switch (thisInput) {
				case InputType.down:
						DownKey ();
						break;
				case InputType.up:
						UpKey ();
						break;
				case InputType.a:
						AKey ();
						break;
				case InputType.b:
						BKey ();
						break;
				case InputType.scrolldown:
						ScrollDown ();
						break;
				case InputType.scrollup:
						ScrollUp ();
						break;
				case InputType.idle:
						break;
				case InputType.left:
						LeftKey ();
						break;
				case InputType.right:
						RightKey ();
						break;
				}
		
				thisInput = InputType.idle;
		}

		private void DownKey ()
		{
				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						mainMenu.DownKey ();
						break;
				case MenuManager.ActiveMenu.startNewGame:
						break;
				case MenuManager.ActiveMenu.optionsMenu:
						subMenuOptions.DownKey();
						break;
				case MenuManager.ActiveMenu.resolution:
						subMenuReso.DownKey();
						break;
				case MenuManager.ActiveMenu.sound:
			subMenuSound.DownKey();
						break;
				case MenuManager.ActiveMenu.quit:
						break;
				}
		
		}
	
		private void UpKey ()
		{
				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						mainMenu.UpKey ();
						break;
				case MenuManager.ActiveMenu.startNewGame:
						break;
				case MenuManager.ActiveMenu.optionsMenu:
						subMenuOptions.UpKey();
						break;
				case MenuManager.ActiveMenu.resolution:
						subMenuReso.UpKey();
						break;
				case MenuManager.ActiveMenu.sound:
						subMenuSound.UpKey();
						break;
				case MenuManager.ActiveMenu.quit:
						break;
				}
		}
	
		void AKey ()
		{
				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						mainMenu.AKey ();
						break;
				case MenuManager.ActiveMenu.startNewGame:
						subMenuStart.AKey ();
						break;
				case MenuManager.ActiveMenu.optionsMenu:
						subMenuOptions.AKey();
						break;
				case MenuManager.ActiveMenu.resolution:
						subMenuReso.AKey();
						break;
				case MenuManager.ActiveMenu.sound:
						subMenuSound.AKey();
						break;
				case MenuManager.ActiveMenu.quit:
						subMenuQuit.AKey ();
						break;
				}
		}
	
		void BKey ()
		{
//		menuManager.getMenu(menuManager.thisMenu).BKey();

				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						mainMenu.BKey ();
						break;
				case MenuManager.ActiveMenu.startNewGame:
						subMenuStart.BKey ();
						break;
				case MenuManager.ActiveMenu.optionsMenu:
						subMenuOptions.BKey ();
						break;
				case MenuManager.ActiveMenu.resolution:
						subMenuReso.BKey();
						break;
				case MenuManager.ActiveMenu.sound:
						subMenuSound.BKey ();
						break;
				case MenuManager.ActiveMenu.quit:
						subMenuQuit.BKey ();
						break;
				}
		}
	
		void ScrollDown ()
		{
		if (menuManager.thisMenu == MenuManager.ActiveMenu.main)
						mainMenu.ScrollDown ();
		}
	
		void ScrollUp ()
		{
		if (menuManager.thisMenu == MenuManager.ActiveMenu.main)
						mainMenu.ScrollUp ();
		}

		void LeftKey ()
		{
				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						break;
				case MenuManager.ActiveMenu.startNewGame:
						subMenuStart.LeftKey ();
						break;
				case MenuManager.ActiveMenu.sound:
						subMenuSound.LeftKey();
						break;
				case MenuManager.ActiveMenu.quit:
						subMenuQuit.LeftKey ();
						break;
		case MenuManager.ActiveMenu.resolution:
			subMenuReso.LeftKey();
			break;
				}
		}

		void RightKey ()
		{
				switch (menuManager.thisMenu) {
				case MenuManager.ActiveMenu.main:
						break;
				case MenuManager.ActiveMenu.startNewGame:
						subMenuStart.RightKey ();
						break;
				case MenuManager.ActiveMenu.sound:
						subMenuSound.RightKey();
						break;
				case MenuManager.ActiveMenu.quit:
						subMenuQuit.RightKey ();
			break;
		case MenuManager.ActiveMenu.resolution:
			subMenuReso.RightKey();
			break;
				}
		}
	
		//method to determine if keyboard/pad is used
		public bool IsPadUsed ()
		{

				return  Input.GetAxis ("Vertical") >= 0.5f
						|| Input.GetAxis ("Vertical") <= -0.5f
						|| Input.GetAxis ("Horizontal") <= -0.5f
						|| Input.GetAxis ("Horizontal") <= -0.5f
						|| Input.GetKeyDown (KeyCode.UpArrow)
						|| Input.GetKeyDown (KeyCode.DownArrow)
						|| Input.GetKeyDown (KeyCode.RightArrow)
						|| Input.GetKeyDown (KeyCode.LeftArrow)
						|| Input.GetKeyDown (KeyCode.W)
						|| Input.GetKeyDown (KeyCode.S)
						|| Input.GetKeyDown (KeyCode.D)
						|| Input.GetKeyDown (KeyCode.A)
						|| Input.GetKeyDown (KeyCode.Joystick1Button0)
						|| Input.GetKeyDown (KeyCode.Return)
						|| Input.GetKeyDown (KeyCode.Escape)
						|| Input.GetKeyDown (KeyCode.Joystick1Button1)
						|| Input.GetKeyDown (KeyCode.Joystick1Button2)
						|| Input.GetKeyDown (KeyCode.Joystick1Button3)
						|| Input.GetKeyDown (KeyCode.Joystick1Button4);
		}

		//method to determine if mouse is used
		private bool IsMouseUsed ()
		{
				return !(Vector3.SqrMagnitude (mousePosition - Input.mousePosition) < 9.99999944E-11f);

		}
}
