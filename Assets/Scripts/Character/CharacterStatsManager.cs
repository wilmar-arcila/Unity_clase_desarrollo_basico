using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    private Vector3 respawnPoint;
    private int lives = 3;
    private int score = 0;
    private int[] items = new int[]{0,0,0};
    private bool[] powers = new bool[]{false,false,false};

    private int scoreForItem1 = 1;
    private int scoreForItem2 = 2;
    private int scoreForItem3 = 3;
    private int scoreForPower = 10;

    private float respawnHigh = 6f;

     //////////////////////////////////////////////
    /*         OBSERVER PATTERN (as Publisher)  */
    public event Action CharacterStatsChanged;
    public event Action CharacterGameOver;
    //////////////////////////////////////////////
    /*          OBSERVER PATTERN (as Observer)  */
    [SerializeField] private GameObject character;
    private InteractionEngine characterInteractionPublisher;
    //////////////////////////////////////////////
    
    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static CharacterStatsManager Instance;
    private void Awake()
    {
        if(CharacterStatsManager.Instance == null){
            CharacterStatsManager.Instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }
    }
    public static CharacterStatsManager getInstance(){
        return CharacterStatsManager.Instance;
    }
    //////////////////////////////////////////////

    //////////////////////////////////////////////
    /*    OBSERVER PATTERN (as Observer)        */
    private void Start()
    {
        characterInteractionPublisher = character.GetComponent<CharacterInstanciator>().getCharacter().GetComponent<InteractionEngine>();
        if (characterInteractionPublisher != null) // Se suscribe a los respectivos eventos
        {
            characterInteractionPublisher.CharacterItemsChanged += OnCharacterItemsChanged;
            characterInteractionPublisher.CharacterPowersChanged += OnCharacterPowersChanged;
            characterInteractionPublisher.CharacterLivesChanged += OnCharacterLivesChanged;
        }

        respawnPoint = new Vector3(0,respawnHigh,0);
    }
    private void OnDestroy()
    {
        if (characterInteractionPublisher != null) // Cancela la suscripción a los eventos
        {
            characterInteractionPublisher.CharacterItemsChanged -= OnCharacterItemsChanged;
            characterInteractionPublisher.CharacterPowersChanged -= OnCharacterPowersChanged;
            characterInteractionPublisher.CharacterLivesChanged -= OnCharacterLivesChanged;
        }
    }
    private void OnCharacterItemsChanged((int item1, int item2, int item3)deltaItems)
    {
        Debug.Log("[CharacterStatsManager]Items Changed: " + deltaItems);
        if(deltaItems.item1 != 0){
            changeItem1(deltaItems.item1);
        }
        if(deltaItems.item2 != 0){
            changeItem2(deltaItems.item2);
        }
        if(deltaItems.item3!= 0){
            changeItem3(deltaItems.item3);
        }
    }
    private void OnCharacterLivesChanged(int deltaLives)
    {
        Debug.Log("[CharacterStatsManager]Lives Changed: " + deltaLives);
        changeLives(deltaLives);
    }
    private void OnCharacterPowersChanged((int item1, int item2, int item3)deltaPowers)
    {
        Debug.Log("[CharacterStatsManager]Powers Changed: " + deltaPowers);
        if(deltaPowers.item1 != 0){
            changePower1(deltaPowers.item1 > 0?true:false);
        }
        if(deltaPowers.item2 != 0){
            changePower2(deltaPowers.item2 > 0?true:false);
        }
        if(deltaPowers.item3 != 0){
            changePower3(deltaPowers.item3 > 0?true:false);
        }
    }
    ///////////////////////////////////////////////

    public void setScore(int _score){
        score = _score;
        CharacterStatsChanged?.Invoke();
    }
    public int getScore(){
        return score;
    }
    private void raiseScore(int _plusScore){
        score += _plusScore;
        CharacterStatsChanged?.Invoke();
    }

    public int getLives(){
        return lives;
    }
    private void changeLives(int deltaL){
        lives += deltaL;
        CharacterStatsChanged?.Invoke();
        if(lives <= 0){
            Debug.Log("[CharacterStatsManager]GAME OVER");
            CharacterGameOver?.Invoke();
            lives = 3;
        }
    }

    public (int, int, int) getItems(){
        return (items[0], items[1], items[2]);
    }
    public void setItems((int item1, int item2, int item3)_items){
        items[0] = _items.item1;
        items[1] = _items.item2;
        items[2] = _items.item3;
        CharacterStatsChanged?.Invoke();
    }
    private void changeItem1(int _item){
        items[0] += _item;
        raiseScore(scoreForItem1);
    }
    private void changeItem2(int _item){
        items[1] += _item;
        raiseScore(scoreForItem2);
    }
    private void changeItem3(int _item){
        items[2] += _item;
        raiseScore(scoreForItem3);
    }

    public (bool, bool, bool) getPowers(){
        return (powers[0], powers[1], powers[2]);
    }
    public void setPowers((bool item1, bool item2, bool item3)_powers){
        powers[0] = _powers.item1;
        powers[1] = _powers.item2;
        powers[2] = _powers.item3;
        CharacterStatsChanged?.Invoke();
    }
    private void changePower1(bool _power){
        powers[0] = _power;
        raiseScore(scoreForPower);
    }
    private void changePower2(bool _power){
        powers[1] = _power;
        raiseScore(scoreForPower);
    }
    private void changePower3(bool _power){
        powers[2] = _power;
        raiseScore(scoreForPower);
    }

    public void setRespawnPoint(Vector3 _respawnPoint){
        respawnPoint = new Vector3(_respawnPoint[0], respawnHigh, _respawnPoint[2]);
    }
    public Vector3 getRespawnPoint(){
        return respawnPoint;
    }

}