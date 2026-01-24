using CrawfisSoftware.Maze;

using System;

namespace CrawfisSoftware.Tiling
{
    public abstract class AbstractMazeColorizer
    {
        protected readonly Maze<int, int> _maze;
        protected readonly MazeMetrics _mazeMetrics;
        protected readonly Random _random;
        private MazeMetricsComputations<int, int> _mazeComputations;

        public Func<int, int, int[]> Colorizer { get { return ColorizerBase; } }

        public AbstractMazeColorizer(Maze<int, int> maze, Random random)
        {
            _maze = maze;
            _random = random;
            ComputeMazeMetrics();
        }

        protected int[] ColorizerBase(int column, int row)
        {
            return ColorizeBaseOnMetrics(_maze, column, row, _mazeMetrics, _mazeComputations.GetCellMetrics(column + row * _maze.Width));
        }

        protected abstract int[] ColorizeBaseOnMetrics(Maze<int, int> maze, int column, int row, MazeMetrics mazeMetrics, MazeCellMetrics mazeCellMetrics);

        private void ComputeMazeMetrics()
        {
            _mazeComputations = new MazeMetricsComputations<int, int>(_maze);
            _mazeComputations.ComputeAllMetrics(_random, Collections.Graph.Direction.N);
        }
    }
}