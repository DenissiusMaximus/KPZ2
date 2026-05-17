namespace LightHTML.Core;

public interface IElementState
{
    string Name { get; }
    IEnumerable<string> AdditionalClasses { get; }
    IReadOnlyDictionary<string, string> AdditionalAttributes { get; }

    IElementState OnEnable();
    IElementState OnDisable();
    IElementState OnFocus();
    IElementState OnBlur();
}

public abstract record ElementStateBase : IElementState
{
    public abstract string Name { get; }
    public virtual IEnumerable<string> AdditionalClasses => [];
    public virtual IReadOnlyDictionary<string, string> AdditionalAttributes =>
        new Dictionary<string, string>();

    public virtual IElementState OnEnable()  => this;
    public virtual IElementState OnDisable() => DisabledState.Instance;
    public virtual IElementState OnFocus()   => new FocusedState();
    public virtual IElementState OnBlur()    => NormalState.Instance;
}

public sealed record NormalState : ElementStateBase
{
    public static readonly NormalState Instance = new();
    public override string Name => "normal";
}

public sealed record FocusedState : ElementStateBase
{
    public override string Name => "focused";
    public override IEnumerable<string> AdditionalClasses => ["focused"];
    public override IReadOnlyDictionary<string, string> AdditionalAttributes =>
        new Dictionary<string, string> { ["aria-focused"] = "true" };
    public override IElementState OnBlur() => NormalState.Instance;
}

public sealed record DisabledState : ElementStateBase
{
    public static readonly DisabledState Instance = new();
    public override string Name => "disabled";
    public override IEnumerable<string> AdditionalClasses => ["disabled"];
    public override IReadOnlyDictionary<string, string> AdditionalAttributes =>
        new Dictionary<string, string> { ["disabled"] = "" };
    public override IElementState OnEnable() => NormalState.Instance;
    public override IElementState OnFocus()  => this;
}
