using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    
    [SerializeField] private GameObject testSubject;
    
    private StatsController _script;
    private int _score, item1, item2, item3;
    private float timer;
    private bool l1, l2, l3;

    void Start()
    {
        _script = testSubject.GetComponent<StatsController>();
        _score = item1 = item2 = item3 = 10;
        l1 = l3 = true;
        l2 = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f){
            Debug.Log("5s timer");
            timer = 0;
            _score += 5;
            item1 += 1;
            item2 += 2;
            item3 += 3;
            _script.updateScore(_score);
            _script.updateItems((item1, item2, item3));
            _script.updateLives((l1, l2, l3));
            _script.updatePowers((l1, l2, l3));
            l2 = l3;
            l1 = l2;
            l3 = false;
        }
    }
}
