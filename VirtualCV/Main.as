package {
	import flash.display.*;
	import flash.events.*;
	import flash.utils.*;
	import flash.net.*;
	import flash.media.SoundChannel;
	import flash.media.SoundTransform;
/*
* author: Benjamin Jugl
* The heart of my virtual Resumé
*/
	public class Main extends MovieClip {
		
		//for Movement
		var leftdown, rightdown: Boolean = false;		//your standard movement variables
		
		//Characters
		var ben_mc: Ben;								//It'sa Me, Ma... ummm, Ben
		var Items: Array;								//All those little pieces of knowledge belong in here
		var hand_mc: Hand;								//THE HAND!!! Was named Teacher in the earlier Versions. is still confused sometimes. But has a good therapist.
		
		//Screens and Buttons
		var background_mc: Background;					//It's plain, simple and kind of fawn, but I like it. Strange.
		var startScreen_mc: StartScreen;				//Where it all Begins
		var startButton_btn: StartButton;				//Push it. Do so. 
		var endScreen_mc: EndScreen;					//This is the End, my friend... (The Doors)
		var creditsScreen_mc: CreditsScreen;			//This is what comes after the End. When you push the right Button.
		var creditsButton_btn: CreditsButton;			//The right Button		
		var restartButton_btn: RestartButton;			//Virtual Reincarnation.
		var backButton_btn: BackButton;					//Actually it is Back to the EndScreen. (To leave the CreditScreen)
		
		//Timers
		var throwTimer: Timer = new Timer(1450);		//Synchronizes Hand and Item Animation
		var downTimer: Timer = new Timer(1000);			//Gives THE HAND some time to get down and relax at the BEginning of the Game
		var startButtonTimer: Timer = new Timer(6000);	//Gives you, dear Friend, some time to marvel at my precious Start Animation
		var creditsButtonTimer: Timer = new Timer(4000);//Same as above, only for the End and the Credit-Screen (The Credit Screen has no Animation, though - but don't mention it. He is still kind of unkomfortable about that.)
		var startTimer: Timer = new Timer(5000);		//Witness the great pleasures of FunnelToTheHead, before starting.
		var funnelSoundTimer: Timer = new Timer (1800);	//Waiting for the perfect Moment to splatt(!).
		
		//Music&Sound - For everything I didn't do myself, there are the Credits. Thanx.
		var channel: SoundChannel = new SoundChannel();
		var lower: SoundTransform = new SoundTransform(0.3, 0.5);
		var middle: SoundTransform = new SoundTransform(0.6, 0.5);	//Did I use that? Might as well leave it in. Perhaps it will find some use later on.
		var louder: SoundTransform = new SoundTransform(1.0,0.5);
		var spinMonkey_msc:SpinningMonkeys=new SpinningMonkeys();
		var polka_msc:Polka=new Polka();
		var fanfare_msc: Fanfare = new Fanfare();
		var throw_snd: Throw = new Throw();
		var flower_snd: Flower = new Flower();
		var funnel_snd: Funnel = new Funnel();
		var excellent_snd: Excellent = new Excellent();
		var yeah_snd: Yeah = new Yeah();							//Chickachicka - Oh yeah. (Yellow)
		var wellDone_snd: Welldone = new Welldone();
		var veryGood_snd: Verygood = new Verygood();
		
		// Little Helpers
		var inGame: Boolean = false;								//Don't start to early! 
		var itemCount: int = 0;																	
		var randomNumberI: int = 1;									//randomize the throwintervall
		var counterI: int = 0;										//helps to potentially throw faster.
		var randomNumberT: int = 1;									//Help THE HAND formerly known as Teacher to have an AI.
		var counterT: int = 0;										//Another Helper for THE HANDs AI.
		var newInterval: Boolean = false;							
		var ItemList: Array;										//List of all Items without the "_itm" 
		
		var ObjectList: Array = new Array(Photoshop_itm,			//List of all possible Objects that get thrown around
			Illustrator_itm,
			Flash_itm,
			Unity_itm,
			JQuery_itm,
			Sql_itm,
			ThreeDsmax_itm,
			Java_itm,
			JavaScript_itm,
			CSharp_itm,
			Css_itm);
			
		/*Main....setting up the Stage, starting EnterFrames, Transfering Objects from ObjectList to ItemList*/

		public function Main() {
			stage.addEventListener(KeyboardEvent.KEY_DOWN, listenKeyDown);
			stage.addEventListener(KeyboardEvent.KEY_UP, listenKeyUp);
			stage.addEventListener(Event.ENTER_FRAME, checkCols);
			stage.addEventListener(Event.ENTER_FRAME, createItem);
			stage.addEventListener(Event.ENTER_FRAME, recycleItem);
			stage.addEventListener(Event.ENTER_FRAME, handAI);

			background_mc = new Background();
			ben_mc = new Ben(160, stage.stageHeight - 60);
			hand_mc = new Hand(stage.stageWidth / 2, 40);
			startScreen_mc = new StartScreen(0, 0);
			
			addChild(background_mc);
			addChild(ben_mc);
			addChild(hand_mc);
			addChild(startScreen_mc);
			
			
			Items = new Array();
			ItemList = new Array();
			for each(var o: Object in ObjectList) {
				ItemList.push(o.getName());
			}
			startButtonTimer.addEventListener(TimerEvent.TIMER, showStartButton);
			startButtonTimer.start();
			channel = fanfare_msc.play(0,1,lower);
			
		}
		/*Shows the StartButton after the Start Animation is completed*/
		
		public function showStartButton(event: TimerEvent){
			startButtonTimer.removeEventListener(TimerEvent.TIMER,showStartButton);
			startButtonTimer.reset();
			startButton_btn = new StartButton(690,530);
			startButton_btn.addEventListener(MouseEvent.CLICK, startGame);
			startButton_btn.addEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			addChild(startButton_btn);
		}
		/*Called by the StartButton does exactly what it is ment to do: Starts the actual Game*/
		public function startGame(event: MouseEvent) {
			channel.stop();
			channel = spinMonkey_msc.play(0,5, lower);
			startButton_btn.removeEventListener(MouseEvent.CLICK, startGame);
			startButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			removeChild(startScreen_mc);
			removeChild(startButton_btn);
			/*initiating Start-Animations for the Player and the hand*/
			ben_mc.gotoAndPlay("start");
			hand_mc.gotoAndPlay("start");
			/*wait a little to play the funnelSound right on time*/
			funnelSoundTimer.addEventListener(TimerEvent.TIMER, funnelSound);
			funnelSoundTimer.start();
			/*wait for the players Animation to be finished, and then start the game*/
			startTimer.addEventListener(TimerEvent.TIMER, waitForFunnel);
			startTimer.start();
		}
		/*Splash - right on time*/
		public function funnelSound(event: TimerEvent){
			funnelSoundTimer.removeEventListener(TimerEvent.TIMER, funnelSound);
			funnelSoundTimer.reset();
			funnel_snd.play(0,1,louder);
		}
		/*want to play again. okay, let me just clean this up a little, 
		refill the item list and tell the mcs to go to start.*/
		public function restartGame(event: MouseEvent) {
			channel.stop();
			channel = spinMonkey_msc.play(0,5,lower);
			restartButton_btn.removeEventListener(MouseEvent.CLICK, restartGame);
			restartButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			creditsButton_btn.removeEventListener(MouseEvent.CLICK, showCredits);
			creditsButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			removeChild(endScreen_mc);
			
			removeChild(creditsButton_btn);
			removeChild(restartButton_btn);
			for each(var o: Object in ObjectList) {
				ItemList.push(o.getName());
			}
			resetItemCounts();
			ben_mc.x = 160;
			ben_mc.y = stage.stageHeight - 60;
			hand_mc.gotoAndPlay("start");
			ben_mc.gotoAndPlay("start");
			startTimer.addEventListener(TimerEvent.TIMER, waitForFunnel);
			startTimer.start();
		}
		/*refill my statics - the items are designed that you can collect exactly the amount of my skill level of everyone. very subtle*/
		public function resetItemCounts(){
			for each(var o: Object in ObjectList) {
				o.resetQuantity();
			}
		}
		/*so you pushed start and all you get is a funnel showed into your head....*/
		public function waitForFunnel(event: TimerEvent) {
			startTimer.removeEventListener(TimerEvent.TIMER, waitForFunnel);
			startTimer.reset();
			hand_mc.gotoAndPlay("getDown");
			downTimer.addEventListener(TimerEvent.TIMER, waitForDown);
			downTimer.start();
		}
		/*Gives THE HAND some time to really appear on stage before the throwing begins*/
		public function waitForDown(event: TimerEvent){
			downTimer.removeEventListener(TimerEvent.TIMER, waitForDown);
			downTimer.reset();
			inGame=true;
		}
/*Movement and AI*/
		/*Basic Movement Functions - thanx to Manfred Osthof */
		public function listenKeyDown(event: KeyboardEvent) {
			if(inGame){
			if (event.keyCode == 37) { //Left arrow
				leftdown = true;
			}
			if (event.keyCode == 39) { //Right arrow
				rightdown = true;
			}
		}
		}


		public function listenKeyUp(event: KeyboardEvent) {
			if (event.keyCode == 37) { //Left arrow
				leftdown = false;
			}
			if (event.keyCode == 39) { //Right arrow
				rightdown = false;
			}
		}
		
		/*throws Items in random intervals. The lower limit gets smaller with every item that is thrown, up to the fifth.*/ 
		public function createItem(event: Event) {
			if (inGame) {									
				counterI++;
				if (counterI == randomNumberI) {
					if (itemCount <= 5)
						itemCount++;
					randomNumberI = randomRange(120 - itemCount * 10, 120 - itemCount * 5);
					counterI = 0;
					if (!checkIfAllEmpty()) {
						hand_mc.gotoAndPlay("throw");
						throwTimer.addEventListener(TimerEvent.TIMER, throwItem);
						throwTimer.start();
					} else {
						if (numChildren <= 3)
							showEndScreen();
					}
				}
			}
		}
		/*gets the item on Stage and plays a sound*/
		public function throwItem(event: TimerEvent) {
			removeEventListener(TimerEvent.TIMER, throwItem);
			throwTimer.reset();
			throw_snd.play(0,1,lower);
			
			addItem(hand_mc.x, hand_mc.y);
		}
		
		/*adds the item to the Array*/
		public function addItem(x, y) {
			var i: Item = chooseItem(x, y);
			if (i != null) {
				trace(i);
				addChild(i);
				Items.push(i);
			}
		}
		
		/*chooses a random item out of the available objects*/
		public function chooseItem(x, y) {
			var rand: int = randomRange(0, ItemList.length - 1);
			var i: Item;
			switch (ItemList[rand]) {
				case "Photoshop":
					i = new Photoshop_itm(x, y);
					if (Photoshop_itm.empty())
						ItemList.splice(ItemList.indexOf("Photoshop"), 1);
					break;
				case "Illustrator":
					i = new Illustrator_itm(x, y);
					if (Illustrator_itm.empty())
						ItemList.splice(ItemList.indexOf("Illustrator"), 1);
					break;
				case "Flash":
					i = new Flash_itm(x, y);
					if (Flash_itm.empty())
						ItemList.splice(ItemList.indexOf("Flash"), 1);
					break;
				case "JQuery":
					i = new JQuery_itm(x, y);
					if (JQuery_itm.empty())
						ItemList.splice(ItemList.indexOf("JQuery"), 1);
					break;
				case "Unity":
					i = new Unity_itm(x, y);
					if (Unity_itm.empty())
						ItemList.splice(ItemList.indexOf("Unity"), 1);
					break;
				case "Sql":
					i = new Sql_itm(x, y);
					if (Sql_itm.empty())
						ItemList.splice(ItemList.indexOf("Sql"), 1);
					break;
				case "Java":
					i = new Java_itm(x, y);
					if (Java_itm.empty())
						ItemList.splice(ItemList.indexOf("Java"), 1);
					break;
				case "JavaScript":
					i = new JavaScript_itm(x, y);
					if (JavaScript_itm.empty())
						ItemList.splice(ItemList.indexOf("JavaScript"), 1);
					break;
				case "Css":
					i = new Css_itm(x, y);
					if (Css_itm.empty())
						ItemList.splice(ItemList.indexOf("Css"), 1);
					break;
				case "CSharp":
					i = new CSharp_itm(x, y);
					if (CSharp_itm.empty())
						ItemList.splice(ItemList.indexOf("CSharp"), 1);
					break;
				case "ThreeDsmax":
					i = new ThreeDsmax_itm(x, y);
					if (ThreeDsmax_itm.empty())
						ItemList.splice(ItemList.indexOf("ThreeDsmax"), 1);
					break;
			}
			return i;
		}
		
		/*checks if the ItemList is Empty, used to determine if the game is over*/
		public function checkIfAllEmpty() {

			return ItemList.length == 0;
		}

		/*removes Item from Array*/
		public function removeItem(item: Item) {
			for (var i in Items) {
				if (Items[i] == item) {
					Items.splice(i, 1);
					break;
				}
			}
		}
		
		/*simple collision Check*/
		public function checkCols(event: Event) {
			var itemsNo: int = Items.length - 1;

			for each(var i: Item in Items) {
				if (i.hitTestObject(ben_mc)) {
					i.deleteItem();
					ben_mc.gotoAndPlay("happy");
					playVoice();
					
					break;
				}
			}
		}
		
		/*random soundeffect for catched items*/
		public function playVoice(){
			var randomVoice: int= randomRange(1,3);
					switch(randomVoice){
						case 1: 
							yeah_snd.play(0,1,lower);
							break;
						case 2:
							wellDone_snd.play(0,1,lower);
							break;
						case 3:
							veryGood_snd.play(0,1,lower);
							break;
						
					}
		}
		
		/*If the player is to slow he will get a second chance. And a third.... and a fourth...No more losers*/
		public function recycleItem(event: Event) {
			for each(var i: Object in ObjectList) {
				if (i != null) {
					if (i.recycled == true) {
						i.recycled = false;
						if (ItemList.indexOf(i.getName()) == -1)
							ItemList.push(i.getName());
					}
				}
			}
		}
		
		/*Little Helper for Randomization - Gives back a random Value between first and second param*/
		function randomRange(minNum: Number, maxNum: Number): Number {
			return (Math.floor(Math.random() * (maxNum - minNum + 1)) + minNum);
		}
		
		/*It's not much...but sufficient for a hand*/
		function handAI(event: Event) {
			counterT++;
			if (counterT == randomNumberT) {
				randomNumberT = randomRange(60, 240);
				counterT = 0;
				hand_mc.changeDirection();
			}
		}
	/*Endscreen and CreditScreen Logic*/
		function showEndScreen() {
			channel.stop();
			channel = polka_msc.play(0,2,lower);
			downTimer.addEventListener(TimerEvent.TIMER, playExcellent);
			downTimer.start();
			
			inGame = false;
			endScreen_mc = new EndScreen(0, 0);
			addChild(endScreen_mc);
			creditsButtonTimer.addEventListener(TimerEvent.TIMER, showCreditsButton);
			creditsButtonTimer.start();
		}
		
		public function playExcellent(event:TimerEvent){
			downTimer.removeEventListener(TimerEvent.TIMER, playExcellent);
			downTimer.reset();
			excellent_snd.play(0,1,lower);
		}
		
		function showCreditsButton(event: TimerEvent){
			creditsButtonTimer.removeEventListener(TimerEvent.TIMER, showCreditsButton);
			creditsButtonTimer.reset();
			creditsButton_btn = new CreditsButton(690, 20);
			creditsButton_btn.addEventListener(MouseEvent.CLICK, showCredits);
			creditsButton_btn.addEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			addChild(creditsButton_btn);
			restartButton_btn = new RestartButton(20,20);
			restartButton_btn.addEventListener(MouseEvent.CLICK, restartGame);
			restartButton_btn.addEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			addChild(restartButton_btn);
		}
		
		function showCredits(event: MouseEvent){
			creditsButton_btn.removeEventListener(MouseEvent.CLICK, showCredits);
			creditsButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			restartButton_btn.removeEventListener(MouseEvent.CLICK, restartGame);
			restartButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			removeChild(restartButton_btn);
			removeChild(creditsButton_btn);
			removeChild(endScreen_mc);
			creditsScreen_mc = new CreditsScreen(0,0);
			addChild(creditsScreen_mc);
			creditsButtonTimer.addEventListener(TimerEvent.TIMER, showBackButton);
			creditsButtonTimer.start();
		}
		
		function showBackButton(event:TimerEvent){
			creditsButtonTimer.removeEventListener(TimerEvent.TIMER, showBackButton);
			creditsButtonTimer.reset();
			backButton_btn = new BackButton(20,20);
			backButton_btn.addEventListener(MouseEvent.CLICK, goBack);
			backButton_btn.addEventListener(MouseEvent.ROLL_OVER, playButtonSnd);			
			addChild(backButton_btn);
		}
		
		function goBack(event:MouseEvent){
			removeChild(creditsScreen_mc);
			backButton_btn.removeEventListener(MouseEvent.CLICK, goBack);
			backButton_btn.removeEventListener(MouseEvent.ROLL_OVER, playButtonSnd);
			removeChild(backButton_btn);
			showEndScreen();
		}
		
		/*Sound for all Buttons*/
		function playButtonSnd(event: MouseEvent){
			flower_snd.play(0,1,lower);
		}

	}
}