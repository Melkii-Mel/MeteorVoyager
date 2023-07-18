public static class HintsTexts
{
    public const string UNIVERSAL_HINT = "There is no hint for this stage of the game. \nThey might appear in the future or you just need to go to touch grass";
    public const string HINT1 = "Game might feel slow at the start, but progress will speed up as you grow. " +
                                "\nYou could make progress faster if you use free booster located at the upper right corner" +
                                "\nStage progression requirement: LvL 50 at Spawn Cooldown Upgrade";

    public const string HINT2 = "Upgrading damage might be pretty useful because you get more material from asteroids by destroying them completely" +
                                "\nAt some point upgrading Spawn Cooldown will cause asteroids to collaps resulting in doubling their health and halfing their amount " +
                                "\n(but amount of material you can gather from them is also doubled)." +
                                "\nThis makes damage upgrade even more valuable" +
                                "\nYou probably want to push to the Charge Attack upgrade because it's quite strong, but it is not really necessary" +
                                "\nStage progression requirement: Lvl 1 at charge attack";

    public const string HINT3 = "Now you have access to new upgrade type. You may think it's useless" +
                                " because power-ups that drops from glowing asteroids are too short-lived to have some kind of impact." +
                                "\nHowever they are more useful than they might seem to be so feel free to upgrade them" +
                                "\nStage progression requirement: Get to one million material at hands";

    public const string HINT4 = "Now, when you went so far, you can RELOCATE" +
                                "\nThis will result in restarting your journey, loosing all your upgrades and material" +
                                "\nHowever, after relocation you will get access to all the data you have collected throughout the journey" +
                                "\nThis information makes new journey more efficient, but instead of passive impact it can be used to making researches" +
                                "\nBe careful as bad utilizing can make new journey less efficient than it could be (but still more efficient than the first one)";
    public static readonly string[] Hints = { HINT1, HINT2, HINT3, HINT4 };
}