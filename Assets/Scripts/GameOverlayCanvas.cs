using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private Image background;

    private float alpha = 0;

    [SerializeField]
    private float fadeSpeed = 0.25f;

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
            bgc.a = alpha;
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

    // Update is called once per frame
    void Update()
    {
        if(GameOver)
        {
            Alpha += Time.deltaTime * fadeSpeed;
        }
        else
        {
            Alpha -= Time.deltaTime * fadeSpeed;
        }
    }

    public void Restart()
    {
        GameOver = false;
    }
}
