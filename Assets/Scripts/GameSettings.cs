using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameSeetings Class designed based on Singleton Design Pattern
public class GameSettings 
{
    [Range(0.0f, 1.0f)]
    public float audioVolume = 0.5f;
    public enum GameLight { DayLight,NightLight}
    public GameLight gameLight = GameLight.DayLight;
    public int gameMusicIndex = 1;
    //Game Gradient
    //Game Graphic
    private GameSettings() { }

    public static GameSettings gameSetting;

    public static GameSettings GetInstance()
    {
        if(gameSetting == null)
        {
            gameSetting = new GameSettings();
        }
        return gameSetting;
    }

}