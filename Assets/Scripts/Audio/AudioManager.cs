using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;

    public Sound[] sounds;
    public Sound[] background_themes;



    private Dictionary<string, int> prefixes = new Dictionary<string, int>();
    private Sound current_background;

    void Awake()
    {
        if (AudioManager.audioManager != null)
        {
            Debug.LogError("Audiomanager already inicialized");
            return;
        }

        AudioManager.audioManager = this;
        setSounds(this.sounds);
        setSounds(this.background_themes,true);
        this.changeBackgroundTheme("main");
    }

    private string extractPrefix(string thing)
    {
        if(!string.IsNullOrEmpty(thing))
        {
            Match match = Regex.Match(thing, @"[A-Za-z]+");
            return match.Success ? match.Value : null;
        }
        return null;
    }

    private void setSounds(Sound[] sounds,bool getPrefix=false)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            
            if(getPrefix)
            {
                this.addPrefix(s.name);
            }

        }
    }

    private void addPrefix(string thing)
    {
        string prefix = this.extractPrefix(thing);
        int prefix_value;
        if(!string.IsNullOrEmpty(prefix))
        {
            prefix_value = this.prefixes.ContainsKey(prefix) ? this.prefixes[prefix] : 0;
            this.prefixes[prefix] = ++prefix_value;
        }
    }

    public void play(string name)
    {
        Sound sound = Array.Find(this.sounds, s => s.name == name);
        if(sound != null)
        {
            sound.source.Play();
        }
    }

    public void changeBackgroundTheme(string prefix)
    { 
        int? prefixes_amount = this.prefixes[prefix];
        Debug.Log($"{prefix}: {prefixes_amount}");
        if(prefixes_amount != null)
        {
            System.Random rand_selection = new System.Random();
            string choice = $"{prefix}_{rand_selection.Next(1, (int)prefixes_amount + 1)}";
            Debug.Log($"{choice}");
            Sound sound = Array.Find(this.background_themes, s => s.name == choice);
            if(current_background != null)
            {
                current_background.source.Stop();
            }
            sound.source.Play();
            this.current_background = sound;
        }
    }

}
