using UnityEngine;
using System.Collections;
/*
* author: Benjamin Jugl
* Virtual Class for Menues
*/
public class Menu : MonoBehaviour {

	public ControlManager control;
	public MenuManager menuManager;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void DownKey ()
	{
	}

	public virtual void UpKey ()
	{

	}

	public virtual void LeftKey ()
	{
	}
	
	public virtual void RightKey ()
	{
		
	}
	
	public virtual void AKey ()
	{

	}
	
	public virtual void BKey ()
	{

	}
	
	public virtual void ScrollDown ()
	{

	}
	
	public virtual void ScrollUp ()
	{

	}

	public virtual void Show()
	{

	}
	public virtual void Hide()
	{

	}
}
