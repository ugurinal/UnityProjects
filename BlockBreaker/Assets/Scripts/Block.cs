using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject powerUps;
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TMP_FontAsset[] fontAssets;

    //cached references
    private Level _level;

    // components variables
    private TextMeshProUGUI _meshPro;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _level = FindObjectOfType<Level>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _meshPro = GetComponentInChildren<TextMeshProUGUI>();
        curHp = maxHp;
        _meshPro.text = curHp.ToString();

        switch (tag)
        {
            case "1HPBlock":
                _level.Inc1HpBlock();
                break;
            case "2HPBlock":
                _level.Inc2HpBlock();
                break;
            case "3HPBlock":
                _level.Inc3HpBlock();
                break;
            case "4HPBlock":
                _level.Inc4HpBlock();
                break;
            case "5HPBlock":
                _level.Inc5HpBlock();
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;

        curHp--;
        _level.IncScore();
        if (curHp <= 0)
        {
            Destroy(gameObject);
            _level.DecCtr();
            CreatePowerUp();
        }
        else
        {
            _spriteRenderer.sprite = sprites[curHp - 1];
            _meshPro.font = fontAssets[curHp - 1];
            _meshPro.text = curHp.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        _level.IncScore(curHp);
        Destroy(gameObject);
        _level.DecCtr();
        CreatePowerUp();
    }

    private void CreatePowerUp()
    {

        var randChance = Random.Range(1f, 100f);
        if (randChance <= 85) return;


        var randPower = (int)Random.Range(0f, 5f);
        var transformVar = transform;

        GameObject powerUp = Instantiate(powerUps.transform.GetChild(randPower).gameObject, transformVar.position,
            transformVar.rotation);
        powerUp.SetActive(true);
    }
}