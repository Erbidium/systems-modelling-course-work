namespace Lab3;

public static class IdentifierGenerator
{
    private static int _nextId;

    public static int GetId()
        => _nextId++;
}