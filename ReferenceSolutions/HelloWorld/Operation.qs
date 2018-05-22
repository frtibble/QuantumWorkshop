namespace HelloWorld
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;

    operation Greet (who : String) : (String)
    {
        body
        {
            return $"Hello, {who}!";
        }
    }
}
