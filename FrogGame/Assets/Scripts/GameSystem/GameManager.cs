using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int creaturesCought = 0;
    public static int goldCought = 0;
    int maxCombo = 0;
    bool isEndingTheGame = false;

    private void Awake()
    {
        EventSystem.current.onAddingPoints += AddingPoints;
        EventSystem.current.onAddingGold += AddingGold;
        EventSystem.current.onEndingGame += theGameIsEnding;
        EventSystem.current.onSettingCombo += Combo; // Uncoment to accept combos
    }

    //Not sure if keep the combo feature, if I decided to take it out i have to delete the maxCombo, the event and the reference in the tongbehaviour
    public void Combo(int combo)
    {
        if(combo > 0){
            maxCombo++;
        }else{
            maxCombo = 0;
        }
    }

    private void FixedUpdate() {
        if(isEndingTheGame && Time.timeScale != 0){
            int totalEnemies = GameObject.FindGameObjectsWithTag("Collectibles").Length;
            if(totalEnemies == 0){
                EventSystem.current.CompletingGame();
            }
        }
    }

    void AddingPoints(){
        creaturesCought += 1; 
    } 
    void AddingGold(int gold){
        goldCought += gold; 
    } 

    public void theGameIsEnding(bool isEnding){
        isEndingTheGame = isEnding;
    }
}
