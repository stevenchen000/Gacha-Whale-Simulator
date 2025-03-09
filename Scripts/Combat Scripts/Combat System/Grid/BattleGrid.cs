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

		public Array<Vector2I> CurrentWalkableSpaces { get; private set; }



		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		public void InitGrid(int horizontal, int vertical, BattleState state)
        {
			horizontalGrids = horizontal;
			verticalGrids = vertical;
            InitBattleSpaces();
            SetAllSpacesToDefault();
        }



		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
            
        }

		


		/*********************
		 * Public functions
		 * *************/


		public void ShowAllTargetableAreas(Dictionary<CharacterDirection, Array<GridSpace>> spaces)
		{
			foreach(var list in spaces.Values)
			{
				foreach(var space in list)
				{
					space.SetCanTarget(true);
				}
			}
		}

		public void ResetCanTarget()
		{
			foreach(var space in battleSpaces)
			{
				space.SetCanTarget(false);
			}
		}

		public void ResetHasSelectedTarget()
		{
			foreach(var space in battleSpaces)
				space.SetHasSelectedTarget(false);
		}

		public void ResetSpaces()
		{
			foreach (var space in battleSpaces)
				space.ResetSpace();
		}

		public void ReapplyWalkableState()
		{
			foreach(var coord in CurrentWalkableSpaces)
			{
				var space = GetSpaceFromCoords(coord);
				space.SetWalkable(true);
			}
		}

		/******************
		 * Get Space
		 * ***************/

        #region Get Space

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

		#endregion


		private Vector2I CheckNearestNeighbors(Vector2I nearestSpace, BattleCharacter character)
        {
			var allCoords = new Array<Vector2I>();
			allCoords.Add(nearestSpace - new Vector2I(1, 0));
			allCoords.Add(nearestSpace + new Vector2I(1, 0));
			allCoords.Add(nearestSpace - new Vector2I(0, 1));
			allCoords.Add(nearestSpace + new Vector2I(0, 1));

			float nearestDistance = 9999;
			Vector2I result = new Vector2I(0,0);
			foreach(var coord in allCoords)
            {
                if (CurrentWalkableSpaces.Contains(coord))
                {
					var space = GetSpaceFromCoords(coord);
					float dist = (space.GlobalPosition - character.GlobalPosition).Length();
					if(dist < nearestDistance)
                    {
						nearestDistance = dist;
						result = coord;
                    }
                }
            }
			return result;
        }

		public Array<BattleCharacter> GetTargetsInRange(BattleManager battle, BattleCharacter character, GridShape attackShape)
        {
			var charPos = character.turnStartPosition;
			var direction = character.facingDirection;
			var targetPositions = attackShape.GetPositionsInRange(charPos, direction);
			var allTargets = PositionsToTargets(targetPositions);
			return GetValidTarget(battle, allTargets, character);
        }

		private Array<BattleCharacter> PositionsToTargets(Array<Vector2I> coords)
        {
			var result = new Array<BattleCharacter>();
			foreach(var coord in coords)
            {
				var space = GetSpaceFromCoords(coord);
				var target = space.CharacterOnSpace;
				if (target != null) result.Add(target);
            }
			return result;
        }

		private Array<BattleCharacter> GetValidTarget(BattleManager battle, Array<BattleCharacter> targets, BattleCharacter character)
        {
			var result = new Array<BattleCharacter>();

			foreach(var target in targets)
            {
				bool isEnemy = !battle.IsInSameParty(target, character);
				if (isEnemy) result.Add(target);
            }

			return result;
        }

		/******************
		 * Occupy and unoccupy spaces
		 * ****************/



		/*********************
		 * Walkable Spaces
		 * *****************/

		public Array<Vector2I> GetAllWalkableAreas(BattleCharacter character)
        {
			var position = character.turnStartPosition;//GetNearestSpaceToCharacter(character);
			int movement = character.stats.movement;
			var walkableSpaces = GetWalkableSpacesIgnoringObstacles(position, movement);

			//if character has Pass effect, skip the enemyspaces check
			walkableSpaces = RemoveSpacesWithEnemies(character, walkableSpaces);
			walkableSpaces = RemoveDisconnectedSpaces(walkableSpaces, character);
			walkableSpaces = RemoveAllOccupiedSpaces(character, walkableSpaces);

			return walkableSpaces;
		}
		public bool CheckIfSpaceIsWalkable(Vector2I position)
        {
			return CurrentWalkableSpaces.Contains(position);
        }
		public bool CheckIfSpaceIsWalkable(GridSpace space)
        {
			return space.IsWalkable;
        }

		/*******************
		 * Change Space State
		 * ****************/

		public void SetAllSpacesToDefault()
		{
			foreach (var space in battleSpaces)
			{
				space.ResetSpace();
			}
		}
		public void SetSpacesWalkable(BattleCharacter character)
		{
			var positions = character.WalkableSpaces;

			foreach (var position in positions)
			{
				var space = GetSpaceFromCoords(position);
				space.SetWalkable(true);
			}
		}


		/****************
		* Helper Functions
		* **************/

		private Array<Vector2I> GetWalkableSpacesIgnoringObstacles(Vector2I position, int movableSpaces)
        {
			int x = position.X;
			int y = position.Y;
			int lowerX = x - movableSpaces;
			int upperX = x + movableSpaces;
			int lowerY = y - movableSpaces;
			int upperY = y + movableSpaces;

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
					if(distance <= movableSpaces)
						walkableSpaces.Add(new Vector2I(i, j));
				}
			}

			return walkableSpaces;
        }

		private Array<Vector2I> RemoveSpacesWithEnemies(BattleCharacter character, Array<Vector2I> spaces)
        {
			Array<Vector2I> result = new Array<Vector2I>();

            foreach (var coord in spaces)
			{
				var space = GetSpaceFromCoords(coord);
				var spaceCharacter = space.CharacterOnSpace;
				if (spaceCharacter != null)
				{
					if(!character.IsEnemy(spaceCharacter))
                    {
						result.Add(coord);
                    }
                }
                else
                {
					result.Add(coord);
                }
			}

			return result;
        }

		private Array<Vector2I> RemoveDisconnectedSpaces(Array<Vector2I> spaces, BattleCharacter character)
        {
			var currPos = character.turnStartPosition;
			var result = new Array<Vector2I>();
			result.Add(currPos);

			AddAllWalkableNeighbors(currPos, spaces, result);
			return result;
        }

		private Array<Vector2I> RemoveAllOccupiedSpaces(BattleCharacter character, Array<Vector2I> spaces)
		{
			var result = new Array<Vector2I>();

			foreach(var coords in spaces)
			{
				var space = GetSpaceFromCoords(coords);
				var spaceCharacter = space.CharacterOnSpace;

				if(spaceCharacter == null || spaceCharacter == character)
				{
					result.Add(coords);
				}
			}

			return result;
		}

		private void AddAllWalkableNeighbors(Vector2I currSpace, Array<Vector2I> originalSpaces, Array<Vector2I> finalSpaces)
        {
			int x = currSpace.X;
			int y = currSpace.Y;
			//left neighbor
			var left = new Vector2I(x-1, y);
			var right = new Vector2I(x + 1, y);
			var up = new Vector2I(x, y + 1);
			var down = new Vector2I(x, y - 1);

			if(SpaceNeedsToBeChecked(left, originalSpaces, finalSpaces))
            {
				finalSpaces.Add(left);
				AddAllWalkableNeighbors(left, originalSpaces, finalSpaces);
            }
			if(SpaceNeedsToBeChecked(right, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(right);
				AddAllWalkableNeighbors(right, originalSpaces, finalSpaces);
			}
			if(SpaceNeedsToBeChecked(up, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(up);
				AddAllWalkableNeighbors(up, originalSpaces, finalSpaces);
			}
			if(SpaceNeedsToBeChecked(down, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(down);
				AddAllWalkableNeighbors(down, originalSpaces, finalSpaces);
			}

		}

		private bool SpaceNeedsToBeChecked(Vector2I space, Array<Vector2I> originalSpaces, Array<Vector2I> finalSpaces)
        {
			return originalSpaces.Contains(space) && !finalSpaces.Contains(space);

		}

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
					space.InitSpace(position);
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
			//space.SetState(GridState.BOUNDARY);
			borderSpaces.Add(space);
			
		}

		private GridSpace InstantiateBattleSpace()
        {
			var space = (GridSpace)Utils.InstantiateCopy(spaceScene);
			AddChild(space);
			
			return space;
        }

		private void SetupGridspacePosition(GridSpace space, int x, int y)
        {
			Vector2 position = startingPoint;
			position += x * horizontalOffset;
			position += y * verticalOffset;
			space.Position = position;
		}
	}
}
