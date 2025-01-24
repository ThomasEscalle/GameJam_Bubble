using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Tile
{
    /// The name of the tile
    public string name;
    public Color color;
    public enum Type {  Wall, Asset};
    public Type type;
    public int tileX = 0;
    public int tileY = 0;
    public GameObject prefab;
}


public class WorldBuilder : MonoBehaviour
{
    /// The mesh filter
    public MeshFilter meshFilter;
    
    [Header("Tile Size")]
    /// The size of the tiles
    public float tileSize = 1f;

    [Header("Tiles")]
    /// The list of tiles
    public Tile[] tiles;
    private Dictionary<string, Tile> tileMap = new Dictionary<string, Tile>();


    [Header("TileMap Texture")]
    public int tilemapWidth = 2048;
    public int tileWidth = 256;


    [Header("Test")]


    /// The test object
    public GameObject testObject;



    // Start is called before the first frame update
    void Start()
    {

        /// Initialise the tile map dictionary
        foreach(Tile tile in tiles) {
            Color col = tile.color;
            col.a = 1;
            string hex = ColorUtility.ToHtmlStringRGBA(col);
            tileMap.Add(hex, tile);
        }


        /// Load the current level
        LoadLevel(GameManagerMain.instance.currentLevel);
    }


    public void LoadLevel(int level) {

        /// Get the level data
        Level levelData = GameManagerMain.instance.levels[level];
        if(levelData == null) {
            Debug.LogError("Level " + level + " not found");
            return;
        }


        /// Build the mesh for the level
        BuildMesh(levelData);

    }


    /// Build the mesh for the level
    public void BuildMesh(Level level) {


        /// Create a new mesh 
        Mesh mesh = new Mesh();

        /// Get the image to build the mesh from
        Texture2D image = level.image;

        /// Create the list of vertices
        List<Vector3> vertices = new List<Vector3>();
        /// Create the list of triangles
        List<int> triangles = new List<int>();
        /// Create the list of uvs
        List<Vector2> uvs = new List<Vector2>();
        /// Create the list of normals
        List<Vector3> normals = new List<Vector3>();


        /// Loop through the pixels of the image
        for(int i = 0 ; i < image.width; i++) {
            for(int j = 0; j < image.height; j++) {
                /// Get the color of the pixel
                Color color = image.GetPixel(i, j);
                string hex = ColorUtility.ToHtmlStringRGBA( color );
                
                /// If the color is white, skip
                if(color == Color.white) {
                    continue;
                }


                /// Check if the color is in the tile map
                if(tileMap.ContainsKey(hex)) {

                    Tile tile = tileMap[hex];
                    
                    /// If the tile is a wall
                    if(tile.type == Tile.Type.Wall) {
                        
                        bool bottom = false;
                        bool top = false;
                        bool left = false;
                        bool right = false;

                        /// Check there is a tile below, and if it is the same type
                        if(j > 0) {
                            Color bottomColor = image.GetPixel(i, j - 1);
                            string bottomHex = ColorUtility.ToHtmlStringRGBA(bottomColor);
                            if(tileMap.ContainsKey(bottomHex)) {
                                Tile bottomTile = tileMap[bottomHex];
                                if(bottomTile.type == Tile.Type.Wall) {
                                    bottom = true;
                                }
                            }
                        }

                        /// Check there is a tile above, and if it is the same type
                        if(j < image.height - 1) {
                            Color topColor = image.GetPixel(i, j + 1);
                            string topHex = ColorUtility.ToHtmlStringRGBA(topColor);
                            if(tileMap.ContainsKey(topHex)) {
                                Tile topTile = tileMap[topHex];
                                if(topTile.type == Tile.Type.Wall) {
                                    top = true;
                                }
                            }
                        }

                        /// Check there is a tile to the left, and if it is the same type
                        if(i > 0) {
                            Color leftColor = image.GetPixel(i - 1, j);
                            string leftHex = ColorUtility.ToHtmlStringRGBA(leftColor);
                            if(tileMap.ContainsKey(leftHex)) {
                                Tile leftTile = tileMap[leftHex];
                                if(leftTile.type == Tile.Type.Wall) {
                                    left = true;
                                }
                            }
                        }

                        /// Check there is a tile to the right, and if it is the same type
                        if(i < image.width - 1) {
                            Color rightColor = image.GetPixel(i + 1, j);
                            string rightHex = ColorUtility.ToHtmlStringRGBA(rightColor);
                            if(tileMap.ContainsKey(rightHex)) {
                                Tile rightTile = tileMap[rightHex];
                                if(rightTile.type == Tile.Type.Wall) {
                                    right = true;
                                }
                            }
                        }

                        /// Create a quad
                        createQuad(tile, i, j, ref vertices,ref triangles, ref uvs,  ref normals, bottom, top, left, right);


                    }
                    else if(tile.type == Tile.Type.Asset) {


                        /// Create the asset
                        GameObject asset = Instantiate(tile.prefab, new Vector3(i * tileSize, 0, j * tileSize), Quaternion.identity);
                        asset.transform.parent = this.transform;

                        
                    }
                    
                    continue;
                }

                else {
                    Debug.LogError("Color " + ColorUtility.ToHtmlStringRGBA( color ));
                }
            }
        }
    




        /// Set the mesh vertices
        mesh.vertices = vertices.ToArray();
        /// Set the mesh triangles
        mesh.triangles = triangles.ToArray();
        /// Set the mesh uvs
        mesh.uv = uvs.ToArray();
        /// Set the mesh normals
        mesh.normals = normals.ToArray();

        /// Set the mesh to the mesh filter
        meshFilter.mesh = mesh;
    }




