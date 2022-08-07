namespace A3ServerTool.Enums;

/// <summary>
/// Provides enum for supported VON codecs.
/// </summary>
public enum VoiceCodecType
{
    [Description("SPEEX")]
    Speex = 0,

    [Description("OPUS")]
    Opus = 1,
}
