using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle,Playing, Ended, Ready};
public class GameController: MonoBehaviour {

    [Range(0f, 0.20f)]
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage platform;
    public GameObject uiIdel;
    public GameObject scoreIdel;
    public Text pointsText;
    public Text recordText;
    public Text lifesText;
    
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGeneration;
    
    private float scaleTime;
    //public float scaleTime = 6f;//
    public float scaleInc = .25f;
    
    private AudioSource musicPlayer;
    private int points;
    /////////
    private int lifes;
    private float velocidad; 
    
    
    // Start is called before the first frame update
    void Start() {
         points = FindObjectOfType<GameManager>().points;
        lifes = FindObjectOfType<GameManager>().lifesss;
        scaleTime = FindObjectOfType<GameManager>().timeFinal;
        //
        musicPlayer = GetComponent<AudioSource>();
        recordText.text = "BEST: " + GetMaxScore().ToString();
        lifesText.text = "Lifes: "+ lifes.ToString();
        //lifesText.SetActive(true);
        //
       
    }

    // Update is called once per frame
    void Update() {
        
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
        //empezar el juego
        if (gameState == GameState.Idle && userAction) {
           FollowGame();
           player.SendMessage("DustPlay");
        }
        //juego en marcha
        else if (gameState == GameState.Playing) {
            //Le hace mover al juego
            Parallax();

        }
        //Juego reparado para reiniciarce
        else if(gameState == GameState.Ready){
            if(userAction){
                lifes--;
                RestarGame();
                //aqui declaro si aun tiene vidas                
            }
        }

    }

    void Parallax() {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        
    }
    
    public void RestarGame(){        
        FindObjectOfType<GameManager>().lifesss = lifes;
        //Debug.Log("las vidas son: " + lifes);
        lifesText.text="Lifes: "+ lifes.ToString();
        if(lifes>0){
            
            
            ResetTimeScale();  
            FollowGame();
        }else{
            FindObjectOfType<GameManager>().timeFinal = 6f;
            FindObjectOfType<GameManager>().points = 0;
            FindObjectOfType<GameManager>().lifesss = 3;
            SceneManager.LoadScene("Principal");        
        }
    }
    
    void GameTimeScale(){
        Time.timeScale += scaleInc;
        
        //Debug.Log("Ritmo incrementado: " + Time.timeScale.ToString());
    }
    
    public void ResetTimeScale(float newTimeScale = 1f){
        float timeActual=Time.timeScale;
        //Debug.Log(Time.timeScale +"--"+scaleInc+"++++"+timeActual);
        FindObjectOfType<GameManager>().timeFinal = timeActual;

        CancelInvoke("GameTimeScale");
        //Debug.Log(timeActual);
        Time.timeScale = newTimeScale;
        //Debug.Log("Ritmo Restablecido"+ Time.timeScale.ToString());

    }

    public void IncreasePoints(){
        //points++;
        //pointsText.text = points.ToString();
        pointsText.text = (++points).ToString();
       
        
        //envio el dato
        if(points >= GetMaxScore()){
            recordText.text = "BETS: " + points.ToString();
            SaveScore(points);
        }
        //envio el dato de los puntos - borrar
        FindObjectOfType<GameManager>().points=points;
    }

    public int GetMaxScore(){
        return PlayerPrefs.GetInt("Max Points", 0);
    }
    
    public void SaveScore(int currentPoints){
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
    //
    public void FollowGame(){
        gameState = GameState.Playing;
        uiIdel.SetActive(false);
        scoreIdel.SetActive(true);
        player.SendMessage("UpdateState","PlayerRum");
        
        enemyGeneration.SendMessage("StartGenerator");
        musicPlayer.Play();
        InvokeRepeating(nameof(GameTimeScale), scaleTime, scaleTime);
    }
}
