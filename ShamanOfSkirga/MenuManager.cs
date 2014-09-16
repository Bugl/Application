using UnityEngine;
using System.Collections;
/*
* author: Benjamin Jugl
* switches between menues
*/
public class MenuManager : MonoBehaviour
{

		public enum ActiveMenu
		{
				main,
				optionsMenu,
				sound,
				resolution,
				startNewGame,
				quit
	}
		;


		public ActiveMenu thisMenu = new ActiveMenu ();
		public ActiveMenu lastMenu = new ActiveMenu ();
		public ControlManager control;
		public Button btnStart;
		public Button btnResume;
		public Button btnOptions;
		public Button btnQuit;
		public Menu menuStart;
		public Menu menuOptions;
		public Menu menuSound;
		public Menu menuResolution;
		public Menu menuQuit;
		public MainMenu menuMain;

		public void ActivateMainButtons ()
		{
				btnStart.Activate ();
				btnResume.Activate ();
				btnOptions.Activate ();
				btnQuit.Activate ();
		}

		public void DeActivateMainButtons ()
		{
				btnStart.DeActivate ();
				btnResume.DeActivate ();
				btnOptions.DeActivate ();
				btnQuit.DeActivate ();
		}

		public void ActivateTotem ()
		{
				menuMain.Activate ();
		}
	
		public void DeActivateTotem ()
		{
				menuMain.DeActivate ();
		}

		public void SwitchTo (ActiveMenu a)
		{
				lastMenu = thisMenu;
				thisMenu = a;

				switch (thisMenu) {
				case ActiveMenu.main:
						ActivateTotem ();
						ActivateMainButtons ();
						getSubMenu (lastMenu).Hide ();
						switch (lastMenu) {
						case ActiveMenu.startNewGame:
								StartCoroutine (WaitASec (MainMenu.MenuButton.startGame));
								break;
						case ActiveMenu.optionsMenu:
								StartCoroutine (WaitASec (MainMenu.MenuButton.options));
								break;
						case ActiveMenu.quit:
								StartCoroutine (WaitASec (MainMenu.MenuButton.quit));
								break;
						}
						break;
				case ActiveMenu.startNewGame:
						DeActivateMainButtons ();
						DeActivateTotem ();
						getSubMenu (a).Show ();
						break;
				case ActiveMenu.optionsMenu:
						switch (lastMenu) {
						case ActiveMenu.main:
								DeActivateMainButtons ();
								getSubMenu (a).Show ();
								break;
						case ActiveMenu.resolution:
								getSubMenu (lastMenu).Hide ();
								getSubMenu (a).Show ();
								break;
						case ActiveMenu.sound:
								getSubMenu (lastMenu).Hide ();
								getSubMenu (a).Show ();
								break;
						}
						break;
				case ActiveMenu.resolution:
						getSubMenu (lastMenu).Hide ();
						getSubMenu (a).Show ();
						break;
				case ActiveMenu.sound:
						getSubMenu (lastMenu).Hide ();
						getSubMenu (a).Show ();
						break;
				case ActiveMenu.quit:
						DeActivateMainButtons ();
						DeActivateTotem ();
						getSubMenu (a).Show ();
						break;
				}
		}

		public Menu getSubMenu (ActiveMenu menu)
		{
				switch (menu) {
				case ActiveMenu.startNewGame:
						return (Menu)menuStart;
				case ActiveMenu.resolution:
						return (Menu)menuResolution;
				case ActiveMenu.sound:
						return (Menu)menuSound;
				case ActiveMenu.optionsMenu:
						return (Menu)menuOptions;
				case ActiveMenu.quit:
						return (Menu)menuQuit;
				}
				return null;
		}

		IEnumerator WaitASec (MainMenu.MenuButton b)
		{
				if (!control.mouseSelection) {
						yield return new WaitForSeconds (1);
						menuMain.thisButton = b;
				}
		}
}
