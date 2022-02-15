namespace XZMarks.Models;

public record Coordinates(string Name, int X, int Z, int? Y = null)
{
    public override string ToString()
    {
        return Y != null ? $"{X} {Y} {Z}" : $"{X} {Z}";
    }
}