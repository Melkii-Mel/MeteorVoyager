using System;

namespace MeteorVoyager.Assets.Localization.Scripts.TextsNS
{
    [Serializable]
    public class GameProgressionStages
    {
        public string UniversalStage = "There is no hint for this stage of the game. \nThey might appear in the future or you just need to go to touch grass";
        public string StageOne = "Game might feel slow at the start, but progress will speed up as you grow. " +
            "\nYou could make progress faster if you use free booster located at the upper right corner" +
            "\nStage progression requirement: LvL 50 at Spawn Cooldown Upgrade";
        public string StageTwo = "Upgrading damage might be pretty useful because you get more material from asteroids by destroying them completely" +
            "\nEvery 30 levels of upgrading Spawn Cooldown will cause asteroids to become more dense resulting in doubling their health and slight decreasing their amount" +
            "\nThe amount of material that you can obtain from them is proportional to their health, so stronger asteroids yield more material. " +
            "\nTherefore, upgrading your damage becomes even more valuable as you progress further." +
            "\nYou probably want to push to the Charge Attack upgrade because it's quite strong, but it is not really necessary" +
            "\nStage progression requirement: Lvl 1 at charge attack";
        public string StageThree = "New upgrades are now available that allow you to obtain temporary power-ups from glowing asteroids. " +
            "\nAlthough these power-ups don't last very long, they are more useful than they may appear at first glance. " +
            "\nDon't hesitate to upgrade them. " +
            "\nTo progress to the next stage, you must collect one million (1e6) units of matter.";

        public string StageFour = "Now, when you went so far, you can RELOCATE" +
            "\nThis will restart your journey and cause you to lose all your upgrades and matter" +
            "\nHowever, upon relocating, you will gain access to all the data you collected during your previous journey" +
            "\nThis data makes new journey more efficient, but besides passive impact it can be spent on researches" +
            "\nBe careful, though, as unoptimized utilization could make your new journey less efficient than it could be " +
            "\n(though it will still be more efficient than the previous one)";
    }
}
