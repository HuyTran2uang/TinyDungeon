[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int currentExp;
    public int currentHealth;
    public int currentMana;
    public int melee;
    public int distance;
    public int magic;
    public int defense;
    public int currentExpMelee;
    public int currentExpDistance;
    public int currentExpMagic;
    public int currentExpDefense;
    public int gold;
    public int diamond;

    public PlayerData()
    {
        name = "Player";
        level = 1;
        currentExp = 0;
        currentHealth = 100;
        currentMana = 100;
        melee = 5;
        distance = 5;
        magic = 5;
        defense = 5;
        currentExpMelee = 0;
        currentExpDistance = 0;
        currentExpMagic = 0;
        currentExpDefense = 0;
        gold = 0;
        diamond = 0;
    }
}
