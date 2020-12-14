using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : ReferenceClass<GameManager>
{
    //For vertical slice add code and functionality to different archers

    public ArcherButtonManager ClickedBtn { get; private set; }
    private UnityArcher selectedArcher;
    private int currency;
    private int monsterHealth = 15;
    public ObjectPool Pool { get; set; }
    public bool IsWaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }


    public int Currency {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            this.currencyText.text = value.ToString() + "<color=red> Respect</color>";
        }

    }

   [SerializeField] private Text currencyText;
    private int wave = 0;

    [SerializeField] private Text waveText;
    [SerializeField] private GameObject waveBtn;
    [SerializeField] private GameObject controlsBtn;
    [SerializeField] private GameObject controlsPanel;
    public int lives;
    [SerializeField] public Text livesText;
    [SerializeField] public Text wavesCompleted;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private Text sellText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;
    
    private bool isPanelActive;

    List<UnityMonster> activeMonsters = new List<UnityMonster>();

    public UnityMonster Monsters { get; set; }



    public void Awake()
    {
        Monsters = GetComponent<UnityMonster>();
        Pool = GetComponent<ObjectPool>();
        Currency = 5;
        lives = 3;
        livesText.text = string.Format("Lives: <color=lime>{0}</color>", lives);
        panel.SetActive(false);
        controlsPanel.SetActive(false);
        isPanelActive = false;
    }
    public void Update()
    {
        if (lives == 0)
        {
            LoseState();
        }
    }
    public void PickArcher(ArcherButtonManager archerBtn)
    {
        if (Currency >= archerBtn.Price && !IsWaveActive)
        {
            this.ClickedBtn = archerBtn;
        }
        
    }

    public void BuyArcher()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            ClickedBtn = null;
        }
        
    }

    public void SelectArcher(UnityArcher archer)
    {
        if (selectedArcher != null)
        {
            selectedArcher.Select();
        }

        selectedArcher = archer;
        selectedArcher.Select();
        sellPanel.SetActive(true);
    }

    public void DeselectArcher()
    {
        if (selectedArcher != null)
        {
            selectedArcher.Select();
        }
        sellText.text = string.Format("<color=lime>+{0}</color>",(selectedArcher.Price / 2));
        sellPanel.SetActive(false);
        selectedArcher = null;
        
    }
    public void StartWave()
    {
        wave++;
        waveText.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
        controlsBtn.SetActive(false);

        

    }
    //Coroutines
    private IEnumerator SpawnWave()
    {
        UnityGridManager.Instance.GeneratePath();
        for (int i = 0; i < wave; i++)
        {
            
            int monsterIndex = Random.Range(0, 4);

            string type = string.Empty;
            switch (monsterIndex)
            {
                case 0:
                    type = "BlueMonster";
                    break;
                case 1:
                    type = "YellowMonster";
                    break;
                case 2:
                    type = "MagentaMonster";
                    break;
                case 3:
                    type = "GreenMonster";
                    break;
            }
            UnityMonster monster = Pool.GetObject(type).GetComponent<UnityMonster>();
            monster.Spawn(monsterHealth);
            activeMonsters.Add(monster);

            if (wave % 3 == 0)
            {
                monsterHealth += 5;
            }


            yield return new WaitForSeconds(1.5f);

        }
        
    }

    public void RemoveMonster(UnityMonster monster)
    {
        activeMonsters.Remove(monster);

        if (!IsWaveActive)
        {
            waveBtn.SetActive(true);
            controlsBtn.SetActive(true);
        }
    }
    public void LoseState()
    {
        isPanelActive = true;
        panel.SetActive(true);

        if (isPanelActive)
        {
            wavesCompleted.text = string.Format("You completed: <color=lime>{0}</color> Waves", wave);
        }


    }
    public void ViewControls()
    {
        controlsPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        controlsPanel.SetActive(false);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

}
