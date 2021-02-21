using TMPro;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Load the data stored to the in-game UI
    /// </summary>
    public class LoadData 
    {
        public void Xp(int playerXp, TextMeshProUGUI playerXpText) 
            => playerXpText.SetText($"Current Xp: {playerXp.ToString()}");
        public void Gold(int playerGold, TextMeshProUGUI playerGoldText) 
            => playerGoldText.SetText($"Current Gold: {playerGold.ToString()}");

        public void Text(GameManager.GameState gameState, Button button, TextMeshProUGUI tittle)
        {
            switch (gameState)
            {
                case GameManager.GameState.GameLost:
                    //Todo - Refactor
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Restart");
                    tittle.SetText("You S*ck!");
                    break;
                case GameManager.GameState.GamePause:
                    //Todo - Refactor
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Continue");
                    tittle.SetText("Pause");
                    break;
                case GameManager.GameState.GameWon:
                    //Todo - Refactor
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Next Level");
                    tittle.SetText("Win Win Win");
                    break;
            }
        }

        public void Images()
        {
            //Todo
        }

    }
}
