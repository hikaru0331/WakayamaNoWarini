using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance_AudioManager;
    private float currentSEVolume = 1.0f;
    private float currentBGMVolume = 1.0f;
    private float bgmVolume = 1.0f;
    private float seVolume = 1.0f;

    private void Awake()
    {
        if (instance_AudioManager == null)
        {
            instance_AudioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioData audioData;

    [SerializeField] private AudioSource SESource;
    [SerializeField] private AudioSource BGMSource;

    public float BGMVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = Mathf.Clamp01(value);
            UpdateBGMVolume();
        }
    }

    public float SEVolume
    {
        get { return seVolume; }
        set
        {
            seVolume = Mathf.Clamp01(value);
            UpdateSEVolume();
        }
    }

    private void UpdateBGMVolume()
    {
        BGMSource.volume = bgmVolume * currentBGMVolume;
    }

    private void UpdateSEVolume()
    {
        SESource.volume = seVolume * currentSEVolume;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] tmp = this.GetComponents<AudioSource>();
        this.SESource = tmp[0];
        this.BGMSource = tmp[1];

        CheckOverlap(this.audioData.SE_Data, "SE_Data");
        CheckOverlap(this.audioData.BGM_Data, "BGM_Data");

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            seVolume = PlayerPrefs.GetFloat("SEVolume");
        }

        UpdateBGMVolume();
        UpdateSEVolume();

    }

    //オーディオIDが重複していないかを確認する
    private void CheckOverlap(List<Datum> data, string variable_name)
    {
        List<int> vs = new List<int>();
        for (int i = 0; i < data.Count; i++)
        {
            if (vs.Contains(data[i].id))
            {
                Debug.LogError(string.Format("{0} のID {1} が重複しています。", variable_name, data[i].id));
            }
            else
            {
                vs.Add(data[i].id);
            }
        }
    }

    //オーディオIDをindexに変換する
    public int ConvertIdIntoIndex(List<Datum> data, int id)
    {
        for (int index = 0; index < data.Count; index++)
        {
            if (id == data[index].id)
            {
                return index;
            }
        }

        Debug.LogError(string.Format("指定されたid {0} のデータは存在しません。", id));

        return -1;
    }


    public void PlaySE(int id)
    {
        int index = this.ConvertIdIntoIndex(this.audioData.SE_Data, id);
        if (index == -1) return;
        this.SESource.clip = this.audioData.SE_Data[index].clip;
        currentSEVolume = this.audioData.SE_Data[index].volume;
        UpdateSEVolume();
        this.SESource.Play();
    }

    public void StopSE()
    {
        this.SESource.Stop();
    }

    public void PauseSE()
    {
        this.SESource.Pause();
    }

    public void UnPauseSE()
    {
        this.SESource.UnPause();
    }

    public void PlayBGM(int id)
    {
        int index = this.ConvertIdIntoIndex(this.audioData.BGM_Data, id);
        if (index == -1) return;
        if (BGMSource.isPlaying && BGMSource.clip == audioData.BGM_Data[index].clip)
        {
            return;
        }
        this.BGMSource.clip = this.audioData.BGM_Data[index].clip;
        currentBGMVolume = this.audioData.BGM_Data[index].volume;
        UpdateBGMVolume();
        this.BGMSource.Play();
    }

    public void StopBGM()
    {
        this.BGMSource.Stop();
    }

    public void PauseBGM()
    {
        this.BGMSource.Pause();
    }

    public void UnPauseBGM()
    {
        this.BGMSource.UnPause();
    }
}