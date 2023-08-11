// NOTE: these are types not implemented

namespace Simulations.Mobile;

#if !IOS && !MACCATALYST
static class Sound
{
    public static void Play() { }
}
#endif