using UnityEngine;
using System.Collections;
/*
* author: Benjamin Jugl
* Handles the Behaviour of the MainMenu
*/
public class MainMenu : Menu
{
	#region Attributs
	//for the Statemachine
	public enum ZoomState
	{
		start,
		totemZoomedIn,
		scrolledDown
	}
	;
	
	public enum MenuButton
	{
		underflow,
		none,
		startGame,
		resumeGame,
		options,
		quit,
		overflow
	}
	;
	public MenuButton thisButton;
	public ZoomState thisState = new ZoomState ();
	public Title title;
	//for Animation
	static public Animator zoomTotem = new Animator ();
	public GameObject normal;
	public GameObject mouseover;

	public GameObject ButtonsObj;
	public MainButton btnNewGame;
	public MainButton btnResume;
	public MainButton btnOptions;
	public MainButton btnQuit;
	#endregion


	#region Standard Methods	
	//initialization
	void Start ()
	{

		thisState = ZoomState.start;
		zoomTotem = gameObject.GetComponent<Animator> ();

	}
	
	void Update ()
	{

	}
	#endregion

	#region Mousebehaviours
	//Click with Statemachine
	private void OnMouseDown ()
	{
		switch (thisState) {
		case ZoomState.start:
			DoButtons();
			ZoomIn ();
			break;
		case ZoomState.totemZoomedIn:
			UndoButtons();
			ZoomOut ();
			break;
		case ZoomState.scrolledDown:
			UndoButtons();
			ZoomOut ();
			break;
		}
	}


	public void DoButtons()
	{
		menuManager.ActivateMainButtons();
	}
	public void UndoButtons(){
			menuManager.DeActivateMainButtons();
	}

	//Hovereffect
	public void OnMouseOver ()
	{
		if (control.mouseSelection) {
			if (thisState.Equals (ZoomState.start)) {
				//Start Mouseovereffekt
				Highlight ();
			}
		}
	}
	//Leaving Hover
	public void OnMouseExit ()
	{
		if (control.mouseSelection) {
			if (thisState.Equals (ZoomState.start)) {
				//End Mouseovereffekt
				Dehilight ();
			}
		}
	}
	#endregion

	#region Keybehaviours


	public override void DownKey()
	{
		switch (thisState) {
		case ZoomState.start:
			ZoomIn();
			break;
		case ZoomState.totemZoomedIn:
			thisButton++;
			switch (thisButton) {
			case(MenuButton.resumeGame):
				zoomTotem.SetTrigger ("totemScroll");
				if (thisState.Equals (ZoomState.totemZoomedIn))
					thisState = ZoomState.scrolledDown;
				else if (thisState.Equals (ZoomState.scrolledDown))
					thisState = ZoomState.totemZoomedIn;
				break;
			case(MenuButton.options):
			case(MenuButton.quit):
				zoomTotem.SetTrigger ("totemScroll");
				thisState = ZoomState.scrolledDown;	
				break;
			}
			break;
		case ZoomState.scrolledDown:
			thisButton++;
			switch (thisButton) {
			case (MenuButton.startGame):
				zoomTotem.SetTrigger ("totemScroll");
				thisState = ZoomState.totemZoomedIn;
				break;
			case(MenuButton.resumeGame):
				zoomTotem.SetTrigger ("totemScroll");
				if (thisState.Equals (ZoomState.totemZoomedIn))
					thisState = ZoomState.scrolledDown;
				else if (thisState.Equals (ZoomState.scrolledDown))
					thisState = ZoomState.totemZoomedIn;
				break;
			case(MenuButton.options):
				break;
				
			case(MenuButton.quit):
				break;
			case(MenuButton.overflow):
				thisButton = MenuButton.startGame;
				zoomTotem.SetTrigger ("totemScroll");
				thisState = ZoomState.totemZoomedIn;
				break;
			}
			break;
		}

	}

