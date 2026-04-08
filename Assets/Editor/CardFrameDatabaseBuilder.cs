using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class CardFrameDatabaseBuilder
{
    private const string SourceRootFolder = "Assets/Art/CardFrames";
    private const string DatabaseAssetPath = "Assets/ScriptableObjects/CardFrameDatabase.asset";

    [MenuItem("Tools/Card/Build Card Frame Database")]
    public static void Build()
    {
        var database = LoadOrCreateDatabase();

        database.backgroundSprites.Clear();
        database.frameSprites.Clear();
        database.bannerSprites.Clear();
        database.typeIconSprites.Clear();
        database.cardOrbSprites.Clear();

        FillEntries(
            $"{SourceRootFolder}/Background",
            database.backgroundSprites,
            new BackgroundSpriteParser()
        );

        FillEntries(
            $"{SourceRootFolder}/Frame",
            database.frameSprites,
            new FrameSpriteParser()
        );

        FillEntries(
            $"{SourceRootFolder}/Banner",
            database.bannerSprites,
            new BannerSpriteParser()
        );

        FillEntries(
            $"{SourceRootFolder}/TypeIcon",
            database.typeIconSprites,
            new TypeSpriteParser()
        );

        FillEntries(
            $"{SourceRootFolder}/CardOrb",
            database.cardOrbSprites,
            new CardOrbSpriteParser()
        );

        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("CardFrameDatabase 자동 생성/갱신 완료");
    }

    private static void FillEntries<T>(
        string folderPath,
        List<T> targetList,
        ISpriteParser<T> parser)
    {
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            if (sprite == null)
                continue;

            string fileName = Path.GetFileNameWithoutExtension(path);

            if (parser.TryParse(fileName, sprite, out T entry))
            {
                targetList.Add(entry);
            }
            else
            {
                Debug.LogWarning($"파싱 실패: {path}");
            }
        }
    }

    private static CardFrameDatabase LoadOrCreateDatabase()
    {
        var database = AssetDatabase.LoadAssetAtPath<CardFrameDatabase>(DatabaseAssetPath);
        if (database != null)
            return database;

        string folderPath = Path.GetDirectoryName(DatabaseAssetPath);
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            CreateFolderRecursive(folderPath);
        }

        database = ScriptableObject.CreateInstance<CardFrameDatabase>();
        AssetDatabase.CreateAsset(database, DatabaseAssetPath);
        AssetDatabase.SaveAssets();

        return database;
    }

    private static void CreateFolderRecursive(string folderPath)
    {
        string[] split = folderPath.Split('/');
        string current = split[0];

        for (int i = 1; i < split.Length; i++)
        {
            string next = current + "/" + split[i];
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, split[i]);
            }
            current = next;
        }
    }
}