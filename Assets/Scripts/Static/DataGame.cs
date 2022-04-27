using System.Collections.Generic;

namespace GranGames.Static
{
    public class DataGame
    {
        private static int currentTier = 1;

        public static bool win; 
        public const string PREFAB_ENDGAME = "PopEndGame";

        public const string GAMEPLAY_SCENE = "GamePlay";
        public const string SELECTPLAYER_SCENE = "SelectGroup";
        public const string MENUSTART_SCENE = "StartMenu";

        public static List<Character> _selectedPlayers;

        //Data Game Status
        public static string CURRENT_TEXT_STATE = "...";
        public const string SELECT_PLAYER = "Came on... Choose one Player to attack";
        public const string PLAYER_ATTACK = "Yeah... Let's Rock";
        public const string SELECT_ENEMY = "kick him... Choose an enemy to catch";
        public const string ENEMY_ATTACK = "Please, Wait... Enemy attacking. So sorry";

        //Getters and Setters
        public static int CurrentTier { get => currentTier; set => currentTier = value; }
    }
}