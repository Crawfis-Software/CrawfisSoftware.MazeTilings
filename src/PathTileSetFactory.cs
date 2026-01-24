using CrawfisSoftware.Collections.Graph;
using CrawfisSoftware.Tiling.TileSets;

using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#else
using CrawfisSoftware.MazeTilings.UnityEngine;
#endif

namespace CrawfisSoftware.Tiling
{
    /// <summary>
    /// Class to create an arc-based path tileSet or a Manhattan like path tileSet
    /// </summary>
    public static class PathTileSetFactory
    {
        /// <summary>
        /// A vertical path tile in a straight line. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Straight0101;
        /// <summary>
        /// A horizontal path tile in a straight line. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Straight1010;
        /// <summary>
        /// Path tile that turns from right edge to bottom edge in an arc. Numbering goes left,top,right,bottom
        /// </summary>
        public static PathTile Circle0011;
        /// <summary>
        /// Path tile that turns from top edge to right edge in an arc. Numbering goes left,top,right,bottom
        /// </summary>
        public static PathTile Circle0110;
        /// <summary>
        /// Path tile that turns from left edge to bottom edge in an arc. Numbering goes left,top,right,bottom
        /// </summary>
        public static PathTile Circle1001;
        /// <summary>
        /// Path tile that turns from left edge to top edge in an arc. Numbering goes left,top,right,bottom
        /// </summary>
        public static PathTile Circle1100;
        /// <summary>
        /// Path tile that turns from right edge to bottom edge along axes. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Manhattan0011;
        /// <summary>
        /// Path tile that turns from top edge to right edge along axes. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Manhattan0110;
        /// <summary>
        /// Path tile that turns from left edge to bottom edge along axes. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Manhattan1001;
        /// <summary>
        /// Path tile that turns from left edge to bottom top along axes. Numbering goes left,top,right,bottom
        /// </summary>
        public static readonly PathTile Manhattan1100;

        /// <summary>
        /// Create a path tile set (6 tiles) with circle arc turns and straight aways
        /// </summary>
        /// <returns>An <paramref>ITileSet</paramref> containing PathTiles with 4 smooth turns and 2 straight-aways</returns>
        public static ITileSet GenerateArcTileSet(float tileWidth, float tileHeight)
        {
            string name = String.Format("PathArcs{0}x{1}", (int)tileWidth, (int)tileHeight);
            var tileSet = new TileSet(name, "The six tiles for continuous paths: 4 turns and two straights", tileWidth, tileHeight);
            tileSet.AddKeyword("Path");
            tileSet.AddKeyword("Arc");

            var scale = new Vector3(tileWidth, 0, tileHeight);
            var tile = new TileSizePathTileDecorator(Circle0011, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Circle0110, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Circle1001, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Circle1100, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Straight0101, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Straight1010, scale);
            tileSet.AddTile(tile);

            return tileSet;
        }

        /// <summary>
        /// Create a path tile set (6 tiles) with sharp turns
        /// </summary>
        /// <returns>A tileSet containing PathTiles with 4 sharp turns and 2 straight-aways</returns>
        public static ITileSet GenerateManhattanTileSet(float tileWidth, float tileHeight)
        {
            string name = String.Format("PathManhattan{0}x{1}", tileWidth, tileHeight);
            var tileSet = new TileSet(name, "The six tiles for continuous paths: 4 turns and two straights", tileWidth, tileHeight);
            tileSet.AddKeyword("Path");
            tileSet.AddKeyword("Manhattan");

            var scale = new Vector3(tileWidth, 0, tileHeight);
            var tile = new TileSizePathTileDecorator(Manhattan0011, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Manhattan0110, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Manhattan1001, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Manhattan1100, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Straight0101, scale);
            tileSet.AddTile(tile);
            tile = new TileSizePathTileDecorator(Straight1010, scale);
            tileSet.AddTile(tile);

            return tileSet;
        }

