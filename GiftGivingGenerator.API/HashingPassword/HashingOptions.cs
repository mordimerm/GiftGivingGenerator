namespace GiftGivingGenerator.API.HashingPassword;

public sealed class HashingOptions
{
	public const string Position = "Position";
	public int Iterations { get; set; } = 10000;
}