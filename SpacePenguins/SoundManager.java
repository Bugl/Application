package de.bib.vpr.data;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;

import de.bib.vpr.util.Logger;

import java.util.HashMap;

/**
 * Lädt Sounds und Musik und ordnet sie in einer HashMap einem Alias (String)
 * zu. Enthält Methoden, die einen String entgegen nehmen und entsprechende
 * Sounds oder Musik lädt und wiedergibt. Muss am Anfang des Programms einmal
 * initialisiert werden.
 * 
 * Loads Sound and Music and links them via HashMap to a String. Contains Methods that receive Strings and play
 * the refered sounds. Has to be initialized at the programs start. (All further references in German)
 *
 * @author Benjamin Jugl
 * @version 1.7
 */
public class SoundManager {

	private Music lastSong;
	private Sound sound;
	private Music music;

	HashMap<String, Sound> sounds = new HashMap<String, Sound>();
	HashMap<String, Music> songs = new HashMap<String, Music>();

	/**
	 * Konstruktor. Legt die Hashmaps sounds und songs an.
	 */
	protected SoundManager() {
		super();

		try {
			makeSounds();
		} catch (Exception e) {
			Logger.error("SoundManager: Fehler beim Laden der Sounds", e);
		}

		try {
			makeSongs();
		} catch (Exception e) {
			Logger.error("SoundManager: Fehler beim Laden der Musik", e);
		}
		lastSong = null;
	}

	/**
	 * Füllt die Hashmap songs und füllt sie.
	 */
	private void makeSongs() {
		songs.put("menu", Gdx.audio.newMusic(Gdx.files.internal("sound/m_menu.ogg")));
		songs.put("gameOverFail", Gdx.audio.newMusic(Gdx.files.internal("sound/m_gameOverFail.ogg")));
		songs.put("gameOverWin", Gdx.audio.newMusic(Gdx.files.internal("sound/m_gameOverWin.ogg")));
		songs.put("level1", Gdx.audio.newMusic(Gdx.files.internal("sound/m_level1.ogg")));
		songs.put("level2", Gdx.audio.newMusic(Gdx.files.internal("sound/m_level2.ogg")));
		songs.put("level3", Gdx.audio.newMusic(Gdx.files.internal("sound/m_level3.ogg")));
		songs.put("level4", Gdx.audio.newMusic(Gdx.files.internal("sound/m_level4.ogg")));
		songs.put("boss", Gdx.audio.newMusic(Gdx.files.internal("sound/m_boss.ogg")));
		songs.put("intro", Gdx.audio.newMusic(Gdx.files.internal("sound/m_intro.ogg")));
		songs.put("credits", Gdx.audio.newMusic(Gdx.files.internal("sound/m_credits.ogg")));
		// songs.put("level3",
		// Gdx.audio.newMusic(Gdx.files.internal("sound/m_level3.ogg")));

	}

