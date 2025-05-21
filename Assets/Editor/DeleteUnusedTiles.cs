using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class DeleteUnusedTiles : EditorWindow
{
    private static string tileFolderPath = "Assets/Tileset"; // Cambia esto si tus tiles están en otra carpeta

    [MenuItem("Tools/Delete Unused Tiles")]
    public static void DeleteUnusedTilesFunc()
    {
        HashSet<TileBase> usedTiles = new HashSet<TileBase>();
        Tilemap[] tilemaps = GameObject.FindObjectsOfType<Tilemap>();

        // Recolectar tiles usados en todos los Tilemaps de la escena
        foreach (Tilemap tilemap in tilemaps)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            foreach (TileBase tile in allTiles)
            {
                if (tile != null)
                {
                    usedTiles.Add(tile);
                }
            }
        }

        Debug.Log($"Tiles usados detectados: {usedTiles.Count}");

        // Buscar todos los tiles en la carpeta
        string[] tileAssetPaths = Directory.GetFiles(tileFolderPath, "*.asset", SearchOption.AllDirectories);
        int deletedCount = 0;

        foreach (string path in tileAssetPaths)
        {
            TileBase tile = AssetDatabase.LoadAssetAtPath<TileBase>(path);

            if (tile != null && !usedTiles.Contains(tile))
            {
                bool result = AssetDatabase.DeleteAsset(path);
                if (result)
                {
                    Debug.Log($"Eliminado tile no usado: {path}");
                    deletedCount++;
                }
                else
                {
                    Debug.LogWarning($"No se pudo eliminar: {path}");
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Eliminación completa. Tiles eliminados: {deletedCount}");
    }
}
