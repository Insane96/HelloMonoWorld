using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using MonoEngine;

namespace HelloMonoWorld.Game
{
    public static class Sounds
    {
        public static SoundEffect SwordSwing { get; private set; }
        public static SoundEffect Hit { get; private set; }
        public static SoundEffect HitPunch { get; private set; }
        public static SoundEffect RockSpell { get; private set; }
        public static SoundEffect RockHit { get; private set; }
        public static SoundEffect WaterSpell { get; private set; }
        public static SoundEffect WaterHit { get; private set; }

        public static void LoadContent(ContentManager contentManager)
        {
            SwordSwing = contentManager.Load<SoundEffect>("sounds/swing");
            Hit = contentManager.Load<SoundEffect>("sounds/hit");
            HitPunch = contentManager.Load<SoundEffect>("sounds/hit_punch");
            RockSpell = contentManager.Load<SoundEffect>("sounds/rocks_spell_01");
            RockHit = contentManager.Load<SoundEffect>("sounds/rocks_hit_01");
            WaterSpell = contentManager.Load<SoundEffect>("sounds/water_spell_01");
            WaterHit = contentManager.Load<SoundEffect>("sounds/water_hit_02");
        }

        //TODO Left-Right sounds with pan
        public static void PlaySound(SoundEffect sound, float volume, float pitch)
        {
            sound.Play(volume * Options.Volume, pitch, 0f);
        }
        public static void PlaySound(SoundEffect sound, float volume)
        {
            PlaySound(sound, volume, 0f);
        }
        public static void PlaySound(SoundEffect sound)
        {
            PlaySound(sound, 1f, 0f);
        }

        public static void PlaySoundVariated(SoundEffect sound, float volume, float pitchVariation)
        {
            PlaySound(sound, volume, Mth.NextFloat(MainGame.random, -pitchVariation, pitchVariation));
        }
    }
}
