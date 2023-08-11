#if IOS || MACCATALYST
using Foundation;
using AVFoundation;

namespace Simulations.Mobile;

static class Sound
{
    static readonly AVAudioPlayer _player = AVAudioPlayer.FromUrl(NSBundle.MainBundle.GetUrlForResource("chomp1", "wav"));

    public static void Play() => _player.Play();
}
#endif