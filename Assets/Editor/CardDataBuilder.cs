#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class CardDataBuilder
{
    private const string CardArtsRoot = "Assets/Art/CardArts";
    private const string CardDataRoot = "Assets/ScriptableObjects/Cards";

    [MenuItem("Tools/Card/Generate CardData From CardArts")]
    public static void Build()
    {
        if (!AssetDatabase.IsValidFolder(CardArtsRoot))
        {
            Debug.LogError($"폴더를 찾을 수 없습니다: {CardArtsRoot}");
            return;
        }

        EnsureFolder(CardDataRoot);

        string[] colorFolderGuids = AssetDatabase.FindAssets("", new[] { CardArtsRoot });
        int createdCount = 0;
        int skippedCount = 0;

        foreach (string guid in colorFolderGuids)
        {
            string folderPath = AssetDatabase.GUIDToAssetPath(guid);

            if (!AssetDatabase.IsValidFolder(folderPath) || folderPath == CardArtsRoot)
                continue;

            string folderName = Path.GetFileName(folderPath);

            if (!Enum.TryParse(folderName, out CardColor cardColor))
            {
                Debug.LogWarning($"CardColor enum에 없는 폴더는 건너뜁니다: {folderName}");
                continue;
            }

            string targetFolderPath = $"{CardDataRoot}/{folderName}";
            EnsureFolder(targetFolderPath);

            string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });

            foreach (string spriteGuid in spriteGuids)
            {
                string spritePath = AssetDatabase.GUIDToAssetPath(spriteGuid);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

                if (sprite == null)
                    continue;

                var parts = SpriteParseUtility.SplitByDash(sprite.name);

                if (parts.Count != 3)
                {
                    Debug.LogWarning($"이름 형식이 올바르지 않습니다: {sprite.name}");
                    continue;
                }

                // 0: CardColor 검사
                if (!Enum.TryParse(parts[0], out CardColor parsedColor))
                {
                    Debug.LogWarning($"CardColor 파싱 실패: {parts[0]}");
                    continue;
                }

                if (parsedColor != cardColor)
                {
                    Debug.LogWarning($"폴더 Color와 파일 Color 불일치: {sprite.name}");
                    continue;
                }

                // 2: "Art" 확인
                if (parts[2] != "Art")
                {
                    continue;
                }

                // 1: 카드 이름
                string cardName = parts[1];

                string assetName = $"{cardName}.asset";
                string assetPath = $"{targetFolderPath}/{assetName}";

                CardData existing = AssetDatabase.LoadAssetAtPath<CardData>(assetPath);
                if (existing != null)
                {
                    skippedCount++;
                    continue;
                }

                CardData newCardData = ScriptableObject.CreateInstance<CardData>();
                newCardData.cardName = cardName;
                newCardData.cardColor = cardColor;
                newCardData.cardImage = sprite;

                AssetDatabase.CreateAsset(newCardData, assetPath);
                createdCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"CardData 생성 완료 - 생성: {createdCount}, 건너뜀: {skippedCount}");
    }

    private static void EnsureFolder(string folderPath)
    {
        if (AssetDatabase.IsValidFolder(folderPath))
            return;

        string parent = Path.GetDirectoryName(folderPath)?.Replace("\\", "/");
        string folderName = Path.GetFileName(folderPath);

        if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent))
        {
            EnsureFolder(parent);
        }

        if (!string.IsNullOrEmpty(parent))
        {
            AssetDatabase.CreateFolder(parent, folderName);
        }
    }
}
#endif