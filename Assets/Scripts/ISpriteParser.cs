using UnityEngine;

public interface ISpriteParser<T>
{
    bool TryParse(string fileName, Sprite sprite, out T t);
}