using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;
using System.IO;
using System.Linq;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    int battleStatus;

    const int BATTLE_START = 0;
    const int BATTLE_PLAY = 1;
    const int BATTLE_END = 2;

    float timer;
    float endTime;
    float scoreTime;

    public GameObject enemy;
    //GameObject player;

    public Image messageStart;
    public Image messageWin;
    public Image messageLose;
    public Image rankingBase;

    public Text time;//残り時間
    public Text destroyScore;
    public Text point;
    public Text ranking;
    public Text debug;
    public Text rankingTitle;
    public Text levelText;
    public Text levelScoreText;

    public static int score;//敵を倒した数。Enemyスクリプトでカウントアップ
    public static int scorePoints;//スコア、これで競ってもらいたい
    private float scorePointsScreen;

    private static int[] highScore = new int[99999];//= {0,0,0,0,0,0};
    private static int[] highScore_C = new int[99999];//= {0,0,0,0,0 };
    private static int[] highDestroy = new int[99999];//= { 0,0,0,0,0};
    private  int[] sort = new int[99999];// ={ 0,0,0,0,0};
    private static float[] highTime = new float[99999];//= { 0,0,0,0,0};
    private static int[] highLevel=new int[99999];
    private static int[] highHp=new int[99999]; 

    private static List<int> highScore_L = new List<int>();
    private List<int> highScore_L_C = new List<int>();
    private static List<int> highDestroy_L = new List<int>();
    private List<int> sort_L = new List<int>();
    private static List<float> highTime_L = new List<float>();
    private static List<int> highLevel_L = new List<int>();
    private static List<int> highHp_L = new List<int>();


    //int capacity = 3;

    private int count = 0;
    private int cou = 0;
    private int max=10;
    //レベルシステム
    private int level=1;
    private int defaultLevelScore=3;
    private int levelScore=0;

    //int clearScore;
    //終了時のカメラ
    public Camera resultCamera;

	// Use this for initialization
	void Start () {
        level = 1;
        defaultLevelScore=3;
        levelScore = defaultLevelScore;
        levelText.enabled = true;
        levelScoreText.enabled = true;
        levelText.text = string.Format("Level_{0:00}",level);
        levelScoreText.text = string.Format("Next to Level -> {0:000}", levelScore);

        //player = GameObject.FindWithTag("Player");
        /*highScore_L.Capacity = capacity;
        highScore_L_C.Capacity = capacity;
        highDestroy_L.Capacity = capacity;
        highTime_L.Capacity = capacity;
        sort_L.Capacity = capacity;
        */
        ranking.enabled = false;
        rankingTitle.enabled = false;
        rankingBase.enabled = false;

        resultCamera.enabled = false;

        battleStatus = BATTLE_START;

        timer = 0;
        endTime = 180f;
        scoreTime = 0.0f;

        messageStart.enabled = true;
        messageWin.enabled = false;
        messageLose.enabled = false;

        score = 0;

        scorePoints = 0;
        scorePointsScreen = 0;

        enemy.SetActive(true);
        //敵の最大生成数をクリアにする
        //clearScore = EnemyInstantiate.instantiateValue;
    }

    // Update is called once per frame
    void Update()
    {
        //レベルシステム
        if (score >= levelScore)
        {
            levelScore += defaultLevelScore + Random.Range(level - 3, level);
            level++;
            if (shotPlayer.damageDefault <= 1000)
            {
                shotPlayer.damageDefault += Random.Range(17, 25);
            }
            if (PlayerAp.armorpointMax <= 20000)
            {
                PlayerAp.armorpointMax += Random.Range(100, 150);
            }
            PlayerAp.armorpoint = PlayerAp.armorpointMax;
            if (PlayerShoot.shotIntervalMax >= 0.15f)
            {
                PlayerShoot.shotIntervalMax -= 0.02f;
            }
            EnemyInstantiate.instantiateValue++;
            if (Enemy.defaultArmorpointMax <= 3000)
            {
                Enemy.defaultArmorpointMax += Random.Range(20, 60);
            }
            endTime += Random.Range(10f, 25f);
        }
            levelText.text = string.Format("Level.{0:000}", level);
        levelScoreText.text = string.Format("Next to Level -> {0:000} Destroy", (levelScore - score));

        time.text = string.Format("{0:000.00}", endTime);

        destroyScore.text = "Destroy: " + score.ToString();

        Debug.Log("score");

            if (battleStatus != BATTLE_END)
            {
                scorePoints += (int)(Time.deltaTime * 500);
                endTime -= Time.deltaTime;
                scoreTime += Time.deltaTime;
            }
            if (scorePoints != scorePointsScreen)
            {
                scorePointsScreen = (int)Mathf.Lerp(scorePointsScreen, scorePoints, 0.5f);
            }

            point.text = string.Format("SCORE: {0:00000000}", scorePointsScreen);

        switch (battleStatus)
        {
            case BATTLE_START:
                timer += Time.deltaTime;

                if (timer > 3)
                {
                    messageStart.enabled = false;
                    battleStatus = BATTLE_PLAY;
                    timer = 0;
                    CharacterMotion.integer = 0;
                }

                break;

            case BATTLE_PLAY:
                if (endTime <= 0f)
                {
                    battleStatus = BATTLE_END;
                    CharacterMotion.result = 1;
                    messageWin.enabled = true;
                    CharacterMotion.integer = 1;
                }
                if (PlayerAp.armorpoint <= 0)
                {
                    battleStatus = BATTLE_END;
                    CharacterMotion.result = 2;
                    messageLose.enabled = true;
                    CharacterMotion.integer = 2;
                }
                break;

            case BATTLE_END:

                PlayerAp.armorpoint = PlayerAp.armorpointMax;
                resultCamera.enabled = true;
                //死後の余韻(一定時間経過でタイトルへ遷移)
                timer += Time.deltaTime;

                //enemy.SetActive(false);

                if (timer > 3)
                {
                    //動きを止める
                    Time.timeScale = 0;
                    if (count == 0)
                    {
                        /*highScore[cou] = scorePoints;
                        highDestroy[cou] = score;
                        hightime[cou] = endTime;*/
                        //リスト型でランキングしたい
                        highScore_L.Add(scorePoints);
                        highDestroy_L.Add(score);
                        highTime_L.Add(scoreTime);
                        highLevel_L.Add(level);
                        highHp_L.Add(PlayerAp.armorpointMax);

                        //result[cou]=scorePoints;
                        //debug.text = result[cou].ToString();
                        Comparison();
                        //debug.text +="\n" +count.ToString();
                        count++;
                    }

                    if (Input.GetButtonDown("Fire1"))
                    {
                        count = 0;
                        SceneManager.LoadScene("Title");

                        //動きを再開
                        Time.timeScale = 1;
                    }
                }
                break;

            default:
                break;
        }
    }
    void Comparison()
    {
        //配列
        /*for(int i=0; i < 5; i++)
        {
            highScore_C[i] = highScore[i];
        }
        */
        //リスト
        foreach(int num in highScore_L)
        {
            highScore_L_C.Add(num);
        }

        //ここにソートを書いてくれよなぁ～たのむよ～
        highScore_L_C.Sort();
        highScore_L_C.Reverse();
        /*for (int i = 0; i < 5; i++)
        {
            debug.text += highScore_C[i].ToString()+"\n";
        }*/
        //int rank=0;

        //配列idとりだし
        /*for (int i=0; i<5; i++)
         {
            if (highScore[i] != 0)
            {
                //int rank_C = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (highScore_C[i] == highScore[j])
                    {
                        sort[i] = j;
                        if (highScore[j] == 0)
                        {
                            sort[j] = j;
                        }
                    }
                    //rank_C++;
                }
                //rank++;
            }
         }*/

        //リストidとりだし
        int a = 0;
        foreach  (int i in highScore_L_C)
        {
            //カウント
            a=0;
            foreach(int j in highScore_L)
            {
                if (j==i)//
                {
                    sort_L.Add(a);
                }
                a++;
            }
        }
        // debug.text += "\n";
        foreach (var Sc in highScore_L.Select((v, i) => new { Value = v, Index = i }))
        {
           cou = Sc.Index;
           /* if (cou < max)
            {*/
                highScore[cou] = Sc.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var D in highDestroy_L.Select((v,i)=>new {Value=v,Index=i}))
        {
            cou = D.Index;
            /*if (cou < max)
            {*/
                highDestroy[cou] = D.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var T in highTime_L.Select((v, i) => new { Value = v, Index = i }))
        {
            cou = T.Index;
            /*if (cou < max)
            {*/
                highTime[cou] = T.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var S in highScore_L_C.Select((v, i) => new { Value = v, Index = i }))
        {
            cou = S.Index;
            /*if (cou < max)
            {*/
                highScore_C[cou] = S.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var L in highLevel_L.Select((v, i) => new { Value = v, Index = i }))
        {
            cou = L.Index;
            /*if (cou < max)
            {*/
                highLevel[cou] = L.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var H in highHp_L.Select((v, i) => new { Value = v, Index = i }))
        {
            cou = H.Index;
            /*if (cou < max)
            {*/
                highHp[cou] = H.Value;
            /*}
            else
            {
                break;
            }*/
        }
        cou = 0;
        foreach (var st in sort_L.Select((v, i) => new { Value = v, Index = i }))
        {
            cou = st.Index;
            sort[cou] = st.Value;

            if (cou < max)
            {
            }
            else
            {
                cou--;
                break;
            }
        }

        for (int i = 0; i <= cou; i++)
        {
            if (highScore_C[i] == highScore[cou])
            {
                ranking.text += "\n▶";
            }
            else
            {
                ranking.text += "\n";
            }
            ranking.text += string.Format("{0:00} : {1:00000000} {2:0000} {3:0000.00} / {4:000} {5:00000}",(i + 1),highScore_C[i],highDestroy[sort[i]],highTime[sort[i]],highLevel[sort[i]],highHp[sort[i]]);
        }
        ranking.enabled = true;
        rankingTitle.enabled = true;
        rankingBase.enabled = true;
        /*if (a > max)
        {
            for (int i= max; i < a; i++)
            {
                highScore_L_C.Remove(i);
                highScore_L.Remove(sort[i]);
                highTime_L.Remove(sort[i]);
                highDestroy_L.Remove(sort[i]);
            }
        }*/
    }
}
