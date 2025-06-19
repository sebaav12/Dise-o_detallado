namespace RawDeal;

public class EffectResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public override string ToString()
    {
        return Success ? Message : "Error";
    }
}