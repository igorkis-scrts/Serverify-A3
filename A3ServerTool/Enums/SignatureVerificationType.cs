namespace A3ServerTool.Enums;

/// <summary>
/// Provides helper enum to handle verifySignatures config property.
/// </summary>
public enum SignatureVerificationType
{
    [Description("No verification")]
    NoVerification = 0,

    [Description("v2")]
    SecondVersion = 2
}
