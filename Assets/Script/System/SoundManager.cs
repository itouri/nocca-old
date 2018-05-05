using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Sound
{
    // サウンドアセットと同じ名前
    public enum ID
    {
        NONE = -1,

        PUTTING_PIECE = 0,
        SELECTING_PIECE,
        END,
    }
}

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> clips;
    public List<Slot> slots;

    public class Slot
    {

        public AudioSource source = null;
        public float timer = 0.0f;
        public bool single_shot = false;
    };

    void Awake()
    {
        this.slots = new List<Slot>();
        //TODO
        Slot slot = new Slot();

        slot.source = this.gameObject.AddComponent<AudioSource>();
        slot.timer = 0.0f;

        this.slots.Add(slot);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO Sound.SLOTに対応
    public void PlaySE(Sound.ID sound_id)
    {
        AudioClip clip = this.clips[(int)sound_id];
        Slot slot = this.slots[0];
        slot.source.PlayOneShot(clip);
    }

    private static SoundManager instance = null;

    public static SoundManager GetInstance()
    {
        if (SoundManager.instance == null)
        {
            // TODO 自分自身を返すんだから Find しなくてもいいのでは?
            SoundManager.instance = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }
        return (SoundManager.instance);
    }
}