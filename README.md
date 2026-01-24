# MazeTilings

A .NET Standard 2.1 library for converting mazes (built on `CrawfisSoftware.Maze`) into Wang-tile tilings. It provides tiling builders, colorizers, and path tile helpers that can be consumed by any renderer. SVG output lives in a separate example project and is intentionally not covered here.

## Package dependencies
Referenced packages (see `MazeTilings.csproj`):
- `CrawfisSoftware.Maze` (0.1.2) — repo: https://github.com/Crawfis-Software/CrawfisSoftware.Maze (README: https://github.com/Crawfis-Software/CrawfisSoftware.Maze/blob/main/README.md)
- `CrawfisSoftware.Tiling` (0.2.0) — repo: https://github.com/Crawfis-Software/CrawfisSoftware.Tiling (README: https://github.com/Crawfis-Software/CrawfisSoftware.Tiling/blob/master/README.md, llms.txt: https://github.com/Crawfis-Software/CrawfisSoftware.Tiling/blob/master/llms.txt)

## Key components
- `MazeWrapperTilingBuilder`: maps a `Maze<int,int>` to tiles via a `ITilingEnumerator` and `ITileSelector`.
- `PathTileSetFactory`: builds arc-based or Manhattan-style path tile sets; includes `TileSizePathTileDecorator` for scaling tiles.
- `MazeColorizers` and `AbstractMazeColorizer`: utilities for turning maze directions/metrics into edge colors.
- `WangColorizer`: generates a Wang tile set on the fly from a colorizer function and builds the corresponding tiling.

## How to use (library-only)
1. Build or obtain a `Maze<int,int>` using the CrawfisSoftware maze builders.
2. Choose or build an `ITileSet` (any renderer-compatible set; the library itself is renderer-agnostic).
3. Select a `ITilingEnumerator` (visit order) and `ITileSelector` (matching strategy).
4. Instantiate `MazeWrapperTilingBuilder` (or `WangColorizer` for dynamic tile creation), set `Colorizer`, and call `UpdateTiling`.
5. Consume the resulting `ITiling2D` with your renderer of choice.

Example:
```csharp
var mazeBuilder = new MazeBuilder<int, int>(width, height);
Maze<int, int> maze = mazeBuilder.GetMaze();
var tileSet = /* any ITileSet compatible with your renderer */;
var tilingBuilder = new MazeWrapperTilingBuilder(maze)
{
    TileSelector = new SequentialTileSelector(2),
    TilingEnumerator = new SequentialTilingEnumerator(),
    Colorizer = MazeColorizers.ColorizeBasedOnDirectionOnly
};
tilingBuilder.UpdateTiling(tileSet);
ITiling2D tiling = tilingBuilder.GetTiling();
// Render tiling with your pipeline (outside this library).
```

## Notes
- This project is renderer-agnostic. SVG-specific code and demos live in `ExampleProjects/MazeTilingSVG`, not here. Unity Examples are also separate.
- Targets .NET Standard 2.1; usable from .NET 6/7/8+/Unity (with provided stubs) so long as the CrawfisSoftware dependencies are available.
