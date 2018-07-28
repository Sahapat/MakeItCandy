using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    public bool _haveCandy;
    public float _candyPercent;
    public bool _haveTopie;
    public float _TopiePercent;
    public bool _havePoppop;
    public float _PoppopPercent;
    public bool _haveIcecream;
    public float _IcecreamPercent;
    public bool _haveCandyFloss;
    public bool _haveSprinkle;
    public float _SprinklePercent;
    public bool _haveCone;
    public bool _haveTrash;
    public int _amount;
    public int _amountBox;

    public int _second;
    public int _minute;

    private LevelLoader load;
    private void Awake()
    {
        load = FindObjectOfType<LevelLoader>();
    }
    private void OnMouseDown()
    {
        LevelManager.haveCandyMachine = _haveCandy;
        LevelManager.haveTopieMachine = _haveTopie;
        LevelManager.havePoppopMachine = _havePoppop;
        LevelManager.haveIceCreamMachine = _haveIcecream;
        LevelManager.haveConeMachine = _haveCone;
        LevelManager.haveSpinkle = _haveSprinkle;
        LevelManager.haveFloss = _haveCandyFloss;
        LevelManager.haveTrash = _haveTrash;
        LevelManager.candyPercent = _candyPercent;
        LevelManager.IceCreamPercent = _IcecreamPercent;
        LevelManager.PoppopPercent = _PoppopPercent;
        LevelManager.sprinklePercent = _SprinklePercent;
        LevelManager.topiePercent = _TopiePercent;
        LevelManager.amount = _amount;
        LevelManager.amountBox = _amountBox;
        LevelManager.second = _second;
        LevelManager.minute = _minute;
        load.LoadScene(3);
    }
}
