using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  GameObject game;
    public  GameObject enemyGenerator;
    public  AudioClip jumpClip;
    public  AudioClip dieClip;
    public  AudioClip pointClip;

    public  ParticleSystem dust;//polvo

    private Animator animator;    
    private AudioSource audioPlayer;
    private float startY;
    
    // Start is called before the first frame update
    void Start()
    {       
        
        animator=GetComponent<Animator>();    
        audioPlayer = GetComponent<AudioSource>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        bool isGrounded = transform.position.y == startY;
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
        
        if(isGrounded && gamePlaying && userAction){
            UpdateState("PlayerJump");
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }
    }
    
    public void UpdateState(string stado = null){//cambia el estado del personje-solo salta
        //Debug.Log("Entra 1"+ stado);
        if(stado != null){
            //Debug.Log("Entra 2222" + stado);
            animator.Play(stado);            
        }   
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Me muero!");
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
            game.SendMessage("ResetTimeScale", 0.5f);           

            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();           
            DustStop();
        }else if(other.gameObject.CompareTag("Point"))
        {
            game.SendMessage("IncreasePoints");
            audioPlayer.clip = pointClip;
            audioPlayer.Play();
        }
    }
    
    void GameReady(){
        game.GetComponent<GameController>().gameState = GameState.Ready;
    }

    public void DustPlay(){
        dust.Play();//        
    }
    void DustStop(){        
        dust.Stop();
    }
}
