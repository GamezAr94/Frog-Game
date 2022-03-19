using UnityEngine;

public class GameManager : MonoBehaviour
{
    int maxCombo = 0;
    bool isEndingTheGame = false;

    private void Awake()
    {
        EventSystem.current.onEndingGame += theGameIsEnding;
        EventSystem.current.onSettingCombo += Combo; // Uncomment to accept combos
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
        //If I want to add a level managed by time I just have to create a switch case where this current if statement is by number of creatures in the scene
        //and other where the time should be greather than zero, in both cases call the complething game.
        if(isEndingTheGame && Time.timeScale != 0){
            int totalEnemies = GameObject.FindGameObjectsWithTag("Collectibles").Length;
            if(totalEnemies == 0){
                EventSystem.current.CompletingGame();
            }
        }
    }

    public void theGameIsEnding(bool isEnding){
        isEndingTheGame = isEnding;
    }
}
