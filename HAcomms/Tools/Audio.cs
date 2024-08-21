using NAudio.CoreAudioApi;

namespace HAcomms.Tools;

public class Audio {
    private readonly MMDevice? _audioDevice;

    public Audio() {
        var deviceEnumerator = new MMDeviceEnumerator();
        _audioDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
    }

    public void HookVolumeNotification(AudioEndpointVolumeNotificationDelegate callback) {
        _audioDevice!.AudioEndpointVolume.OnVolumeNotification += callback;
    }

    public bool IsMuted =>_audioDevice!.AudioEndpointVolume.Mute;
}