	/**
	 * Füllt die Hashmap sounds - optionale Sounds sind auskommentiert und
	 * werden bei Implementierung eingefügt
	 */
	private void makeSounds() {
		sounds.put("gameOver", Gdx.audio.newSound(Gdx.files.internal("sound/s_gameOver.ogg")));
		sounds.put("levelEnd", Gdx.audio.newSound(Gdx.files.internal("sound/s_levelEnd.ogg")));
		sounds.put("buttons", Gdx.audio.newSound(Gdx.files.internal("sound/s_buttons.ogg")));
		sounds.put("buyConfirm", Gdx.audio.newSound(Gdx.files.internal("sound/s_buyConfirm.ogg")));
		// sounds.put("buyFail",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_buyFail.ogg")));
		sounds.put("weapon1", Gdx.audio.newSound(Gdx.files.internal("sound/s_weapon1.ogg")));
		sounds.put("weaponLaser", Gdx.audio.newSound(Gdx.files.internal("sound/s_weaponLaser.ogg")));
		sounds.put("weaponBall", Gdx.audio.newSound(Gdx.files.internal("sound/s_weaponBall.ogg")));
		sounds.put("bomb", Gdx.audio.newSound(Gdx.files.internal("sound/s_bomb2.ogg")));
		// sounds.put("powerUp",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp.ogg")));
		sounds.put("explosion", Gdx.audio.newSound(Gdx.files.internal("sound/s_explosion.ogg")));
		sounds.put("hit", Gdx.audio.newSound(Gdx.files.internal("sound/s_hit.ogg")));
		sounds.put("hitEnemy", Gdx.audio.newSound(Gdx.files.internal("sound/s_hitEnemy.ogg")));
		// //sounds.put("explosionEnemy2",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionEnemy2.ogg")));
		// //sounds.put("explosionEnemy3",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionEnemy3.ogg")));
		// //sounds.put("explosionEnemy4",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionEnemy4.ogg")));
		// //sounds.put("explosionEnemy5",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionEnemy5.ogg")));
		sounds.put("explosionLong", Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionLong.ogg")));
		sounds.put("charge", Gdx.audio.newSound(Gdx.files.internal("sound/s_charge.ogg")));
		// sounds.put("shildAlarm",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_shildAlarm.ogg")));
		sounds.put("error", Gdx.audio.newSound(Gdx.files.internal("sound/s_error.ogg")));
		// sounds.put("explosionBoss",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_explosionBoss.ogg")));
		// //sounds.put("bossText1",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_bossText1.ogg")));
		// //sounds.put("bossText2",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_bossText2.ogg")));
		// //sounds.put("bossText3",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_bossText3.ogg")));
		// sounds.put("obstacle",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_obstacle.ogg")));
		// //sounds.put("powerUp1",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp1.ogg")));
		// //sounds.put("powerUp2",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp2.ogg")));
		// //sounds.put("powerUp3",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp3.ogg")));
		// //sounds.put("powerUp4",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp4.ogg")));
		// //sounds.put("powerUp5",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_powerUp5.ogg")));
		// //sounds.put("enemyGroup1",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_enemyGroup1.ogg")));
		// //sounds.put("enemyGroup2",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_enemyGroup2.ogg")));
		// //sounds.put("enemyGroup3",
		// Gdx.audio.newSound(Gdx.files.internal("sound/s_enemyGroup3.ogg")));
	}

	/**
	 * Spielt den entsprechenden Sound ab<br>
	 * Spielt im Falle eines Fehlers den Errorsound ab.
	 * 
	 * @param soundStr
	 *            Ein String welcher der Key für die HashMap der Sounddateien
	 *            gilt.
	 */
	public void playSound(String soundStr) {

		sound = sounds.get(soundStr);
		if (sound == null) {
			Logger.error("Sound '" + soundStr + "' wurde nicht gefunden");
			sound = sounds.get("error");
		}

		if (sound != null)
			
			sound.play(Core.OPTION.getSoundVol() / 300); // Greift auf
															// optiondata zu und
															// setzt die
															// eingestellte
															// Lautstärke.
        //Logger.debug("Spiele Sound " + soundStr + " mit der Lautstärke "+ Core.OPTION.getSoundVol()/300);//spielt das Lied
    }

	/**
	 * Stopt das zuletzt gespielt Lied und spielt ein neues Lied ab. <br>
	 * Spielt im Falle eines Fehlers das Errorlied ab.
	 * 
	 * @param musicStr
	 *            Ein String welcher der Key für die HashMap der Musikdateien
	 *            gilt.
	 */
	public void playMusic(String musicStr) {
		this.playMusic(musicStr, true);
	}

	public void playMusic(String musicStr, boolean repeat) {
		try {
			music = songs.get(musicStr);
			if (music == null)
				Logger.error("Musik '" + musicStr + "' wurde nicht gefunden");
			

			else {
				if (lastSong == songs.get("menu") && music == songs.get("intro")) {
					//Fängt den Timer des Intros ab, wenn dieses vorzeitig abgebrochen wird.
					//Logger.debug("Soundoverride abgefangen");
				}
				else if(lastSong == songs.get("level1") && music == songs.get("intro")){
					//Fängt den Timer des Intros ab, wenn dieses vorzeitig abgebrochen wird.
					//Logger.debug("Soundoverride abgefangen");
				}

				else {
					if (lastSong != music) // wenn der gleiche Song schon spielt
											// wird er nicht neu geladen/gestartet
					{
						if (lastSong != null) {
							lastSong.stop(); // stopt das letzte Lied
						}
						if (repeat) {
							music.setLooping(true);
						}
						music.setVolume(Core.OPTION.getMusicVol() / 300); // Greift
																			// auf
																			// optiondata
																			// zu
																			// und
																			// setzt
																			// die
																			// eingestellte
																			// Lautstärke.
						music.play();
	                    //Logger.debug("Spiele Song " + musicStr + " mit der Lautstärke " + Core.OPTION.getMusicVol() / 300);// spielt
	                    // das
																															// Lied
						lastSong = music;
					}
				}
			}
		} catch (Exception e) {
			// TODO: handle exception....Musik im Menu wirft beim zweiten mal abspielen einen Fehler.
		}

		

		
	}

	/**
	 * Schmeißt alle Sounds und Lieder aus dem Arbeitsspeicher
	 */
	public void dispose() {
		lastSong.dispose();
		music.dispose();
		sound.dispose();
	}

	/**
	 * Sorgt dafür, dass die Lautstärke verändert werden kann (z.B beim
	 * Verschieben des Lautstärkeregelers im Optionenmenü)
	 */
	public void musicVolumeChange() {
		music.setVolume(Core.OPTION.getMusicVol() / 300);
	}

	/**
	 * Pausiert das aktuelle Musikstück
	 */
	public void pauseMusic() {
		if (music != null) {
			music.pause();
		}
	}

	/**
	 * Spielt das aktuelle Musikstück (weiter) ab.
	 */
	public void resumeMusic() {
		if (music != null) {
			music.play();
		}
	}

}
