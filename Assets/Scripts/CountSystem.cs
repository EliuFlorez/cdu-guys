using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountSystem : MonoBehaviour
{
    [Header("Menu Coins")]
    public int countCoins;
    public TMP_Text textCoins;
    public AudioClip audioCoin;

    [Header("Menu Gems")]
    public int countGems;
    public TMP_Text textGems;
    public AudioClip audioGem;

    // Private
    private AudioSource _audio;

    private void Awake()
    {
        // Audio
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Increase(Vector3 position, string type)
    {
        switch (type)
        {
            case "coin":
                // Audio
                _audio.clip = audioCoin;
                _audio.Play();

                // Coutn
                countCoins += 1;
                if (countCoins > 99)
                {
                    countCoins = 99;
                }

                textCoins.text = countCoins.ToString("0");
                break;
            case "gem":
                // Audio
                _audio.clip = audioGem;
                _audio.Play();

                // Coutn
                countGems += 1;
                if (countGems > 99)
                {
                    countGems = 99;
                }

                textGems.text = countGems.ToString("0");
                break;
            default:
                // code block
                break;
        }
    }

    public void Decrease(string type, int min)
    {
        switch (type)
        {
            case "coin":
                // Audio
                _audio.clip = audioCoin;
                _audio.Play();

                // Coutn
                countCoins -= min;
                if (countCoins < 0)
                {
                    countCoins = 0;
                }

                textCoins.text = countCoins.ToString("0");
                break;
            case "gem":
                // Audio
                _audio.clip = audioGem;
                _audio.Play();

                // Coutn
                countGems -= min;
                if (countGems < 0)
                {
                    countGems = 0;
                }

                textGems.text = countGems.ToString("0");
                break;
            default:
                // code block
                break;
        }
    }
}
