using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private TextMeshProUGUI volumeDisplay;
    [SerializeField] private float displayDuration = 5.0f;

    private float musicPercent = 1.0f;
    private float sfxPercent = 1.0f;
    private Coroutine fadeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMixerAndUI();
        volumeDisplay.gameObject.SetActive(false);
        ShowDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = false;

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            AdjustMusic(-0.1f);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AdjustMusic(0.1f);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            AdjustSFX(-0.1f);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            AdjustSFX(0.1f);
            changed = true;
        }

        if (changed)
        {
            ShowDisplay();
        }    
    }

    void ShowDisplay()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        volumeDisplay.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        volumeDisplay.gameObject.SetActive(false);
    }

    void AdjustMusic(float amount)
    {
        musicPercent = Mathf.Clamp01(musicPercent + amount);
        UpdateMixerAndUI();
    }

    void AdjustSFX(float amount)
    {
        sfxPercent = Mathf.Clamp01(sfxPercent + amount);
        UpdateMixerAndUI();
    }

    void UpdateMixerAndUI()
    {
        myMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(musicPercent, 0.0001f)) * 20);
        myMixer.SetFloat("SFXVol", Mathf.Log10(Mathf.Max(sfxPercent, 0.0001f)) * 20);

        volumeDisplay.text = $"Music (-/=): {Mathf.Round(musicPercent * 100)}%\n" +
                             $"SFX ([/]): {Mathf.Round(sfxPercent * 100)}%";
    }

    void UpdateUI()
    {
        volumeDisplay.gameObject.SetActive(true);
    }
}