    /// Create a quad
    public void createQuad(Tile tile, int x, int y, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs, ref List<Vector3> normals, bool bottom= false, bool top = false, bool left = false, bool right = false) {

        /// Create the vertices
        Vector3 bottomLeft = new Vector3(x, y, 0) * tileSize;
        Vector3 bottomRight = new Vector3(x + 1, y) * tileSize;
        Vector3 topLeft = new Vector3(x,  y + 1,0) * tileSize;
        Vector3 topRight = new Vector3(x + 1, y + 1,0 ) * tileSize;

        /// Add the vertices to the list
        vertices.Add(bottomLeft);
        vertices.Add(bottomRight);
        vertices.Add(topLeft);
        vertices.Add(topRight);

        /// Add the triangles to the list
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);

        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 3);

        /// Add the uvs to the list
        float tileSizeAAA = 1 / ((float)tilemapWidth / (float)tileWidth);
        float offset = 0.001f;
        Vector2 uv00 = new Vector2(tile.tileX * tileSizeAAA + offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv10 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv01 = new Vector2(tile.tileX * tileSizeAAA + offset, (tile.tileY + 1) * tileSizeAAA - offset);
        Vector2 uv11 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, (tile.tileY + 1) * tileSizeAAA - offset);

        uvs.Add(uv00);
        uvs.Add(uv10);
        uvs.Add(uv01);
        uvs.Add(uv11);

        /// Add the normals to the list
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);


        if(!bottom) {
            createBottomQuad(tile, x, y, ref vertices, ref triangles, ref uvs, ref normals);
        }
        if (!top) {
            createTopQuad(tile, x, y, ref vertices, ref triangles, ref uvs, ref normals);
        }
        if (!left) {
            createLeftQuad(tile, x, y, ref vertices, ref triangles, ref uvs, ref normals);
        }
        if(!right) {
            createRightQuad(tile, x, y, ref vertices, ref triangles, ref uvs, ref normals);
        }

    }

    public void createBottomQuad(Tile tile, int x, int y, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs, ref List<Vector3> normals) {
        /// Create the vertices
        Vector3 bottomLeft = new Vector3(x, y, 0) * tileSize;
        Vector3 bottomRight = new Vector3(x + 1, y) * tileSize;
        Vector3 topLeft = new Vector3(x, y, 1) * tileSize;
        Vector3 topRight = new Vector3(x + 1, y, 1) * tileSize;

        /// Add the vertices to the list
        vertices.Add(bottomLeft);
        vertices.Add(bottomRight);
        vertices.Add(topLeft);
        vertices.Add(topRight);

        /// Add the triangles to the list
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);

        /// Add the uvs to the list
        float tileSizeAAA = 1 / ((float)tilemapWidth / (float)tileWidth);
        float offset = 0.001f;
        Vector2 uv00 = new Vector2(tile.tileX * tileSizeAAA + offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv10 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv01 = new Vector2(tile.tileX * tileSizeAAA + offset, (tile.tileY + 1) * tileSizeAAA - offset);
        Vector2 uv11 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, (tile.tileY + 1) * tileSizeAAA - offset);

        uvs.Add(uv00);
        uvs.Add(uv10);
        uvs.Add(uv01);
        uvs.Add(uv11);

        /// Add the normals to the list
        normals.Add(Vector3.down);
        normals.Add(Vector3.down);
        normals.Add(Vector3.down);
        normals.Add(Vector3.down);
    }

    public void createTopQuad(Tile tile, int x, int y, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs, ref List<Vector3> normals) {
        /// Create the vertices
        Vector3 bottomLeft = new Vector3(x, y + 1, 0) * tileSize;
        Vector3 bottomRight = new Vector3(x + 1, y + 1) * tileSize;
        Vector3 topLeft = new Vector3(x, y + 1, 1) * tileSize;
        Vector3 topRight = new Vector3(x + 1, y + 1, 1) * tileSize;

        /// Add the vertices to the list
        vertices.Add(bottomLeft);
        vertices.Add(bottomRight);
        vertices.Add(topLeft);
        vertices.Add(topRight);

        /// Add the triangles to the list
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);

        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 3);

        /// Add the uvs to the list
        float tileSizeAAA = 1 / ((float)tilemapWidth / (float)tileWidth);
        float offset = 0.001f;
        Vector2 uv00 = new Vector2(tile.tileX * tileSizeAAA + offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv10 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv01 = new Vector2(tile.tileX * tileSizeAAA + offset, (tile.tileY + 1) * tileSizeAAA - offset);
        Vector2 uv11 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, (tile.tileY + 1) * tileSizeAAA - offset);

        uvs.Add(uv00);
        uvs.Add(uv10);
        uvs.Add(uv01);
        uvs.Add(uv11);

        /// Add the normals to the list
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
    }

    public void createLeftQuad(Tile tile, int x, int y, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs, ref List<Vector3> normals) {
        /// Create the vertices
        Vector3 bottomLeft = new Vector3(x, y, 0) * tileSize;
        Vector3 bottomRight = new Vector3(x, y + 1) * tileSize;
        Vector3 topLeft = new Vector3(x, y, 1) * tileSize;
        Vector3 topRight = new Vector3(x, y + 1, 1) * tileSize;

        /// Add the vertices to the list
        vertices.Add(bottomLeft);
        vertices.Add(bottomRight);
        vertices.Add(topLeft);
        vertices.Add(topRight);

        /// Add the triangles to the list
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);

        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 3);

        /// Add the uvs to the list
        float tileSizeAAA = 1 / ((float)tilemapWidth / (float)tileWidth);
        float offset = 0.001f;
        Vector2 uv00 = new Vector2(tile.tileX * tileSizeAAA + offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv10 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv01 = new Vector2(tile.tileX * tileSizeAAA + offset, (tile.tileY + 1) * tileSizeAAA - offset);
        Vector2 uv11 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, (tile.tileY + 1) * tileSizeAAA - offset);

        uvs.Add(uv00);
        uvs.Add(uv10);
        uvs.Add(uv01);
        uvs.Add(uv11);

        /// Add the normals to the list
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
    }


    public void createRightQuad(Tile tile, int x, int y, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs, ref List<Vector3> normals) {
        /// Create the vertices
        Vector3 bottomLeft = new Vector3(x + 1, y, 0) * tileSize;
        Vector3 bottomRight = new Vector3(x + 1, y + 1) * tileSize;
        Vector3 topLeft = new Vector3(x + 1, y, 1) * tileSize;
        Vector3 topRight = new Vector3(x + 1, y + 1, 1) * tileSize;

        /// Add the vertices to the list
        vertices.Add(bottomLeft);
        vertices.Add(bottomRight);
        vertices.Add(topLeft);
        vertices.Add(topRight);

        /// Add the triangles to the list
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);

        /// Add the uvs to the list
        float tileSizeAAA = 1 / ((float)tilemapWidth / (float)tileWidth);
        float offset = 0.001f;
        Vector2 uv00 = new Vector2(tile.tileX * tileSizeAAA + offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv10 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, tile.tileY * tileSizeAAA + offset);
        Vector2 uv01 = new Vector2(tile.tileX * tileSizeAAA + offset, (tile.tileY + 1) * tileSizeAAA - offset);
        Vector2 uv11 = new Vector2((tile.tileX + 1) * tileSizeAAA - offset, (tile.tileY + 1) * tileSizeAAA - offset);

        uvs.Add(uv00);
        uvs.Add(uv10);
        uvs.Add(uv01);
        uvs.Add(uv11);

        /// Add the normals to the list
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
        normals.Add(Vector3.back);
    }

}
