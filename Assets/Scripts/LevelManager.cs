using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject ui;

    void Awake() {
        //Destroy UI when on you win or when you lose
        if (SceneManager.GetActiveScene().name.Equals("You Win Screen"))
        {
            Debug.Log("On You Win Screen");
            ui = GameObject.Find("UI");
            Destroy(ui);

        }
        else if (SceneManager.GetActiveScene().name.Equals("You Lose Screen"))
        {
            Debug.Log("On You Lose Screen");
            ui = GameObject.Find("UI");
            Destroy(ui);
        }
    }

	public void LoadLevel(string name){
		//Debug.Log ("New Level load: " + name);
		//Application.LoadLevel (name);
        SceneManager.LoadScene(name);
	}

    public void NextLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    //Carrying out the bottom in the Level Script
    //public void BrickDestroyed() {//going to ask the question
    //    if (Box.breakCount) {//if this is the last brick destroyed
    //        NextLevel();
    //    }
    //}



}