	public override void UpKey ()
	{
		switch (thisState) {
		case ZoomState.start:
			ZoomIn();
			break;
		case ZoomState.totemZoomedIn:
			thisButton--;
			switch (thisButton) {
			case (MenuButton.underflow):
			case(MenuButton.none):
				thisButton = MenuButton.quit;
				zoomTotem.SetTrigger ("totemScroll");
				thisState = ZoomState.scrolledDown;
				break;
			case(MenuButton.startGame):
				break;
			case(MenuButton.resumeGame):
				break;	
			case(MenuButton.options):
				break;
			case(MenuButton.quit):
				break;
			}
			break;
		case ZoomState.scrolledDown:
			thisButton--;
			switch (thisButton) {
			case(MenuButton.none):
				break;
			case(MenuButton.startGame):
				zoomTotem.SetTrigger ("totemScroll");
				thisState = ZoomState.totemZoomedIn;
				break;
			case(MenuButton.resumeGame):
				
				zoomTotem.SetTrigger ("totemScroll");
				if (thisState.Equals (ZoomState.totemZoomedIn))
					thisState = ZoomState.scrolledDown;
				else if (thisState.Equals (ZoomState.scrolledDown))
					thisState = ZoomState.totemZoomedIn;
				break;	
			case(MenuButton.options):
				
				break;
			case(MenuButton.quit):
				
				break;
			}
			break;
		}
	}

	public override void AKey ()
	{
		switch (thisState) {
		case ZoomState.start:
			ZoomIn();
			break;
		case ZoomState.totemZoomedIn:
			switch (thisButton) {
			case(MenuButton.none):
				break;
			case(MenuButton.startGame):
				thisButton = MenuButton.none;
				btnNewGame.Click();
				break;
			case(MenuButton.resumeGame):
				thisButton = MenuButton.none;
				btnResume.Click();
				break;	
			}
			break;
		case ZoomState.scrolledDown:
			switch (thisButton) {
			case(MenuButton.resumeGame):
				thisButton = MenuButton.none;
				btnResume.Click();
				break;	
			case(MenuButton.options):
				thisButton = MenuButton.none;
				btnOptions.Click();
				break;
			case(MenuButton.quit):
				thisButton = MenuButton.none;
				btnQuit.Click ();

				break;
			}
			break;
		}
	}
	
	public override void BKey ()
	{
		switch (thisState) {
		case ZoomState.start:
			break;
		case ZoomState.totemZoomedIn:
			ZoomOut ();
			break;
		case ZoomState.scrolledDown:
			ZoomOut ();
			break;
		}
	}
	
	public override void ScrollDown ()
	{
		if (thisState.Equals (ZoomState.totemZoomedIn)) {
			zoomTotem.SetTrigger ("totemScroll");
			thisState = ZoomState.scrolledDown;
		}
	}
	
	public override void ScrollUp ()
	{
		if (thisState.Equals (ZoomState.scrolledDown)) {
			zoomTotem.SetTrigger ("totemScroll");
			thisState = ZoomState.totemZoomedIn;
		}
	}

	#endregion

	#region Animation & Highlighting
	public void Highlight ()
	{
		if (normal != null)
			normal.GetComponent<SpriteRenderer> ().enabled = false;
		mouseover.GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	public void Dehilight ()
	{
		if (normal != null)
			normal.GetComponent<SpriteRenderer> ().enabled = true;
		mouseover.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	public void ZoomIn ()
	{
		thisButton = MenuButton.none;
		zoomTotem.SetTrigger ("zoomActivate");
		title.Zoom();
		thisState = ZoomState.totemZoomedIn;
		StartCoroutine(WaitASec());

	}
	
	public void ZoomOut ()
	{

		thisButton = MenuButton.none;
		zoomTotem.SetTrigger ("zoomActivate");
		title.Zoom ();
		thisState = ZoomState.start;
		ButtonsObj.SetActive(false);
	}

	public void Activate()
	{
		this.GetComponent<Collider2D>().enabled = true;
	}
	
	public void DeActivate()
	{
		this.GetComponent<Collider2D>().enabled = false;
	}

	IEnumerator WaitASec()
	{
			
		yield return new WaitForSeconds (1);
			ButtonsObj.SetActive(true);
	}
#endregion
}
