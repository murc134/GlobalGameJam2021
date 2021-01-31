using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverlayCanvas : MonoBehaviour
{
    private static GameOverlayCanvas instance = null;

    public static GameOverlayCanvas Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<GameOverlayCanvas>();
                if(instance == null)
                {
                    instance = Instantiate<GameOverlayCanvas>(Resources.Load<GameOverlayCanvas>("GameOverlayCanvas"));
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI pressAnyKeyText;

    [SerializeField]
    private Image background;

    private float alpha = 0;

    [SerializeField]
    private float fadeSpeed = 0.25f;

    [SerializeField][Range(0,1)]
    private float maxAlphaBg = 0.9f;

    public float Alpha
    {
        get
        {
            return Mathf.Clamp(alpha, 0.0f, 1.0f); ;
        }
        set
        {
            alpha = Mathf.Clamp(value, 0.0f, 1.0f);

            gameOverText.alpha = alpha;

            Color bgc = background.color;
            bgc.a = alpha* maxAlphaBg;
            background.color = bgc;

        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(gameOverText == null) gameOverText = GetComponentInChildren<TextMeshProUGUI>();
        if (background == null) background = GetComponentInChildren<Image>();

        Alpha = alpha;
    }

    public bool GameOver = false;

    private bool pressAnyKeyAlphaUp = true;

    // Update is called once per frame
    void Update()
    {
        if(GameOver)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Alpha += Time.deltaTime * fadeSpeed;

            
       
            if(Alpha != 1)
            {
                pressAnyKeyText.alpha = Alpha;
            }
            else
            {
                float pressAnyAlpha = Mathf.Clamp(pressAnyKeyText.alpha + (pressAnyKeyAlphaUp ? Time.deltaTime * fadeSpeed : -Time.deltaTime * fadeSpeed), 0.0f, 1.0f);
                if (pressAnyKeyAlphaUp)
                {
                    if (pressAnyAlpha >= 1)
                    {
                        pressAnyKeyAlphaUp = false;
                    }
                }
                else
                {
                    if (pressAnyAlpha <= 0)
                    {
                        pressAnyKeyAlphaUp = true;
                    }
                }
                pressAnyKeyText.alpha = pressAnyAlpha;
            }

        }
        else
        {
            Alpha -= Time.deltaTime * fadeSpeed;
            pressAnyKeyText.alpha = 0;
        }
        
    }

    public void Restart()
    {
        GameOver = false;
    }
}
