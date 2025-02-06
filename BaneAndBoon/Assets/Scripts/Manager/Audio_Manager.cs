using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [Header("Audios")]
    [SerializeField] AudioSource move;
    [SerializeField] AudioSource switchBG;
    [SerializeField] AudioSource lightBGM;
    [SerializeField] AudioSource shadowBGM;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource dash;

    [Header("Settings")]
    private bool lastShadowState;

    private void Start()
    {
        Player_Manager.instance.player.onMove += MoveAudio;
        Player_Manager.instance.player.stopMove += StopMoveAudio;
        Player_Manager.instance.player.onJump += JumpAudio;
        Player_Manager.instance.player.onDash += DashAudio;
        SwitchManager.instance.onSwitch += SwitchAudio;
    }

    private void Update()
    {
        if (Player_Manager.instance.player.inShadowState != lastShadowState)
        {
            lastShadowState = Player_Manager.instance.player.inShadowState;
            UpdateBGM();
        }
    }
    private void UpdateBGM()
    {
        if (Player_Manager.instance.player.inShadowState)
        {
            lightBGM?.Stop();
            shadowBGM?.Play();
        }
        else
        {
            shadowBGM?.Stop();
            lightBGM?.Play();
        }
    }

    private void DashAudio()
    {
        dash.Play();
    }

    private void JumpAudio()
    {
        jump.Play();
    }
    private void MoveAudio()
    {
        move.Play();
    }

    private void StopMoveAudio()
    {
        move.Stop();
    }

    private void SwitchAudio()
    {
        switchBG.Play();
    }

    private void OnDisable()
    {
        Player_Manager.instance.player.onMove -= MoveAudio;
        Player_Manager.instance.player.stopMove -= StopMoveAudio;
        SwitchManager.instance.onSwitch -= SwitchAudio;
        Player_Manager.instance.player.onJump -= JumpAudio;
        Player_Manager.instance.player.onDash -= DashAudio;
    }
}
