namespace HelloMonoWorld.Game.Spell
{
    public static class Spells
    {
        public static BasicSpell MagicBullet = new(1f, 0f, 250f, 2f);
        public static EnergyBallSpell EnergyBall = new(1f, 0f, 350f, 5f);
    }
}
