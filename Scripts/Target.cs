using Godot;

public interface ITarget
{
	int ScoreForHit { get; }
}
public partial class Target : BaseButton, ITarget
{
	private Score _score = null!;

	public virtual int ScoreForHit { get; } = 1;

	public override void _Ready()
		=> _score = this.GetScore();

	public override void _Pressed()
		=> _score.AddScore(this);
}