using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
	public partial class BattleGrid : Node2D
	{
		private Array<GridSpace> battleSpaces;
		private Dictionary<Vector2I, GridSpace> gridPositions;
		private Array<GridSpace> borderSpaces;

		[Export] private PackedScene spaceScene;
		[Export] private Vector2 startingPoint;
		[Export] private Vector2 horizontalOffset;
		[Export] private Vector2 verticalOffset;

		[Export] private int horizontalGrids = 7;
		[Export] private int verticalGrids = 3;

		private string spaceScenePath = "";

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GD.Print("Battle Grid running");
			InitScenePath();
			InitBattleSpaces();
			InitBoundarySpaces();
			SetAllSpacesToDefault();
			GD.Print(battleSpaces.Count);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
            
        }

		/*********************
		 * Public functions
		 * *************/

		public GridSpace GetSpaceFromCoords(int x, int y)
		{
			Vector2I coords = new Vector2I(x, y);
			GridSpace result = null;

			if (gridPositions.ContainsKey(coords))
			{
				result = gridPositions[coords];
			}

			return result;
		}

		public GridSpace GetSpaceFromCoords(Vector2I pos)
        {
			int x = pos.X;
			int y = pos.Y;
			return GetSpaceFromCoords(x, y);
        }

		public void SetAllSpacesToDefault()
		{
			foreach (var space in battleSpaces)
			{
				space.SetState(GridState.DEFAULT);
			}
			GD.Print("Reset all spaces");
		}

		public GridSpace GetNearestSpace(Vector2 position)
        {
			GridSpace result = null;
			var nearestCoord = CalculateNearestGridSpace(position);
			result = GetSpaceFromCoords(nearestCoord);
			return result;
        }

		public Vector2I CalculateNearestGridSpace(Vector2 position)
		{
			float baseY = startingPoint.Y;
			float currY = position.Y;
			float diffY = baseY - currY;
			float verticalOffsetDistance = MathF.Abs(verticalOffset.Y);

			int vertical = (int)MathF.Round(diffY / verticalOffsetDistance);

			float baseX = startingPoint.X;
			float currX = position.X;
			float horizontalOffsetDistance = horizontalOffset.X;
			float horizontalOffsetBasedOnVertical = vertical * verticalOffset.X;
			float diffX = currX - baseX - horizontalOffsetBasedOnVertical;

			int horizontal = (int)MathF.Round(diffX / horizontalOffsetDistance);

			//GD.Print($"{horizontal}, {vertical}");

			return new Vector2I(horizontal, vertical);
		}

		public Array<Vector2I> RevealAllWalkableAreas(Vector2I position, int movableSpaces)
        {
			int x = position.X;
			int y = position.Y;
			int lowerX = x - movableSpaces;
			int upperX = x + movableSpaces;
			int lowerY = y - movableSpaces;
			int upperY = y + movableSpaces;
			GD.Print($"{movableSpaces} movable spaces");
			GD.Print($"Starting at ({position})");

			var walkableSpaces = new Array<Vector2I>();

			for (int i = lowerX; i <= upperX; i++)
			{
				for (int j = lowerY; j <= upperY; j++)
				{
					int distX = Math.Abs(x - i);
					int distY = Math.Abs(y - j);
					int distance = distX + distY;
					var space = GetSpaceFromCoords(i, j);
					if (space == null) continue;

					if(distance == 0)
                    {
						space.SetState(GridState.ALLY_STANDING);
						//GD.Print("Default space revealed");
                    }else if (distance <= movableSpaces)
					{
						space.SetState(GridState.ALLY_MOVEABLE);
						//GD.Print("New space revealed");
					}

					walkableSpaces.Add(new Vector2I(i, j));
				}
			}
			GD.Print("Walkable areas revealed");

			return walkableSpaces;
		}

		public bool CheckIfSpaceIsWalkable(Vector2I position)
        {
			var space = GetSpaceFromCoords(position);
			return space.IsWalkable();
        }

		public bool CheckIfSpaceIsWalkable(GridSpace space)
        {
			return space.IsWalkable();
        }


		/****************
		* Helper Functions
		* **************/

		private void InitBattleSpaces()
		{
			battleSpaces = new Array<GridSpace>();
			gridPositions = new Dictionary<Vector2I, GridSpace>();

			for (int i = 0; i < horizontalGrids; i++)
			{
				for (int j = 0; j < verticalGrids; j++)
				{
					var space = InstantiateBattleSpace();
					SetupGridspacePosition(space, i, j);
					battleSpaces.Add(space);
					var position = new Vector2I(i, j);
					gridPositions[position] = space;
				}
			}
		}

		private void InitBoundarySpaces()
        {
			borderSpaces = new Array<GridSpace>();

			for(int i = -1; i < horizontalGrids + 1; i++)
            {
				CreateBoundarySpace(i, -1);
				CreateBoundarySpace(i, verticalGrids);
            }

			for(int j = 0; j < verticalGrids; j++)
            {
				CreateBoundarySpace(-1, j);
				CreateBoundarySpace(horizontalGrids, j);
            }
        }

		private void CreateBoundarySpace(int x, int y)
        {
			var space = InstantiateBattleSpace();
			SetupGridspacePosition(space, x, y);
			space.SetState(GridState.BOUNDARY);
			borderSpaces.Add(space);
		}

		private GridSpace InstantiateBattleSpace()
        {
			var scene = ResourceLoader.Load<PackedScene>(spaceScenePath);
			var space = scene.Instantiate<GridSpace>();
			AddChild(space);
			
			return space;
        }

		private void SetupGridspacePosition(GridSpace space, int x, int y)
        {
			Vector2 position = startingPoint;
			position += x * horizontalOffset;
			position += y * verticalOffset;
			//GD.Print(position);
			space.Position = position;
		}


		private void InitScenePath()
        {
			spaceScenePath = spaceScene.ResourcePath;
        }
	}
}
