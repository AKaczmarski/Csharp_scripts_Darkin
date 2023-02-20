using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsInGame : MonoBehaviour
{
    public AudioSource coins;                           //Deklaracja zmiennej dla obiektu AudioSource
    public AudioSource medkit;
    public AudioSource falldeath;
    public AudioSource deathfromzombie;
    public AudioSource winGame;
    public AudioSource zombieBite;
    public AudioSource spikeHit;
    public AudioSource menu;
    public AudioSource jumping;
    public AudioSource forest;
    public AudioSource castle;
    public AudioSource footsteps;
    public AudioSource zombieroar;
    public AudioSource zombiehorde;
    public AudioSource vents;

        public void coinsSound() => coins.Play();                   //funkcja do odtwarzania dzwi�ku gdy jest wywo�ywana
        public void medkitSound() => medkit.Play();                 //efekt dzwiekowy jest okre�lany przez zmienn� np. coins medkit, kt�ra zak�ada, �e jest to obiekt, kt�ry mo�e odtwarza� dzwi�k
        public void falldeathSound() => falldeath.Play();
        public void deathfromzombieSound() => deathfromzombie.Play();
        public void winGameSound() => winGame.Play();
        public void zombieBiteSound() => zombieBite.Play();
        public void spikeHitSound() => spikeHit.Play();
        public void menuSound() => menu.Play();
        public void jumpSound() => jumping.Play();
        public void forestSound() => forest.Play();
        public void forestSoundOff()                            //funkcja aby wy��czy� dzwi�k
    {
        forest.Stop();
    }
        public void castleSound() => castle.Play();
        public void silence()
    {
        castle.Stop();
    }
        public void footstepsSound() => footsteps.Play();
        public void footOffSound()
        {
            footsteps.Stop();
        }

        public void zombieroarSound() => zombieroar.Play();
        public void zombiehordeSound() => zombiehorde.Play();
        public void ventsSound() => vents.Play();
        
}
