using System.Collections.Generic;

public interface IConsumable
{
    List<BaseStat> Stats { get; set; }
    void Consume();
    void Consume(CharacterStats stats);
}