        static PathTileSetFactory()
        {
            const int numberOfSamples = 60;
            const float radius = 0.5f;
            ReComputeArcTiles(numberOfSamples, radius);

            Straight0101 = new PathTile("PathStraight", 4, Direction.S, Direction.N, new Vector3[] {
                new Vector3(0.5f,0,0), new Vector3(0.5f,0,1f) });
            Straight1010 = new PathTile("PathStraight", 5, Direction.W, Direction.E, new Vector3[] {
                new Vector3(0,0,0.5f), new Vector3(1f,0,0.5f) });

            Manhattan0011 = new PathTile("PathManhattan", 6, Direction.S, Direction.E, new Vector3[] {
                new Vector3(0.5f,0,0), new Vector3(0.5f,0,0.47f), new Vector3(0.51f, 0, 0.49f),
                new Vector3(0.52f, 0, 0.51f), new Vector3(0.53f, 0, 0.5f), new Vector3(1.0f,0,0.5f)});
            Manhattan0110 = new PathTile("PathManhattan", 7, Direction.N, Direction.E, new Vector3[] {
                new Vector3(0.5f,0,1), new Vector3(0.5f,0,0.53f), new Vector3(0.51f, 0, 0.51f),
                new Vector3(0.52f, 0, 0.51f), new Vector3(0.53f, 0, 0.5f), new Vector3(1.0f,0,0.5f)});
            Manhattan1001 = new PathTile("PathManhattan", 8, Direction.S, Direction.W, new Vector3[] {
                new Vector3(0.5f,0,0), new Vector3(0.5f,0,0.47f), new Vector3(0.49f, 0, 0.49f),
                new Vector3(0.48f, 0, 0.51f), new Vector3(0.47f, 0, 0.5f), new Vector3(0.0f,0,0.5f)});
            Manhattan1100 = new PathTile("PathManhattan", 9, Direction.N, Direction.W, new Vector3[] {
                new Vector3(0.5f,0,1), new Vector3(0.5f,0,0.53f), new Vector3(0.49f, 0, 0.51f),
                new Vector3(0.48f, 0, 0.49f), new Vector3(0.47f, 0, 0.5f), new Vector3(0.0f,0,0.5f)});
        }

        /// <summary>
        /// Recompute the arc tiles based on the number of samples and the radius
        /// </summary>
        /// <param name="numberOfSamples">Number of samples on the quarter-circle.</param>
        /// <param name="radius">The radius of the circle or the half-width of the tile. Defaults to 1/2.</param>
        public static void ReComputeArcTiles(int numberOfSamples, float radius = 0.5f)
        {
            var quadrant01Points1001 = new Vector3[numberOfSamples];
            var quadrant02Points0011 = new Vector3[numberOfSamples];
            var quadrant03Points1100 = new Vector3[numberOfSamples];
            var quadrant04Points1001 = new Vector3[numberOfSamples];
            float theta = 0;
            float piOver2 = 0.5f * Mathf.PI;
            float delta = piOver2 / (float)(numberOfSamples - 1);
            for (int i = 0; i < numberOfSamples; i++)
            {
                float x, z;
                x = radius * Mathf.Cos(theta);
                z = radius * Mathf.Sin(theta);
                quadrant01Points1001[i] = new Vector3(x, 0, z);
                float phi = theta + piOver2;
                x = radius * Mathf.Cos(phi);
                x += 1;
                z = radius * Mathf.Sin(phi);
                quadrant02Points0011[i] = new Vector3(x, 0, z);
                phi += piOver2;
                x = radius * Mathf.Cos(phi);
                x += 1;
                z = radius * Mathf.Sin(phi);
                z += 1;
                quadrant03Points1100[i] = new Vector3(x, 0, z);
                phi += piOver2;
                x = radius * Mathf.Cos(phi);
                z = radius * Mathf.Sin(phi);
                z += 1;
                quadrant04Points1001[i] = new Vector3(x, 0, z);

                theta += delta;
            }
            // All arc paths are clockwise around the circle
            Circle1001 = new PathTile("PathArc", 0, Direction.S, Direction.W, quadrant01Points1001);
            Circle0011 = new PathTile("PathArc", 1, Direction.E, Direction.S, quadrant02Points0011);
            Circle0110 = new PathTile("PathArc", 2, Direction.N, Direction.E, quadrant03Points1100);
            Circle1100 = new PathTile("PathArc", 3, Direction.W, Direction.N, quadrant04Points1001);
        }
    }
}